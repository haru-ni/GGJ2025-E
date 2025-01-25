using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Utils;
using Behaviour = ScriptableObjects.Enemy.Behaviour;

namespace Game
{
    public class InGameManager : Singleton<InGameManager>
    {
        private const float TurnIntervalTime = 0.3f;

        [SerializeField] private CardManager cardManager;
        [SerializeField] private StageManager stageManager;
        [SerializeField] private Player player;

        private bool _inGameEnd;
        private int _turnIndex;
        private int Turn => _turnIndex + 1;
        private bool _isPlayerTurn;
        private bool _cardPlayed;
        private bool _enemyBehaviourEnd;

        protected override void Awake()
        {
            base.Awake();
            StartInGame().Forget();
        }

        public async UniTask StartInGame()
        {
            SoundManager.Instance.PlayBgm(GameConst.BgmType.NormalBattle);
            cardManager.Setup();
            stageManager.Setup();
            while (!_inGameEnd)
            {
                await OneTurnRoutine();
                _turnIndex++;
            }
        }

        public async UniTask OneTurnRoutine()
        {
            await PlayerTurn();
            await EnemyTurn();
        }

        public async UniTask PlayerTurn()
        {
            Debug.Log($"PlayerTurn");
            _isPlayerTurn = true;
            _cardPlayed = false;
            await UniTask.Yield();
            await UniTask.WaitUntil(() => _cardPlayed);
            await UniTask.WaitForSeconds(TurnIntervalTime);
        }

        public void PlayCard(HandCard handCard)
        {
            if (!_isPlayerTurn || _cardPlayed) return;
            _cardPlayed = true;
            player.AddBubble(handCard.cardDefinition.bubbleData);
            Debug.Log($"泡を張った：{handCard.cardDefinition.name}");
            SoundManager.Instance.PlaySe(GameConst.SeType.Decision3);
            handCard.SetEnable(false);
        }

        public async UniTask EnemyTurn()
        {
            Debug.Log($"EnemyTurn");
            _enemyBehaviourEnd = false;
            await UniTask.Yield();
            EnemyBehaviour().Forget();
            await UniTask.WaitUntil(() => _enemyBehaviourEnd);
            await UniTask.WaitForSeconds(TurnIntervalTime);
        }

        public async UniTask EnemyBehaviour()
        {
            var enemyBehaviours = stageManager.GetEnemyBehaviour(_turnIndex);
            var enemyBehaviour = ValidateEnemyBehaviour(enemyBehaviours);
            Debug.Log(
                $"敵の攻撃：色={enemyBehaviour.bubbleData.color},強度={enemyBehaviour.bubbleData.power},枚数={enemyBehaviour.numberOfDestroy}");
            EnemyAttack(enemyBehaviour);

            _enemyBehaviourEnd = true;
        }

        public void EnemyAttack(Behaviour behaviour)
        {
            // 破壊枚数と同じ数の泡を同時に参照
            var referenceBubbles = player.GetBubbleData(behaviour.numberOfDestroy);

            // 壊れる泡があれば、その外側は全て破壊される
            var breakCount = (from bubble in referenceBubbles.Select((element, index) => (element, index))
                let isBreak = TryBreakBubble(bubble.element.GetBubbleData(), behaviour.bubbleData)
                where isBreak
                select referenceBubbles.Count - bubble.index).FirstOrDefault();

            if (breakCount == 0)
            {
                Debug.Log($"守った");
                return;
            }

            player.RemoveBubble(breakCount);

            Debug.Log($"泡が{breakCount}枚割られた");
            var targetEnemy = stageManager.GetEnemies().FirstOrDefault();
            if (targetEnemy == null)
            {
                Debug.LogError($"対象となる敵がいない！");
                return;
            }
            targetEnemy.DecrementHp();
        }

        private bool TryBreakBubble(BubbleData shield, BubbleData attack)
        {
            Debug.Log($"泡の強度{shield.power},泡の色{shield.color},攻撃の強度{attack.power},攻撃の色{attack.color}");
            // 色が違うと問答無用で割れる
            if (attack.color != shield.color)
            {
                Debug.Log($"色が違うから割れる");
                return true;
            }

            // 色が合ってても強度が足りないと割れる
            if (attack.color == shield.color && attack.power > shield.power)
            {
                Debug.Log($"強度が足りないから割れる");
                return true;
            }

            // それ以外は割れない
            Debug.Log($"割れなかった");
            return false;
        }

