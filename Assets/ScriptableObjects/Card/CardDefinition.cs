using Game;
using UnityEngine;

namespace ScriptableObjects.Card
{
    [CreateAssetMenu(fileName = "CardData", menuName = "ScriptableObjects/CardData")]
    public class CardDefinition : ScriptableObject
    {
        public string cardName;
        public Sprite sprite;
        public BubbleData bubbleData;
    }
}