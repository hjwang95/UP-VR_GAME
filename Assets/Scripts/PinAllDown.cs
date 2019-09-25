using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinAllDown : MonoBehaviour
{
    public GameObject zero;
    public GameObject CollectedNumber0;
    private int pinDownNumber;

    public bool checkForDemo;


    // Use this for initialization
    void Start()
    {
        pinDownNumber = 0;
        PinDown.isPinDown += updatePinDown;
    }

    // Update is called once per frame
    void Update()
    {
        if (pinDownNumber == 10 || checkForDemo)
        {
            zero.SetActive(true);
            CollectedNumber0.SetActive(true);
        }
    }

    private void updatePinDown(object sender, EventArgs args)
    {
        pinDownNumber++;
    }
}
