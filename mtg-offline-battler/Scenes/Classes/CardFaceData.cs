#nullable enable
public class CardFaceData
{
    public string oracle_id { get; set; } = "";
    public float? cmc { get; set; }
    public string? colour_indicator { get; set; }
    public string[]? colours { get; set; }
    public string? defense { get; set; }
    public string? layout { get; set; }
    public string? loyalty { get; set; }
    public string mana_cost { get; set; } = "";
    public string name { get; set; } = "";
    public string oracle_text { get; set; } = "";
    public string? power { get; set; }
    public string? toughness { get; set; }
    public string? type_line { get; set; }
}