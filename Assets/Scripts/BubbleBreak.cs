using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBreak : MonoBehaviour
{ 

    public static event EventHandler bubbleBreak;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "FairyBar")
        {
            gameObject.SetActive(false);
            OnBubbleBreak();
        }
    }

    protected virtual void OnBubbleBreak()
    {
        if (bubbleBreak != null)
        {
            bubbleBreak(this, EventArgs.Empty);
        }
    }
}
