using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public Timer timer;

    [SerializeField]
    private int score;


    [SerializeField]
    private CharacterPanel charPanelPrefab;
    public List<CharacterPanel> CharacterPanels;
    [SerializeField]
    private Transform charPanelLayout;

    [SerializeField]
    private GameObject endGamePanel;

    [SerializeField]
    private GameObject TargetRushEndPanel;

    [SerializeField]
    private Text TargetRushScoreText;



    private void Start()
    {
        Instance = this;
    }

    public void OnCharacterKO(Character c)
    {
        if (GetCharPanel(c.playerNumber))
        {
            GetCharPanel(c.playerNumber).OnCharacterKO();
        }
    }

    public void ClearAllCharPanels()
    {
        while (CharacterPanels.Count > 0)
        {
            Destroy(CharacterPanels[0].gameObject);
            CharacterPanels.RemoveAt(0);
        }
    }

    public void CreateCharPanel(Character c,int numOfStocks)
    {
        CharacterPanel newPanel = Instantiate(charPanelPrefab, charPanelLayout);
        CharacterPanels.Add(newPanel);
        newPanel.SetupPanel(c, numOfStocks);
    }

    public void OnCharSpawn(Character c,int playerNum,int numOfStocks)//sub to charManager
    {
        CharacterPanel cp = GetCharPanel(playerNum);
        if (cp)
        {
            cp.SetupPanel(c, numOfStocks);
        }
        else
        {
            CreateCharPanel(c, numOfStocks);
        }
    }

    public CharacterPanel GetCharPanel(int playerNum)
    {
        foreach (CharacterPanel cp in CharacterPanels)
        {
            if (cp.parentChar.playerNumber == playerNum)
            {
                return cp;
            }
        }
        //Debug.Log("CharacterPanel not found.");
        return null;
    }

    public void Show(GameObject go)
    {
        go.SetActive(true);
    }

    public void Hide(GameObject go)
    {
        go.SetActive(false);
    }


    public void ChangeScore(int amount)
    {
        score += amount;
        TargetRushScoreText.text = "Score : " + score;
    }

    public void ShowTargetRushEndPanel()
    {
        TargetRushEndPanel.GetComponentInChildren<Text>().text = "Targets : " + UIManager.Instance.score + "\nTime Remaining : " + (int)timer.timeRemaining *2 + "\nTotal Score : " + (UIManager.Instance.score + (int)timer.timeRemaining*2);
        TargetRushEndPanel.SetActive(true);
        timer.StopTimer();
        GameManager.Instance.EndGame();
    }
}

