using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugUIManager : MonoBehaviour
{
    [SerializeField]
    Character DummyPrefab;

    [SerializeField]
    Character PlayerPrefab;

    [SerializeField]
    int numOfStocksPerSpawn;

    public void ClearAllCharacters()
    {
        CharacterManager.Instance.ClearAllCharactersInGame();
        UIManager.Instance.ClearAllCharPanels();
    }

    public void ResetTimer(float time)
    {
        UIManager.Instance.timer.SetupTimer(time);
        UIManager.Instance.timer.StartTimer();
    }

    public void SpawnDummy(int playerNumber)
    {
        CharacterManager.Instance.SpawnCharacter(DummyPrefab, playerNumber, (TeamColor)playerNumber,3);
    }

    public void SpawnPlayer(int playerNumber)
    {
        CharacterManager.Instance.SpawnCharacter(PlayerPrefab, playerNumber, (TeamColor)playerNumber, numOfStocksPerSpawn);
    }
}
