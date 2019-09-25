using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

public class MagnetOperation : MonoBehaviour
{
    public GameObject magnetPointer;
    private Animator magnetPointerAnimator;

    public GameObject spear;
    private Animator spearAnimator;


    private void Start()
    {
        magnetPointerAnimator = magnetPointer.GetComponent<Animator>();
        spearAnimator = spear.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "FrameFront")
        {
            magnetPointerAnimator.SetBool("MagnetTouch", true);
            spearAnimator.SetBool("MagnetTouch", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "FrameFront")
        {
            magnetPointerAnimator.SetBool("MagnetTouch", false);
            spearAnimator.SetBool("MagnetTouch", false);
        }
    }
}

