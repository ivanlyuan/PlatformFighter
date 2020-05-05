using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField]
    private GameMode _gameMode;

    public GameMode gameMode { get { return _gameMode; } }
    public bool isGameEnded { get; private set; }

	void Start ()
    {
        Instance = this;
        isGameEnded = false;
	}


    public void StartGame(GameMode gameMode)
    {
        
    }

    public void EndGame()
    {
        isGameEnded = true;
    }


    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}

public enum GameMode
{
    Local,
    Online,
    TargetRush
}
