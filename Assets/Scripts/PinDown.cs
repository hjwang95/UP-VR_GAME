using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinDown : MonoBehaviour
{
    public static event EventHandler isPinDown;
    private bool down;

    // Use this for initialization
    void Start()
    {
        down = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!down)
        {
            //Problem when teleporting, transform(360, 0, 360)
            //So mod 359 for floating problem

            if (Mathf.Abs(transform.eulerAngles.x) % 359.0f > 20.0f || Mathf.Abs(transform.eulerAngles.z) % 359.0f > 20.0f)
            {
                OnIsPinDown();

                down = true;
            }
        }
    }
    
    protected virtual void OnIsPinDown()
    {
        if (isPinDown != null)
        {
            isPinDown(this, EventArgs.Empty);
        }
    }
}
