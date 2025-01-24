using ScriptableObjects.Enemy;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

            enemyImage.sprite = _enemyDefinition.sprite;
            hpText.SetText($"{_currentHp}/{_enemyDefinition.hp}");

            foreach (var element in _enemyDefinition.BehaviourList)
            {
                var behavior = Instantiate(behaviorDisplay, behaviourContainer);
                behavior.Setup(element.bubbleData);
            }
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