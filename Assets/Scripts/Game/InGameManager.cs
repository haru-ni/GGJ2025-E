using ScriptableObjects.Card;
using UnityEngine;
using Utils;

namespace Game
{
    public class InGameManager : Singleton<InGameManager>
    {
        [SerializeField] private CardManager cardManager;
        [SerializeField] private StageManager stageManager;
        [SerializeField] private Player player;


        public void PlayerTurn()
        {
        }

        public void EnemyTurn()
        {
            
        }

        public void PlayCard(HandCard handCard)
        {
            player.AddBubble(handCard.cardDefinition.bubbleData);
            SoundManager.Instance.PlaySe(GameConst.SeType.Decision3);
            handCard.SetEnable(false);
        }
    }
}