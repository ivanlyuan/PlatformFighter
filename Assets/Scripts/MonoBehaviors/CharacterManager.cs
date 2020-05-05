using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager Instance;

    [SerializeField]
    private Character[] CharacterPrefabs;

    [SerializeField]
    private List<Character> CharactersInGame;

    private RespawnPoint[] RespawnPoints;

    private void Start()
    {
        Instance = this;
        RespawnPoints = Stage.Instance.RespawnPoints;
    }

    public void ClearAllCharactersInGame()
    {
        while (CharactersInGame.Count > 0)
        {
            Character c = CharactersInGame[0];
            if (c)
            {
                ClearCharacter(c);
            }
            
        }
    }



    public void SpawnCharacter(Character c,int playerNumber,TeamColor teamColor,int numOfStocks)
    {

        foreach (Character character in CharacterPrefabs)
        {
            if (character == c)
            {
                //remove prev character the player was using
                Character prevChar = GetCharacter(playerNumber);
                if (prevChar)
                {
                    CharactersInGame.Remove(prevChar);
                    Destroy(prevChar.gameObject);
                }
                Character newChar = Instantiate(character, RespawnPoints[playerNumber].transform.position, Quaternion.identity);
                CharactersInGame.Add(newChar);
                newChar.OnSpawn(teamColor, playerNumber);
                UIManager.Instance.OnCharSpawn(newChar, playerNumber, numOfStocks);

                if (GameManager.Instance.gameMode == GameMode.TargetRush)
                {
                    CameraManager.Instance.SetFollowTarget(newChar.gameObject);
                }
                return;
                
            }
        }
    }

    public void ClearCharacter(int playerNum)
    {
        Character c = GetCharacter(playerNum);
        if (c)
        {
            Destroy(c.gameObject);
            CharactersInGame.RemoveAt(playerNum);
        }
    }

    public void ClearCharacter(Character c)
    {
        Destroy(c.gameObject);
        CharactersInGame.Remove(c);
    }

    public void OnCharacterKO(Character c)///NEEDS MODIFYING!!!
    {
        CharacterPanel cp = UIManager.Instance.GetCharPanel(c.playerNumber);
        if (cp.stockCount <= 0)
        {
            CharactersInGame.Remove(c);
            cp.OnCharacterDefeated();
            Destroy(c.gameObject);
        }
        else
        {
            StartCoroutine(RespawnCharacter(c));
            c.OnKnockedOut();
        }

    }

    private IEnumerator RespawnCharacter(Character c)
    {
        c.gameObject.SetActive(false);
        c.transform.position = RespawnPoints[c.playerNumber].transform.position;//respawn
        yield return new WaitForSeconds(1f);

        if (c)
        {
            c.gameObject.SetActive(true);
        }
    }

    public Character GetCharacter(int playerNum)
    {
        foreach (Character character in CharactersInGame)
        {
            if (character.playerNumber == playerNum)
            {
                return character;
            }
        }

        //Debug.Log("Character not found");
        return null;
    }

    public Character GetCharacter(Character c)
    {
        foreach (Character character in CharactersInGame)
        {
            if (character == c)
            {
                return character;
            }
        }

        Debug.Log("Character not found");
        return null;
    }
}
