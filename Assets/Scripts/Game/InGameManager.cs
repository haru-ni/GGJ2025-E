using Cysharp.Threading.Tasks;
using UnityEngine;
using Utils;

namespace Game
{
    public class InGameManager : Singleton<InGameManager>
    {
        [SerializeField] private CardManager cardManager;
        [SerializeField] private StageManager stageManager;
        [SerializeField] private Player player;

        private bool _inGameEnd;
        private bool _cardPlayed;
        private bool _enemyBehaviourEnd;

        protected override void Awake()
        {
            base.Awake();
            StartInGame().Forget();
        }

        public async UniTask StartInGame()
        {
            cardManager.Setup();
            stageManager.Setup();
            while (!_inGameEnd)
            {
                await OneTurnRoutine();
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
            _cardPlayed = false;
            await UniTask.Yield();
            await UniTask.WaitUntil(() => _cardPlayed);
        }

        public async UniTask EnemyTurn()
        {
            Debug.Log($"EnemyTurn");
            _enemyBehaviourEnd = false;
            await UniTask.Yield();
            await UniTask.WaitUntil(() => _cardPlayed);
        }

        public void PlayCard(HandCard handCard)
        {
            player.AddBubble(handCard.cardDefinition.bubbleData);
            SoundManager.Instance.PlaySe(GameConst.SeType.Decision3);
            handCard.SetEnable(false);
            _cardPlayed = true;
        }

        public async UniTask EnemyBehaviour()
        {
            // TODO 敵の行動
            _enemyBehaviourEnd = true;
        }
    }
}