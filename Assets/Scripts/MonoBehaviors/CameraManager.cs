using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    public static CameraManager Instance;


    [SerializeField]
    GameObject targetToFollow;
	// Use this for initialization
	void Start ()
    {
        Instance = this;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (targetToFollow)
        {
            transform.position = new Vector3(targetToFollow.transform.position.x, targetToFollow.transform.position.y, transform.position.z);
        }
	}

    public void SetFollowTarget(GameObject go)
    {
        targetToFollow = go;
    }
}
