using Godot;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

public partial class CardParser : Node2D
{
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
				CardData newCard = new CardData();
				newCard.oracle_id = card.GetProperty("oracle_id").GetString();
				newCard.name = card.GetProperty("name").GetString();
				newCard.layout = card.GetProperty("layout").GetString();
				newCard.card_faces =
					card.TryGetProperty("card_faces", out JsonElement faces)
						? JsonSerializer.Deserialize<CardFaceData[]>(faces.GetRawText())
						: null;
				newCard.cmc = card.GetProperty("cmc").GetDouble();
				newCard.colour_identity = GetStringArrayOrNull(card, "color_identity");
				newCard.colour_indicator = GetStringOrNull(card, "color_indicator");
				newCard.colours = GetStringArrayOrNull(card, "colors");
				newCard.defense = GetStringOrNull(card, "defense");
				newCard.hand_modifier = GetStringOrNull(card, "hand_modifier");
				newCard.keywords =
					card.GetProperty("keywords")
						.EnumerateArray()
						.Select(x => x.GetString()!)
						.ToArray();
				newCard.life_modifier = GetStringOrNull(card, "life_modifier");
				newCard.loyalty = GetStringOrNull(card, "loyalty");
				newCard.mana_cost = GetStringOrNull(card, "mana_cost");
				newCard.oracle_text = card.GetProperty("oracle_text").GetString();
				newCard.power = GetStringOrNull(card, "power");
				newCard.toughness = GetStringOrNull(card, "thoughness");
				newCard.produced_mana = GetStringArrayOrNull(card, "produced_mana");
				newCard.type_line = card.GetProperty("type_line").GetString();
				cards[newCard.oracle_id] = newCard;
			}
			PrintingData newPrinting = new PrintingData();
			newPrinting.oracle_id = card.GetProperty("oracle_id").GetString();
			printings[newPrinting.oracle_id].Add(newPrinting);
		}
		// write to cards.json and printings.json in user data folder
	}

#nullable enable
	private static string[]? GetStringArrayOrNull(JsonElement card, string property)
	{
		return card.TryGetProperty(property, out JsonElement value)
		? value.EnumerateArray().Select(x => x.GetString()!).ToArray()
			: null;
	}

	private static string? GetStringOrNull(JsonElement card, string property)
	{
		return card.TryGetProperty(property, out JsonElement value)
			? value.GetString()
			: null;
	}
}
