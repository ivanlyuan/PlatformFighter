using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public static Stage Instance;
    public RespawnPoint[] RespawnPoints { get; private set; }

    void Start ()
    {
        Instance = this;
        RespawnPoints = GetComponentsInChildren<RespawnPoint>();
    }
	
}
