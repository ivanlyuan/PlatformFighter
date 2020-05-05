using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviour
{

    public void Hide(GameObject go)
    {
        go.SetActive(false);
    }

    public void Show(GameObject go)
    {
        go.SetActive(true);
    }


}
