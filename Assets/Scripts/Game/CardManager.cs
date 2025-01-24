using System.Collections.Generic;
using ScriptableObjects.CardPack;
using UnityEngine;
using Utils;

namespace Game
{
    public class CardManager : Singleton<CardManager>
    {
        [SerializeField] private CardPackDefinition initialDeck; 
        [SerializeField] private List<CardPackDefinition> cardPackDefinitions = new();
    }
}
