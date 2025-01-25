using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Behaviour = ScriptableObjects.Enemy.Behaviour;

namespace Game
{
    public class AttackInformation : MonoBehaviour
    {
        [SerializeField] private Image sequentialOrderImage;
        [SerializeField] private List<Sprite> orderSprites;

        [SerializeField] private RectTransform informationContainer;
        [SerializeField] private Image starPrefab;
        [SerializeField] private List<Sprite> startSprites = new();

        public void Setup(int order, Behaviour behaviour)
        {
            sequentialOrderImage.sprite = orderSprites[order];
            sequentialOrderImage.SetNativeSize();

            for (var i = 0; i < behaviour.bubbleData.power; i++)
            {
                var star = Instantiate(starPrefab, informationContainer);
                star.sprite = startSprites[(int)behaviour.bubbleData.color];
            }
        }
    }
}