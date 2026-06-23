using Godot;
using System.Collections.Generic;
using System.Text.Json;

public partial class CardParser : Node2D
{
	// string = oracle_id
	public static Dictionary<string, CardData> cards = new();
	public static Dictionary<string, List<PrintingData>> printings = new();

	public static void parseJSON(string default_cards)
	{
		using JsonDocument doc = JsonDocument.Parse(default_cards);
		JsonElement root = doc.RootElement;
		foreach (JsonElement card in root.EnumerateArray())
		{
            if (!cards.ContainsKey(card.GetProperty("oracle_id").GetString()))
			{
				CardData newCard = new CardData
				{
					name = card.GetProperty("name").GetString(),
					id = card.GetProperty("id").GetString(),
					oracle_id = card.GetProperty("oracle_id").GetString(),
				};
				cards[newCard.oracle_id] = newCard;
				// create entry in dictionary with the first printing
			}
			else
			{
				// add the card image to the printings dictionary for the existing entry
			}

		}
	}
}
