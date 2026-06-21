extends Node

func _ready():
	DisplayServer.window_set_mode(DisplayServer.WINDOW_MODE_WINDOWED)
	DisplayServer.window_set_flag(DisplayServer.WINDOW_FLAG_BORDERLESS, true)
	var screen_size = DisplayServer.screen_get_size()
	DisplayServer.window_set_size(screen_size)
	DisplayServer.window_set_position(Vector2i.ZERO)
