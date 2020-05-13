using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HollowPlatform : MonoBehaviour
{
    private PlatformEffector2D effector;
    private float waitTime;
    public float timeToWait = 0.5f;

    private void Start()
    {
        effector = GetComponent<PlatformEffector2D>();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.DownArrow) || (Input.GetKeyUp(KeyCode.S)))
        {
            waitTime = timeToWait;
        }

        if (Input.GetKey(KeyCode.DownArrow) || (Input.GetKey(KeyCode.S)))
        {
            if (waitTime <= 0)
            {
                effector.rotationalOffset = 180f;
                waitTime = timeToWait;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }

        if (Input.GetKey(KeyCode.UpArrow) || (Input.GetKey(KeyCode.W)))
        {
            effector.rotationalOffset = 0;
        }
    }
}
