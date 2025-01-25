using ScriptableObjects.Enemy;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
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
            this._enemyDefinition = enemyDefinition;
            _currentHp = this._enemyDefinition.hp;

            enemyImage.sprite = this._enemyDefinition.sprite;
            hpText.SetText($"{_currentHp}/{this._enemyDefinition.hp}");

            foreach (var element in this._enemyDefinition.BehaviourList)
            {
                var behavior = Instantiate(behaviorDisplay, behaviourContainer);
                behavior.Setup(element.bubbleData);
            }
        }

        public Behaviour GetEnemyBehaviour(int turnIndex)
        {
            return _enemyDefinition.BehaviourList[turnIndex % _enemyDefinition.BehaviourList.Count];
        }

        [SerializeField] private EnemyDefinition temp;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                Setup(temp);
            }
        }
    }
}