using UnityEngine;
using Utils;

namespace ScriptableObjects.Card
{
    [CreateAssetMenu(fileName = "CardData", menuName = "ScriptableObjects/CardData")]
    public class CardDefinition : ScriptableObject
    {
        public string cardName;
        public Sprite sprite;
        public int power;
        public GameConst.ColorType color;
    }
}