using ScriptableObjects.Card;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class Card : MonoBehaviour
    {
        [SerializeField] protected Image cardImage;
        [SerializeField] protected TextMeshProUGUI powerText;
        public RectTransform RectTransform => transform as RectTransform;

        protected CardDefinition CardDefinition;

        public virtual void Setup(CardDefinition cardDefinition)
        {
            CardDefinition = cardDefinition;
            cardImage.sprite = cardDefinition.sprite;
            cardImage.color = cardDefinition.bubbleData.GetColor();
            powerText.SetText($"{cardDefinition.bubbleData.power}");
        }

        public CardDefinition GetCardModel()
        {
            return CardDefinition;
        }
    }
}