        public Behaviour ValidateEnemyBehaviour(List<Behaviour> enemyBehaviours)
        {
            // 攻撃情報が一つに統合されるまで繰り返す
            // while (enemyBehaviours.Count > 1)
            // {
            //     var merged = false;
            //     var behaviourCount = enemyBehaviours.Count;
            //     for (var i = 0; i < behaviourCount - 1 && !merged; i++)
            //     {
            //         for (var j = i + 1; j < behaviourCount && !merged; j++)
            //         {
            //             var result = TryMergeBehaviour(enemyBehaviours[i], enemyBehaviours[j]);
            //             if (!result.merged) continue;
            //             merged = true;
            //             enemyBehaviours.Remove(enemyBehaviours[i]);
            //             enemyBehaviours.Remove(enemyBehaviours[j]);
            //             enemyBehaviours.Add(result.behaviour);
            //         }
            //     }
            // }

            // TODO あとでもっとよくする
            var behaviourCount = enemyBehaviours.Count;
            for (var i = 0; i < behaviourCount - 1; i++)
            {
                for (var j = i + 1; j < behaviourCount; j++)
                {
                    var result = TryMergeBehaviour(enemyBehaviours[i], enemyBehaviours[j]);
                    if (!result.merged) continue;
                    enemyBehaviours.Remove(enemyBehaviours[i]);
                    enemyBehaviours.Remove(enemyBehaviours[j]);
                    enemyBehaviours.Add(result.behaviour);
                }
            }

            return enemyBehaviours.FirstOrDefault();
        }

        private (bool merged, Behaviour behaviour) TryMergeBehaviour(Behaviour a, Behaviour b)
        {
            var (canMixColor, mixColorType) = TryMixColor(new List<GameConst.ColorType>
                { a.bubbleData.color, b.bubbleData.color });

            // 色が同じで枚数が両方1 → 強度を足して統合
            if (a.bubbleData.color == b.bubbleData.color && a.numberOfDestroy == 1 && b.numberOfDestroy == 1)
            {
                return (true, new Behaviour
                {
                    bubbleData = new BubbleData
                    {
                        color = a.bubbleData.color,
                        power = a.bubbleData.power + b.bubbleData.power,
                    },
                    numberOfDestroy = 1
                });
            }

            // 色が同じで枚数が両方2以上 → 枚数を足して統合
            if (a.bubbleData.color == b.bubbleData.color && a.numberOfDestroy > 1 && b.numberOfDestroy > 1)
            {
                return (true, new Behaviour
                {
                    bubbleData = new BubbleData
                    {
                        color = a.bubbleData.color,
                        power = 1,
                    },
                    numberOfDestroy = a.numberOfDestroy + b.numberOfDestroy
                });
            }

            // 特殊な組み合わせ（例：青+赤=紫）で強度が同じで枚数が両方1 → 強い方の強度と枚数を採用して色を混ぜる
            if (canMixColor && a.bubbleData.power == b.bubbleData.power && a.numberOfDestroy == 1 &&
                b.numberOfDestroy == 1)
            {
                return (true, new Behaviour
                {
                    bubbleData = new BubbleData
                    {
                        color = mixColorType,
                        power = Math.Max(a.bubbleData.power, b.bubbleData.power),
                    },
                    numberOfDestroy = Math.Max(a.numberOfDestroy, b.numberOfDestroy)
                });
            }

            // 特殊な組み合わせ（例：青+赤=紫）で枚数が両方2以上 → 枚数を足して色を混ぜる
            if (canMixColor && a.numberOfDestroy > 1 && b.numberOfDestroy > 1)
            {
                return (true, new Behaviour
                {
                    bubbleData = new BubbleData
                    {
                        color = mixColorType,
                        power = 1,
                    },
                    numberOfDestroy = a.numberOfDestroy + b.numberOfDestroy
                });
            }

            return (false, null);
        }

        private (bool result, GameConst.ColorType colorType) TryMixColor(List<GameConst.ColorType> colors)
        {
            // ソートしてから一致しているか比較
            var sortedMaterialList = colors.OrderBy(e => e).ToList();
            colors.Sort();
            foreach (var element in from element in GameConst.MixColorDictionary
                     let sortedRecipeList = element.Key.OrderBy(e => e)
                     where sortedMaterialList.SequenceEqual(sortedRecipeList)
                     select element)
            {
                return (true, element.Value);
            }

            return (false, new GameConst.ColorType());
        }
    }
}