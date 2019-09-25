using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSwitch : MonoBehaviour {

    public int AudioCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        AudioCount++;
    }

}
