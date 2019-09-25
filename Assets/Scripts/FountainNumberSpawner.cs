using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FountainNumberSpawner : MonoBehaviour
{


    public GameObject number;

    void FixedUpdate()
    {
        Instantiate(number, transform.position, Quaternion.identity, this.transform);
    }


}
