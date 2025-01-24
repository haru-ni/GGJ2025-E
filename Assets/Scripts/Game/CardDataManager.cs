using System.Collections.Generic;
using ScriptableObjects.Card;
using UnityEngine;

namespace Game
{
    public class CardDataManager : MonoBehaviour
    {
        [SerializeField] private List<CardDataDefinition> cardDataDefinitions = new();
        
        public List<CardDataDefinition> CardDataDefinitions => cardDataDefinitions;
    }
}
