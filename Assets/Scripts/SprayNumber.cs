using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SprayNumber : MonoBehaviour
{
    public SteamVR_Action_Single squeeze = null;

    public GameObject Spray;
    public GameObject feetCollider;

    public GameObject CollectedNumber7;
    public GameObject CollectedNumber6;
    public GameObject room1Hint;

    private Material SprayColor;
    private bool triggerDown;

    //Sound related
    public static event EventHandler spraying;
    public static event EventHandler trainMoving;
    public static event EventHandler doorOpen;

   
    private void Start()
    {
        triggerDown = false;
        SprayColor = Spray.transform.GetChild(0).GetComponent<Renderer>().material;
        Debug.Log(SprayColor);
    }

    void Update()
    {
        float squeezeValue = squeeze.GetAxis(SteamVR_Input_Sources.Any);
        if (squeezeValue > 0)
        {
            triggerDown = true;
        }
        else
        {
            triggerDown = false;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Sticky" && triggerDown)
        {
            OnSpraying();

            if (this.name == "7")
            {
                room1Hint.SetActive(true);
                room1Hint.GetComponent<Animator>().SetBool("HintReadyToGo", true);
                GameObject[] door = GameObject.FindGameObjectsWithTag("Door");
                
                foreach(GameObject d in door)
                {
                    d.GetComponent<Animator>().SetTrigger("Open");
                }

                //Debug.Log("Spray collision");
                //
                feetCollider.SetActive(true);

                this.GetComponent<Renderer>().material = SprayColor;
                Spray.SetActive(false);
                CollectedNumber7.SetActive(true);

                OnDoorOpen();
            }

            else if (this.name == "6")
            {
                Animator trainMovement = GameObject.Find("Train").GetComponent<Animator>();
                trainMovement.Play("Room2-TrainMovement");

                
                this.GetComponent<Renderer>().material = SprayColor;
                Spray.SetActive(false);

                OnTrainMoving();
                CollectedNumber6.SetActive(true);

                //Animator sixMovement = GameObject.Find("Tool6").GetComponent<Animator>();
                //sixMovement.SetTrigger("Spray");

            }
        }

    }

    protected virtual void OnSpraying()
    {
        if (spraying != null)
        {
            spraying(this, EventArgs.Empty);
        }
    }

    protected virtual void OnTrainMoving()
    {
        if (trainMoving != null)
        {
            trainMoving(this, EventArgs.Empty);
        }
    }

    protected virtual void OnDoorOpen()
    {
        if (doorOpen != null)
        {
            doorOpen(this, EventArgs.Empty);
        }
    }
}
