extends Node

var local_update_at
var remote_update_at

var default_cards_url

var is_importing = false

func load_data():
	load_meta()
	await $bulk_data_file.request("https://api.scryfall.com/bulk-data")

func load_meta():
	if FileAccess.file_exists("user://meta.json"):
		var file = FileAccess.open("user://meta.json", FileAccess.READ)
		var data = JSON.parse_string(file.get_as_text())
		local_update_at = data.get("default_cards_updated_at", "")

func should_retry(response):
	return response == 408 or response == 429 or response >= 500
	
func retry_download(url):
	await get_tree().create_timer(5.0).timeout
	await $bulk_data_file.request(url)

func _on_bulk_data_file_request_completed(result: int, response_code: int, headers: PackedStringArray, body: PackedByteArray) -> void:
	if (should_retry(response_code)):
		await retry_download("https://api.scryfall.com/bulk-data")
		return
	var json = JSON.parse_string(body.get_string_from_utf8())
	for bulk in json["data"]:
		if (bulk["type"] == "default_cards"):
			remote_update_at = bulk["updated_at"]
			if (should_update()):
				default_cards_url = bulk["download_uri"]
				await $default_cards_file.request(bulk["download_uri"])

func update_meta_file():
	var meta = {
		"default_cards_updated_at": remote_update_at
	}
	var file = FileAccess.open("user://meta.json", FileAccess.WRITE)
	file.store_string(JSON.stringify((meta), "\t"))
	file.close()

func _on_default_cards_file_request_completed(result: int, response_code: int, headers: PackedStringArray, body: PackedByteArray) -> void:
	if (should_retry(response_code)):
		await retry_download(default_cards_url)
		return
	# var default_cards = JSON.parse_string(body.get_string_from_utf8())
	# to filter the data and build the json data files
	
	# block user input and display something to say that the cards are being imported
	is_importing = true
	# await save_cards(default_cards)
	is_importing = false
	update_meta_file()

func should_update():
	if local_update_at == null:
		return true
	return remote_update_at > local_update_at
