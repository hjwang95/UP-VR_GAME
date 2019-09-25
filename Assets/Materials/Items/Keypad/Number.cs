using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Number : MonoBehaviour {
    public GameObject keypad;
    /////// set VR here
    /// for now mouse click
    
    private void OnTriggerEnter(Collider other)
    {
        keypad.GetComponent<Code>().Receiver(gameObject);
    }


}
