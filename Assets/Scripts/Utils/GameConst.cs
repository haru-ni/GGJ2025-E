using System.Collections.Generic;

namespace Utils
{
    public static class GameConst
    {
        public enum SeType
        {
            Decision3,
        }

        public static readonly Dictionary<SeType, string> SePathDictionary = new()
        {
            { SeType.Decision3, "Sound/SE/Decision3" },
        };

        public enum BgmType
        {
            SunnySpot,
        }

        public static readonly Dictionary<BgmType, string> BgmPathDictionary = new()
        {
            { BgmType.SunnySpot, "Sound/BGM/bo-tto hidamari" },
        };

        public enum CardType
        {
        }

        public enum ColorType
        {
            Red,
            Blue,
            Purple,
        }

        public static Dictionary<List<ColorType>, ColorType> MixColorDictionary = new()
        {
            { new List<ColorType> { ColorType.Red, ColorType.Blue }, ColorType.Purple },
        };

        public const int PlayerBubbleLimit = 5;
        public const int HandCardCount = 5;
    }
}