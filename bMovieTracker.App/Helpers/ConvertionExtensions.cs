//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace bMovieTracker.App.Helpers
//{
//    public static class ConvertionExtensions
//    {
//        public static TEnum? GetNullableEnum<TEnum>(this object value)
//            where TEnum: System.Enum
//        {
//            if (value == null)
//                return null;
//            var enumValue = value.ToString();
//            Enum.IsDefined(TEnum, enumValue) && !string.IsNullOrEmpty(value) && value.All(Char.IsDigit) ? (GenreTypes?)int.Parse(v) : null)
//            return null;
//        }
//    }
//}
