using System.Collections.Generic;

namespace Utils
{
    public static class GameConst
    {
        #region Config

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

        #endregion

        #region SE

        public enum SeType
        {
            Cancel2,
            Cancel6,
            Cancel8,
            Decision2,
            Decision3,
            Decision12,
            Decision22,
            Decision24,
            Decision31,
            Decision33,
            Decision34,
            Decision42,
            Decision44,
            Decision50,
            Move10,
            Pop,
            VoiceCombatStart1,
            VoiceCombatStart2,
            VoiceDamage,
            VoiceGreeting,
            VoiceLose,
            VoiceWin,
        }

        public static readonly Dictionary<SeType, string> SePathDictionary = new()
        {
            { SeType.Cancel2, "Sound/SE/Cancel2" },
            { SeType.Cancel6, "Sound/SE/Cancel6" },
            { SeType.Cancel8, "Sound/SE/Cancel8" },
            { SeType.Decision2, "Sound/SE/Decision2" },
            { SeType.Decision3, "Sound/SE/Decision3" },
            { SeType.Decision12, "Sound/SE/Decision12" },
            { SeType.Decision22, "Sound/SE/Decision22" },
            { SeType.Decision24, "Sound/SE/Decision24" },
            { SeType.Decision31, "Sound/SE/Decision31" },
            { SeType.Decision33, "Sound/SE/Decision33" },
            { SeType.Decision34, "Sound/SE/Decision34" },
            { SeType.Decision42, "Sound/SE/Decision42" },
            { SeType.Decision44, "Sound/SE/Decision44" },
            { SeType.Decision50, "Sound/SE/Decision50" },
            { SeType.Move10, "Sound/SE/Move10" },
            { SeType.Pop, "Sound/SE/Pop" },
            { SeType.VoiceCombatStart1, "Sound/SE/Pop" },
            { SeType.VoiceCombatStart2, "Sound/SE/Pop" },
            { SeType.VoiceDamage, "Sound/SE/Pop" },
            { SeType.VoiceGreeting, "Sound/SE/Pop" },
            { SeType.VoiceLose, "Sound/SE/Pop" },
            { SeType.VoiceWin, "Sound/SE/Pop" },
        };

        #endregion

        #region BGM

        public enum BgmType
        {
            PixelLoveInTokyo,
            NormalBattle,
            Ending,
        }

        public static readonly Dictionary<BgmType, string> BgmPathDictionary = new()
        {
            { BgmType.PixelLoveInTokyo, "Sound/BGM/PixelLoveInTokyo" },
            { BgmType.NormalBattle, "Sound/BGM/NormalBattle" },
            { BgmType.Ending, "Sound/BGM/Ending" },
        };

        #endregion

        #region Spine

        public enum BattleEffect
        {
            EnemyHit,
            Hit,
            ShieldBurst,
        }

        public static readonly Dictionary<BattleEffect, string> BattleEffectDictionary = new()
        {
            { BattleEffect.EnemyHit, "enemy_hit" },
            { BattleEffect.Hit, "hit" },
            { BattleEffect.ShieldBurst, "shield_burst" },
        };

        public static readonly Dictionary<BattleEffect, float> BattleEffectTime = new()
        {
            { BattleEffect.EnemyHit, 2.0f },
            { BattleEffect.Hit, 0.5f },
            { BattleEffect.ShieldBurst, 1.0f },
        };

        public enum PlayerShield
        {
            Shield1,
            Shield2,
            Shield3,
            Shield4,
            Shield5,
        }

        public static readonly Dictionary<PlayerShield, string> PlayerShieldDictionary = new()
        {
            { PlayerShield.Shield1, "shield_1" },
            { PlayerShield.Shield2, "shield_2" },
            { PlayerShield.Shield3, "shield_3" },
            { PlayerShield.Shield4, "shield_4" },
            { PlayerShield.Shield5, "shield_5" },
        };

        public static readonly Dictionary<PlayerShield, float> PlayerShieldTime = new()
        {
            { PlayerShield.Shield1, 3.5f },
            { PlayerShield.Shield2, 3.5f },
            { PlayerShield.Shield3, 3.5f },
            { PlayerShield.Shield4, 3.5f },
            { PlayerShield.Shield5, 3.5f },
        };

        #endregion
    }
}