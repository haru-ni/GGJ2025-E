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
            NormalBattle,
        }

        public static readonly Dictionary<BgmType, string> BgmPathDictionary = new()
        {
            { BgmType.SunnySpot, "Sound/BGM/bo-tto hidamari" },
            { BgmType.NormalBattle, "Sound/BGM/normalBattleBGM" },
        };

        public enum CardType
        {
        }

        public enum ColorType
        {
            Pink,
            Green,
            Bicolor,
        }

        public static readonly Dictionary<List<ColorType>, ColorType> MixColorDictionary = new()
        {
            { new List<ColorType> { ColorType.Pink, ColorType.Green }, ColorType.Bicolor },
        };

        public const int PlayerBubbleLimit = 5;
        public const int HandCardCount = 5;
    }
}