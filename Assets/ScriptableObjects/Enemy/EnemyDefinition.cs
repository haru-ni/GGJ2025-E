using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace ScriptableObjects.Enemy
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData")]
    public class EnemyDefinition : ScriptableObject
    {
        public Sprite sprite;
        public List<Behaviour> BehaviourList;
        public int hp;
    }

    [Serializable]
    public class  Behaviour
    {
        public GameConst.ColorType color;
        public int power;
    };
}
