extends Node

func _ready():
	var screen_size = DisplayServer.screen_get_size()
	# borderless setup matching the screen's resolution
	get_window().mode = Window.MODE_WINDOWED
	get_window().borderless = true
	get_window().size = screen_size
	get_window().position = Vector2i.ZERO
	await $DataLoader.load_data()

func _process(delta: float) -> void:
	pass
