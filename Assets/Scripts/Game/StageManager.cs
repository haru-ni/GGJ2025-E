using System.Collections.Generic;
using ScriptableObjects.Stage;
using UnityEngine;
using Utils;

namespace Game
{
    public class StageManager : Singleton<StageManager>
    {
        [SerializeField] private List<StageDefinition> stageDefinitions = new();
    }
}
