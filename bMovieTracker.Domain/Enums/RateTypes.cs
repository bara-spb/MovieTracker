using System.Runtime.Serialization;

namespace bMovieTracker.Domain
{
    public enum RateTypes
    {
        [EnumMember(Value = "One Star")]
        OneStar,
        [EnumMember(Value = "Two Stars")]
        TwoStars,
        [EnumMember(Value = "Three Stars")]
        ThreeStars,
        [EnumMember(Value = "Four Stars")]
        FourStars,
        [EnumMember(Value = "Five Stars")]
        FiveStars
    }
}
