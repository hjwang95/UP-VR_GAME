using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingReset : MonoBehaviour {

    private Vector3 startPosition;
	// Use this for initialization
	void Start () {
        startPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.x < 25f)
        {
            transform.position = startPosition;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
	}
}
