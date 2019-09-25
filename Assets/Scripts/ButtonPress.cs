using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPress : MonoBehaviour {

    public GameObject numberBoard;

    [SerializeField]
    public GameObject ButtonHit;

    public void OnButtonPressed()
    {
        numberBoard.SetActive(true);
        ButtonHit.SetActive(false);
    }
}
