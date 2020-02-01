using System;

namespace _Scripts.Factions
{
    public enum Faction
    {
        Humans,
        Nature
    }

    public static class FactionExtensions
    {
        public static Faction Invert(this Faction faction)
        {
            switch (faction)
            {
                case Faction.Humans: return Faction.Nature;
                case Faction.Nature: return Faction.Humans;
                default: throw new ArgumentOutOfRangeException(nameof(faction), faction, null);
            }
        }
    }
}