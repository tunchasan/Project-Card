namespace ProjectCard.Core.Utilities
{
    public enum Kind
    {
        Ace = 0,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
    }
    
    public enum Suit
    {
        Spade = 0,
        Diamond,
        Heart,
        Club,
    }

    public enum SortType
    {
        Shuffle,
        Straight,
        SameKind,
        Smart
    }
}