using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ViveInput : MonoBehaviour
{
    public SteamVR_Action_Boolean touchPadAction;
    public SteamVR_Action_Vector2 touchPadActionValue;

    private bool touchingPad;
    private Vector2 touchPadValue;
    private Vector2 touchPadStartValue;
    private Vector2 touchPadEndValue;

    public static int touchpadDirectionValue;
    public static event EventHandler touchpadDirection;

    private void Awake()
    {
        touchPadStartValue = Vector2.zero;
        touchPadEndValue = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        touchingPad = touchPadAction.GetState(SteamVR_Input_Sources.Any);

        if (touchingPad)
        {
            if (touchPadStartValue == Vector2.zero)
            {
                touchPadStartValue = touchPadActionValue.GetAxis(SteamVR_Input_Sources.Any);
            }
            else
            {
                touchPadEndValue = touchPadActionValue.GetAxis(SteamVR_Input_Sources.Any);
            }
        }
        else
        {
            if ((touchpadDirectionValue = getDirection(touchPadStartValue, touchPadEndValue)) != -1)
            {
                OnTouchpadDirection();
            }

            touchPadStartValue = touchPadEndValue = Vector2.zero;
        }


    }

    protected virtual void OnTouchpadDirection()
    {
        if(touchpadDirection != null)
        {
            touchpadDirection(this, EventArgs.Empty);
        }
    }

    private int getDirection(Vector2 start, Vector2 end)
    {
        if (start == end)
        {
            return -1;
        }

        float y = Mathf.Abs(end.y - start.y);
        float x = Mathf.Abs(end.x - start.x);

        if (y > x)
        {
            if (end.y - start.y < 0)
            {
                return 1; //Down
            }
            else if (end.y - start.y > 0)
            {
                return 2; //Up
            }
        }
        else
        {
            if (end.x - start.x < 0)
            {
                return 3; //left
            }
            else if (end.x - start.x > 0)
            {
                return 4; //right
            }
        }

        return -1;
    }
}
