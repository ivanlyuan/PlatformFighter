using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Timer : MonoBehaviour
{

    private Text text;
    public float timeRemaining { private set; get; }
    private bool isStarted = false;
    private bool isStopped = false;

	void Start ()
    {
        text = GetComponent<Text>();
	}

    public void SetupTimer(float seconds)
    {
        timeRemaining = seconds;
    }
	
    public void StartTimer()
    {
        StartCoroutine(Run());
    }

    public void StopTimer()
    {
        isStopped = true;
    }

    private IEnumerator Run()
    {
        if (isStarted)
        {
            yield break;
        }

        isStarted = true;
        while (timeRemaining > 0)
        {
            if (isStopped)
            {
                yield break;
            }


            timeRemaining -= Time.deltaTime;
            text.text = ((int)timeRemaining).ToString();
            yield return new WaitForEndOfFrame();
        }

        yield break;
    }

}
