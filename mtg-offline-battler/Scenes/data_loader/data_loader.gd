extends Node

var ui

var is_dev_mode = true

var local_update_at
var remote_update_at

var default_cards_url
var tags_url

func _ready():
	ui = get_parent().get_node("UI")

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
			default_cards_url = bulk["download_uri"]
		if (bulk["type"] == "oracle_tags"):
			tags_url = bulk["download_uri"]
	
	
	ui.set_status("Downloading cards")
	await $CardParser.parseJSON(default_cards_url, tags_url)
	update_meta_file()

func update_meta_file():
	var meta = {
		"default_cards_updated_at": remote_update_at
	}
	var file = FileAccess.open("user://meta.json", FileAccess.WRITE)
	file.store_string(JSON.stringify((meta), "\t"))
	file.close()

func should_update():
	if is_dev_mode or local_update_at == null:
		return true
	return remote_update_at > local_update_at
