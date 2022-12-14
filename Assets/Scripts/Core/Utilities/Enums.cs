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
        None,
        Shuffle,
        Straight,
        SameKind,
        Smart
    }
    
    public enum ErrorCode
    {
        None,
        NoDataReceiveFromStraightRequest,
        NoDataReceiveFromSameKindRequest,
        NoDataReceiveFromSmartRequest
    }
}