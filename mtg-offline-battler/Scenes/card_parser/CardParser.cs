using Godot;
using System.Collections.Generic;
using System.Text.Json;

public partial class CardParser : Node2D
{
	// Called when the node enters the scene tree for the first time.
	private Dictionary<string, CardData> cards = new();
	private Dictionary<string, List<PrintingData>> printings = new();
	
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public static void parseJSON(string default_cards)
	{
		using JsonDocument doc = JsonDocument.Parse(default_cards);
		JsonElement root = doc.RootElement;
		foreach (JsonElement card in root.EnumerateArray())
		{
			string name = card.GetProperty("name").GetString();
		}
	}
}
