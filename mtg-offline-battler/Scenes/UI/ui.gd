extends CanvasLayer

var progress_bar: ProgressBar
var status_label: Label

func _ready():
	progress_bar = $DataLoadingProgress
	status_label = $ProgressLabel
	progress_bar.min_value = 0
	progress_bar.max_value = 100

func set_progress(loading_value):
	progress_bar.value = loading_value

func set_status(text):
	status_label.text = text

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass
