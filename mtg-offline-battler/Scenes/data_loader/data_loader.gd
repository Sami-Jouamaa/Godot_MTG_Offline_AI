extends Node

var requestJSON

func _ready() -> void:
	$HTTPRequest.request_completed.connect(_on_request_completed)

func load_data():
	print("hello")
	# make http request to scryfall for cards' info and images
	
	# this is just the different types of bulk data files
	# use the Default cards download link, contains every print of every english cards
	# for example, Double major has 5 prints, the "Default cards" json file contains 5 entries of Double major, one for each print
	# await $HTTPRequest.request("https://api.scryfall.com/bulk-data")

func _on_request_completed(result, response_code, headers, body):
	requestJSON = JSON.parse_string(body.get_string_from_utf8())
	print(requestJSON)
