using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeterBar : MonoBehaviour
{
    [SerializeField]
    Image meter;
    [SerializeField]
    Image meterBackground;

    public void SetMeterBar(float curMeter,float maxMeterSize)
    {
        meter.rectTransform.localScale = new Vector3(curMeter / maxMeterSize, 1, 1);
    }
	

}
