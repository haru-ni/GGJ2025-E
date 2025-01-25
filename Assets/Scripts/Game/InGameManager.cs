using Cysharp.Threading.Tasks;
using UnityEngine;
using Utils;

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

            _enemyBehaviourEnd = true;
        }
    }
}