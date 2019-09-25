using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairybarReset : MonoBehaviour
{

    private GameObject fairyBar;
    private Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        fairyBar = transform.gameObject;
        rb = transform.GetComponent<Rigidbody>();

        ViveAction.pickFairyBar += unfreezeFairyBar;
        ViveAction.dropFairyBar += freezeFairyBar;
    }
   

    private void freezeFairyBar(object sender, EventArgs args)
    {
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
    }

    private void unfreezeFairyBar(object sender, EventArgs args)
    {
        rb.constraints = RigidbodyConstraints.None;
    }
}
