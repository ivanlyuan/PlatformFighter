using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{

    private Target[] Targets;
    private LineRenderer lineRenderer;


	void Start ()
    {
        Targets = GetComponentsInChildren<Target>();
        lineRenderer = GetComponent<LineRenderer>();
        CreateLines();
	}

    private void CreateLines()
    {
        lineRenderer.positionCount = Targets.Length;



        for (int i = 0; i < Targets.Length; i++) 
        {
            lineRenderer.SetPosition(i, Targets[i].transform.position);
        }
    }

}
