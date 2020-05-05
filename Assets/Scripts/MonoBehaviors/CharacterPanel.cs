using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPanel : MonoBehaviour
{
    public Character parentChar { get; private set; }

    public int stockCount { get; private set; }
    [SerializeField]
    private Text charNameText;
    [SerializeField]
    private Text damageText;
    private Sprite charIcon;
    private Sprite stockIcon;

    public MeterBar meterBar { get; private set; }

    public bool isDefeated { get; private set; }

    private void Start()
    {
        meterBar = GetComponentInChildren<MeterBar>();
    }

    public void SetupPanel(Character c,int numOfStocks)
    {
        //generate stock icons
        parentChar = c;
        stockCount = numOfStocks;
        charIcon = c.charIcon;
        UpdateDamageText();
        UpdateMeterBar();

    }

    public void OnCharacterKO()
    {
        Debug.Log("OnCharacterKO()");
        stockCount--;
        if (stockCount <= 0)
        {
            isDefeated = true;
            Debug.Log("Player" + parentChar.playerNumber + " defeated");
        }
    }

    public void OnCharacterDefeated()
    {
        damageText.text = "";
    }

    public void UpdateDamageText()
    {
        damageText.text = parentChar.charState.damagePercentage.ToString() + "%";
    }

    public void UpdateMeterBar()
    {
        if (meterBar)
        {
            meterBar.SetMeterBar(parentChar.charState.meter, parentChar.charSettings.maxMeterSize);
        }

    }

}
