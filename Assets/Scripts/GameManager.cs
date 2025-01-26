using Game;
using Meta;
using UnityEngine;
using Utils;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private TitleWindowManager titleWindowManager;
    [SerializeField] private InGameWindowManager inGameManager;
    [SerializeField] private EndingWindowManager endingWindowManager;

    public int currentStageIndex;

    protected override void Awake()
    {
        base.Awake();
        Setup();
    }

    private void Setup()
    {
        titleWindowManager.Setup();
        titleWindowManager.SetEnable(true);
    }

    public void TitleToInGame()
    {
        titleWindowManager.SetEnable(false);
        inGameManager.Setup();
        inGameManager.SetEnable(true);
    }

    public void InGameToTitle()
    {
        inGameManager.SetEnable(false);
        titleWindowManager.Setup();
        titleWindowManager.SetEnable(true);
    }

    public void Ending()
    {
        inGameManager.SetEnable(false);
        titleWindowManager.SetEnable(false);
        endingWindowManager.SetEnable(true);
        endingWindowManager.Setup();
    }
}