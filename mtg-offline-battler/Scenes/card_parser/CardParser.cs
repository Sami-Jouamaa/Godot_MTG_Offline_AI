using System.Net.Http;
using Godot;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

public partial class CardParser : Node2D
{
	public static Dictionary<string, CardData> cards = new();
	public static Dictionary<string, List<PrintingData>> printings = new();

	public async void parseJSON(string default_cards_url)
	{
		using System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
		string default_cards = await client.GetStringAsync(default_cards_url);

		GD.Print("parseJSON started");
		using JsonDocument doc = JsonDocument.Parse(default_cards);
		JsonElement root = doc.RootElement;
		foreach (JsonElement card in root.EnumerateArray())
		{
			// refuse non-land cards from unsets
			string set = card.GetProperty("set").GetString();
			if (set is "ugl" or "unh" or "ust" or "unf" or "und")
			{
				string type = card.GetProperty("type_line").GetString();
				if (!type.Contains("Land")) continue;
			}

			PrintingData newPrinting = new PrintingData();
			newPrinting.oracle_id = GetStringOrNull(card, "oracle_id");

			if (card.TryGetProperty("image_uris", out JsonElement image_uris))
			{
				newPrinting.large_uri = GetStringOrNull(image_uris, "large");
				newPrinting.small_uri = GetStringOrNull(image_uris, "small");
			}
			else
			{
				newPrinting.large_uri = null;
				newPrinting.small_uri = null;
			}
			newPrinting.set = card.GetProperty("set").GetString();
			newPrinting.set_id = card.GetProperty("set_id").GetString();

			if (card.GetProperty("string").GetString() == "reversible_card") continue;

			if (!cards.ContainsKey(card.GetProperty("oracle_id").GetString()))
			{
				CardData newCard = new CardData();
				newCard.oracle_id = GetStringOrNull(card, "oracle_id");
				newCard.name = card.GetProperty("name").GetString();
				newCard.layout = card.GetProperty("layout").GetString();
				newCard.card_faces =
					card.TryGetProperty("card_faces", out JsonElement faces)
						? JsonSerializer.Deserialize<CardFaceData[]>(faces.GetRawText())
						: null;
				newCard.cmc = card.GetProperty("cmc").GetDouble();
				newCard.colour_identity = GetStringArrayOrNull(card, "color_identity");
				newCard.colour_indicator = GetStringArrayOrNull(card, "color_indicator");
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
				newCard.oracle_text = GetStringOrNull(card, "oracle_text");
				newCard.power = GetStringOrNull(card, "power");
				newCard.toughness = GetStringOrNull(card, "thoughness");
				newCard.produced_mana = GetStringArrayOrNull(card, "produced_mana");
				newCard.type_line = card.GetProperty("type_line").GetString();
				cards[newCard.oracle_id] = newCard;

				printings[newPrinting.oracle_id] = new List<PrintingData>();
			}
			printings[newPrinting.oracle_id].Add(newPrinting);
		}
		using var cardsFile = FileAccess.Open("user://cards.json", FileAccess.ModeFlags.ReadWrite);
		cardsFile.StoreString(JsonSerializer.Serialize(cards));

		using var printingsFile = FileAccess.Open("user://printings.json", FileAccess.ModeFlags.ReadWrite);
		printingsFile.StoreString(JsonSerializer.Serialize(printings));
		GD.Print("parseJSON done");
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
