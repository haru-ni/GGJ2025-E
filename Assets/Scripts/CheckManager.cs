using Spine.Unity;
using UnityEngine;
using Utils;

public class CheckManager : Singleton<CheckManager>
{
    [SerializeField] private SkeletonGraphic battleEffectSpine;
    [SerializeField] private SkeletonGraphic playerShieldSpine;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            battleEffectSpine.AnimationState.SetAnimation(0,
                GameConst.BattleEffectDictionary[GameConst.BattleEffect.EnemyHit], false);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            battleEffectSpine.AnimationState.SetAnimation(0,
                GameConst.BattleEffectDictionary[GameConst.BattleEffect.Hit], false);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            battleEffectSpine.AnimationState.SetAnimation(0,
                GameConst.BattleEffectDictionary[GameConst.BattleEffect.ShieldBurst], false);
        }


        if (Input.GetKeyDown(KeyCode.A))
        {
            playerShieldSpine.AnimationState.SetAnimation(0,
                GameConst.PlayerShieldDictionary[GameConst.PlayerShield.Shield1], false);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            playerShieldSpine.AnimationState.SetAnimation(0,
                GameConst.PlayerShieldDictionary[GameConst.PlayerShield.Shield2], false);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            playerShieldSpine.AnimationState.SetAnimation(0,
                GameConst.PlayerShieldDictionary[GameConst.PlayerShield.Shield3], false);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            playerShieldSpine.AnimationState.SetAnimation(0,
                GameConst.PlayerShieldDictionary[GameConst.PlayerShield.Shield4], false);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            playerShieldSpine.AnimationState.SetAnimation(0,
                GameConst.PlayerShieldDictionary[GameConst.PlayerShield.Shield5], false);
        }
    }
}