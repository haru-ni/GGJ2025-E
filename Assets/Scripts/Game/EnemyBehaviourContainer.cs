using System.Collections.Generic;
using UnityEngine;
using Behaviour = ScriptableObjects.Enemy.Behaviour;

namespace Game
{
    public class EnemyBehaviourContainer : MonoBehaviour
    {

        private List<AttackInformation> _attackInformation;

        public void Setup(int order,Behaviour behaviour)
        {

        }
    }
}