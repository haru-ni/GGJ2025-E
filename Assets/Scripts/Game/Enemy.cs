using System;
using System.Collections.Generic;
using System.Linq;
using ScriptableObjects.Enemy;
using UnityEngine;
using UnityEngine.UI;
using Behaviour = ScriptableObjects.Enemy.Behaviour;

namespace Game
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Image enemyImage;
        [SerializeField] private Image hpPrefab;
        [SerializeField] private RectTransform hpContainer;
        [SerializeField] private AttackInformation attackInformation;
        [SerializeField] private RectTransform attackInformationContainer;

        private EnemyDefinition _enemyDefinition;
        private readonly List<Image> _hpImageList = new();
        private int _currentHp;

        public void Setup(EnemyDefinition enemyDefinition)
        {
            _enemyDefinition = enemyDefinition;
            _currentHp = _enemyDefinition.hp;

            UpdateHpDisplay();
            enemyImage.sprite = _enemyDefinition.sprite;
            enemyImage.SetNativeSize();

            foreach (var v in _enemyDefinition.BehaviourList.Select((element, index) => (element, index)))
            {
                var behavior = Instantiate(attackInformation, attackInformationContainer);
                behavior.Setup(v.index, v.element);
            }
        }

        public void SetOnDeathAction(Action<Enemy> action)
        {
            _onDeath = action;
        }

        private void UpdateHpDisplay()
        {
            while (_hpImageList.Count != _currentHp)
            {
                var currentCount = _hpImageList.Count;

                // 目標の個数に応じてオブジェクトを生成または削除
                if (currentCount < _currentHp)
                {
                    // オブジェクトを生成
                    var hpImage = Instantiate(hpPrefab, hpContainer);
                    _hpImageList.Add(hpImage);
                }
                else if (currentCount > _currentHp)
                {
                    // オブジェクトを削除
                    var objectToRemove = _hpImageList[currentCount - 1];
                    _hpImageList.RemoveAt(currentCount - 1);
                    Destroy(objectToRemove);
                }
            }
        }

        public Behaviour GetEnemyBehaviour(int turnIndex)
        {
            return _enemyDefinition.BehaviourList[turnIndex % _enemyDefinition.BehaviourList.Count];
        }

        public void DecrementHp(int damage = 1)
        {
            _currentHp -= damage;
            UpdateHpDisplay();
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