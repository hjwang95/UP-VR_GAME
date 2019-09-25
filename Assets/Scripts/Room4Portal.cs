using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Valve.VR;

public class Room4Portal : MonoBehaviour
{

    public GameObject otherportal;
    public GameObject foot;
    public GameObject cameraObject;
   
    public Boolean shouldPortal = false;
    private void Update()
    {
        if (shouldPortal == true)
        {
            Vector3 origPos = cameraObject.transform.position;
           
            cameraObject.transform.position = new Vector3(origPos.x, origPos.y, origPos.z - 15.64f);
           
            shouldPortal = false;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "feetCollider")
        {
            StartCoroutine(DoTheDance());
        }


    }

    public IEnumerator DoTheDance()
    {
        shouldPortal = false;
        yield return new WaitForSeconds(5f); // waits 3 seconds
        shouldPortal = true; // will make the update method pick up 
    }

    private void OnTriggerExit(Collider other)
    {
        
        if(other.name == "feetCollider")
        {
            shouldPortal = false;
            foot.SetActive(false);
            StartCoroutine(DoTheEnable());
           
        }
            
    }

    public IEnumerator DoTheEnable()
    {
        yield return new WaitForSeconds(5f); // waits 3 seconds
        foot.SetActive(true);
    }



}