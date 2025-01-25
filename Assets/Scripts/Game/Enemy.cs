using System;
using ScriptableObjects.Enemy;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Behaviour = ScriptableObjects.Enemy.Behaviour;

namespace Game
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Image enemyImage;
        [SerializeField] private TextMeshProUGUI hpText;
        [SerializeField] private BehaviorDisplay behaviorDisplay;
        [SerializeField] private RectTransform behaviourContainer;

        private EnemyDefinition _enemyDefinition;
        private int _currentHp;

        public void Setup(EnemyDefinition enemyDefinition)
        {
            _enemyDefinition = enemyDefinition;
            _currentHp = _enemyDefinition.hp;

            UpdateHpText();
            enemyImage.sprite = _enemyDefinition.sprite;
            enemyImage.SetNativeSize();

            foreach (var element in _enemyDefinition.BehaviourList)
            {
                var behavior = Instantiate(behaviorDisplay, behaviourContainer);
                behavior.Setup(element.bubbleData);
            }
        }

        public void SetOnDeathAction(Action<Enemy> action)
        {
            _onDeath = action;
        }

        private void UpdateHpText()
        {
            hpText.SetText($"{_currentHp}/{_enemyDefinition.hp}");
        }

        public Behaviour GetEnemyBehaviour(int turnIndex)
        {
            return _enemyDefinition.BehaviourList[turnIndex % _enemyDefinition.BehaviourList.Count];
        }

        public void DecrementHp(int damage = 1)
        {
            _currentHp -= damage;
            UpdateHpText();
            if (_currentHp > 0) return;
            Dead();
        }

        private Action<Enemy> _onDeath;
        public void Dead()
        {
            DestroyImmediate(gameObject);
            
        }
    }
}