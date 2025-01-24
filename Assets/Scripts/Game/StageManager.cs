using System.Collections.Generic;
using Data;
using ScriptableObjects.Enemy;
using ScriptableObjects.Stage;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Game
{
    public class StageManager : Singleton<StageManager>
    {
        [SerializeField] private List<StageDefinition> stageDefinitions = new();
        [SerializeField] private Enemy enemyPrefab;
        [SerializeField] private RectTransform enemyArea;
        [SerializeField] private Image backGroundImage;

        private readonly List<Enemy> _enemyList = new();

        public void Awake()
        {
            // TODO 適切なステージインデックスを読み込むようにする
            const int stageIndex = 1;
            StageSetup(stageDefinitions[stageIndex % stageDefinitions.Count]);
        }

        public void StageSetup(StageDefinition stageDefinition)
        {
            backGroundImage.sprite = stageDefinition.backGroundSprite;
            foreach (var element in stageDefinition.enemyList)
            {
                CreateEnemy(element);
            }
        }

        public void CreateEnemy(EnemyDefinition enemyDefinition)
        {
            var enemy = Instantiate(enemyPrefab, enemyArea);
            enemy.Setup(enemyDefinition);
            _enemyList.Add(enemy);
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