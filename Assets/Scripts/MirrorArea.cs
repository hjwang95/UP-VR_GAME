using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Valve.VR;

public class MirrorArea : MonoBehaviour
{
   
    public GameObject mirrorArea;
    public GameObject foot;
    public GameObject stick;
   
    private void Start()
    {
        Renderer r = mirrorArea.GetComponent<Renderer>();
        Color colorOrig = r.material.color;
        mirrorArea.GetComponent<Renderer>().material.color = new Color(colorOrig.r, colorOrig.g, colorOrig.b, 0.5f);

    }
   
    void OnTriggerEnter(Collider other)
    {
        if (other.name == "feetCollider")
        {
            stick.SetActive(true);
            stick.GetComponent<Animator>().SetBool("isAppear", true);
        }

    }

   

}