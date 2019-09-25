using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatterNow : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer!=9)
        {
            GetComponent<Rigidbody>().isKinematic = false;
            Invoke("Allow", 0.2f);
        }
    }
    void Allow()
    {
        gameObject.layer = 10;
    }
}
