using System.Collections.Generic;
using ScriptableObjects.Enemy;
using UnityEngine;

namespace ScriptableObjects.Stage
{
    [CreateAssetMenu(fileName = "StageData", menuName = "ScriptableObjects/StageData")]
    public class StageDefinition : ScriptableObject
    {
        public List<EnemyDefinition> enemyList;
        public Sprite backGroundSprite;
    }
}
