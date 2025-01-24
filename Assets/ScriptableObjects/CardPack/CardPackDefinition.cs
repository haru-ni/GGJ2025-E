using System.Collections.Generic;
using ScriptableObjects.Card;
using UnityEngine;

namespace ScriptableObjects.CardPack
{
    [CreateAssetMenu(fileName = "CardPackData", menuName = "ScriptableObjects/CardPackData")]
    public class CardPackDefinition : ScriptableObject
    {
        public string packname;
        public Sprite sprite;
        public List<CardDefinition> cards;
    }
}