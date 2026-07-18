using System;
// the way target(s) is stored will probably be changed depending on how targeting cards and players will work

public enum CounterType
{
    zero_one,
    zero_two,
    zero_three,
    zero_four,
    zero_five,
    one_zero,
    one_one,
    one_two,
    one_three,
    one_four,
    one_five,
    two_zero,
    two_one,
    two_two,
    two_three,
    two_four,
    two_five,
    Flying,
    Indestructible,
    Lifelink,
    Radiation,
    Haste
    // to continue
}

public enum Triggers
{
    Creature_Entered,
    Enchantment_Entered,
    Artifact_Enterered,

}

public class DrawCards
{
    public int DrawAmount = 0;
    public string Target = "";
}

public class DiscardCards
{
    public int DiscardAmount = 0;
    public string Target = "";
}

public class GainOrLoseLife
{
    // can be negative
    public int LifeAmount = 0;
    public string Target = "";
}

public class Counters
{
    public CounterType TypeOfCounter = 0;
    public int CountersAmount = 0;
    public int NbCreatures = 0;
    public string Target = "";
}

