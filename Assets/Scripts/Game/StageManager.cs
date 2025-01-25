using System.Collections.Generic;
using System.Linq;
using ScriptableObjects.Enemy;
using ScriptableObjects.Stage;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using Behaviour = ScriptableObjects.Enemy.Behaviour;

namespace Game
{
    public class StageManager : Singleton<StageManager>
    {
        [SerializeField] private List<StageDefinition> stageDefinitions = new();
        [SerializeField] private Enemy enemyPrefab;
        [SerializeField] private RectTransform enemyArea;
        [SerializeField] private Image backGroundImage;

        private readonly List<Enemy> _enemyList = new();

        public void Setup()
        {
            // TODO 適切なステージインデックスを読み込むようにする
            const int stageIndex = 1;
            StageSetup(stageDefinitions[stageIndex % stageDefinitions.Count]);
        }

        public void StageSetup(StageDefinition stageDefinition)
        {
            backGroundImage.sprite = stageDefinition.backGroundSprite;
            // backGroundImage.SetNativeSize();
            foreach (var element in stageDefinition.enemyList)
            {
                CreateEnemy(element);
            }
        }

        public void CreateEnemy(EnemyDefinition enemyDefinition)
        {
            var enemy = Instantiate(enemyPrefab, enemyArea);
            enemy.Setup(enemyDefinition);
            enemy.SetOnDeathAction(x => { _enemyList.Remove(x); });
            _enemyList.Add(enemy);
        }

        public List<Enemy> GetEnemies()
        {
            return _enemyList;
        }

        public List<Behaviour> GetEnemyBehaviour(int turnIndex)
        {
            return _enemyList.Select(x => x.GetEnemyBehaviour(turnIndex)).ToList();
        }

        public void StageEnd()
        {
            for (var i = _enemyList.Count - 1; i >= 0; i--)
            {
                DestroyImmediate(_enemyList[i]);
                _enemyList.RemoveAt(i);
            }
            // TODO リザルトに遷移？
        }
    }
}