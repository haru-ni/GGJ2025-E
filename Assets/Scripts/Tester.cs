using Data;
using UnityEngine;
using Utils;

public class Tester : MonoBehaviour
{
    private UserData _userData = new();

    private void Update()
    {
        // SE、BGM再生のテスト
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log($"テスト:Press A");
            SoundManager.Instance.PlaySe(GameConst.SeType.Decision3);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log($"テスト:Press S");
            SoundManager.Instance.PlayBgm(GameConst.BgmType.SunnySpot);
        }

        // セーブデータの読み書きのテスト
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log($"テスト:Press P（Save）");
            SaveDataUtil.Save(_userData);
            DisplayCoin();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log($"テスト:Press O（Load）");
            _userData = SaveDataUtil.Load(_userData);
            DisplayCoin();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log($"テスト:Press I（Incre）");
            _userData.coin++;
            DisplayCoin();
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            Debug.Log($"テスト:Press U（Decre）");
            _userData = SaveDataUtil.Load(_userData);
            _userData.coin--;
            DisplayCoin();
        }
    }

    private void DisplayCoin()
    {
        Debug.Log($"テスト:{_userData.coin}");
    }
}