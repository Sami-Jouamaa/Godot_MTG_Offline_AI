using System;
using Microsoft.VisualBasic;

public class CardData
{
    public string oracle_id {get; set;}
    public string name {get; set;}
    public string layout {get; set;}
    public Array card_faces {get; set;}
    public float cmc {get; set;}
    public string colour_identity {get; set;}
    public string colour_indicator {get; set;}
    public string colours {get; set;}
}
// Core fields
// layout 	String 		A code for this card’s layout.
// oracle_id 	UUID 	Nullable 	A unique ID for this card’s oracle identity. This value is consistent across reprinted card editions, and unique among different cards with the same name (tokens, Unstable variants, etc). Always present except for the reversible_card layout where it will be absent; oracle_id will be found on each face instead.


// Gameplay Fields

// card_faces 	Array 	Nullable 	An array of Card Face objects, if this card is multifaced.
// cmc 	Decimal 		The card’s mana value. Note that some funny cards have fractional mana costs.
// color_identity 	Colors 		This card’s color identity.
// color_indicator 	Colors 	Nullable 	The colors in this card’s color indicator, if any. A null value for this field indicates the card does not have one.
// colors 	Colors 	Nullable 	This card’s colors, if the overall card has colors defined by the rules. Otherwise the colors will be on the card_faces objects, see below.
// defense 	String 	Nullable 	This face’s defense, if any.
// hand_modifier 	String 	Nullable 	This card’s hand modifier, if it is Vanguard card. This value will contain a delta, such as -1.
// keywords 	Array 		An array of keywords that this card uses, such as 'Flying' and 'Cumulative upkeep'.
// life_modifier 	String 	Nullable 	This card’s life modifier, if it is Vanguard card. This value will contain a delta, such as +2.
// loyalty 	String 	Nullable 	This loyalty if any. Note that some cards have loyalties that are not numeric, such as X.
// mana_cost 	String 	Nullable 	The mana cost for this card. This value will be any empty string "" if the cost is absent. Remember that per the game rules, a missing mana cost and a mana cost of {0} are different values. Multi-faced cards will report this value in card faces.
// name 	String 		The name of this card. If this card has multiple faces, this field will contain both names separated by ␣//␣.
// oracle_text 	String 	Nullable 	The Oracle text for this card, if any.
// power 	String 	Nullable 	This card’s power, if any. Note that some cards have powers that are not numeric, such as *.
// produced_mana 	Colors 	Nullable 	Colors of mana that this card could produce.
// reserved 	Boolean 		True if this card is on the Reserved List.
// toughness 	String 	Nullable 	This card’s toughness, if any. Note that some cards have toughnesses that are not numeric, such as *.
// type_line 	String 		The type line of this card.


// Print Fields

// image_uris 	Object 	Nullable 	An object listing available imagery for this card. See the Card Imagery article for more information.
// set_ 	String 		This card’s abreviation

// to exclude some sets (the joke ones)
// set_id 	UUID


// Card Face Objects from the card_faces attribute if applicable

// cmc 	Decimal 	Nullable 	The mana value of this particular face, if the card is reversible.
// color_indicator 	Colors 	Nullable 	The colors in this face’s color indicator, if any.
// colors 	Colors 	Nullable 	This face’s colors, if the game defines colors for the individual face of this card.
// defense 	String 	Nullable 	This face’s defense, if any.
// image_uris 	Object 	Nullable 	An object providing URIs to imagery for this face, if this is a double-sided card. If this card is not double-sided, then the image_uris property will be part of the parent object instead.
// layout 	String 	Nullable 	The layout of this card face, if the card is reversible.
// loyalty 	String 	Nullable 	This face’s loyalty, if any.
// mana_cost 	String 		The mana cost for this face. This value will be any empty string "" if the cost is absent. Remember that per the game rules, a missing mana cost and a mana cost of {0} are different values.
// name 	String 		The name of this particular face.
// oracle_id 	UUID 	Nullable 	The Oracle ID of this particular face, if the card is reversible.
// oracle_text 	String 	Nullable 	The Oracle text for this face, if any.
// power 	String 	Nullable 	This face’s power, if any. Note that some cards have powers that are not numeric, such as *.
// toughness 	String 	Nullable 	This face’s toughness, if any.
// type_line 	String 	Nullable 	The type line of this particular face, if the card is reversible.
