using System;
#nullable enable
public class CardData
{
    public string? oracle_id { get; set; }
    public string name { get; set; } = "";
    public string layout { get; set; } = "";
    public CardFaceData[]? card_faces { get; set; }
    public double cmc { get; set; } = 0.0;
    public string[]? colour_identity { get; set; }
    public string[]? colour_indicator { get; set; }
    public string[]? colours { get; set; }
    public string? defense { get; set; }
    public string? hand_modifier { get; set; }
    public string[] keywords { get; set; } = Array.Empty<string>();
    public string? life_modifier { get; set; }
    public string? loyalty { get; set; }
    public string? mana_cost { get; set; }
    public string? oracle_text { get; set; }
    public string? power { get; set; }
    public string? toughness { get; set; }
    public string[]? produced_mana { get; set; }
    public string type_line { get; set; } = "";
}