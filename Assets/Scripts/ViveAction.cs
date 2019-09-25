using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ViveAction : MonoBehaviour
{
    public SteamVR_Action_Boolean grabAction = null;

    private SteamVR_Behaviour_Pose pose = null;
    private FixedJoint joint = null;

    private Interaction currentInteractable = null;
    public List<Interaction> contactInteractables = new List<Interaction>();

    private Interaction currentInteractable_btn = null;
    public List<Interaction> contactInteractables_btn = new List<Interaction>();

    private bool stickyThing;
    private bool pickableSpray;
    private GameObject sticky;

    //Fairywond Related
    [SerializeField]
    public GameObject hintEffect;
    public static event EventHandler pickFairyBar;
    public static event EventHandler dropFairyBar;

    //Magnet Related
    private GameObject magnet;
    private Animator magnetAnimator;
    private bool magnetTouch;

    public static bool magnetActive;

    // Sound Related
    public static event EventHandler getFairyWand;
    public static event EventHandler getButtonDown;

    Animator btndown;

    private void Awake()
    {
        stickyThing = false;
        magnetActive = false;
        pose = GetComponent<SteamVR_Behaviour_Pose>();
        joint = GetComponent<FixedJoint>();
        btndown = GameObject.Find("Button-red").GetComponent<Animator>();

        pickableSpray = false;
        BubbleBreak.bubbleBreak += activeSpray;
    }

    //Update is called once per frame
    private void Update()
    {
        if (stickyThing)
        {
            sticky.transform.position = this.transform.position - new Vector3(0, 0.2f, 0);
        }
        //Down
        if (grabAction.GetStateDown(pose.inputSource))
        {
            //print(pose.inputSource + ": Trigger Down");
            Pickup();
            if (!btndown.GetBool("BtnDown"))
            {
                TriggerButton();
            }
        }
        //Up
        if (grabAction.GetStateUp(pose.inputSource))
        {
            //print(pose.inputSource + "Trigger Up");
            Drop();
            UntriggerButton();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sticky") && pickableSpray)
        {
            sticky = other.gameObject;
            ViveTexturePainter.spray = sticky;
            ViveTexturePainter.brushColor = sticky.transform.GetChild(0).GetComponent<Renderer>().material.color;
            stickyThing = true;
        }

        if (other.gameObject.CompareTag("Interactable"))
        {
            if (other.gameObject.name == "Magnet")
            {

                contactInteractables.Add(other.gameObject.GetComponent<Interaction>());
                print("Magnet in");
                magnet = other.gameObject;
                magnetAnimator = magnet.GetComponent<Animator>();
                magnetTouch = true;
            }
            else if(other.gameObject.name == "FairyBarLong")
            {
                hintEffect.SetActive(false);
                OnGetFairyWand();
                OnPickFairyBar();

                contactInteractables.Add(other.gameObject.transform.GetChild(0).gameObject.GetComponent<Interaction>());
                // Debug.Log(other.gameObject.transform.GetChild(0).gameObject);
                // Debug.Log(contactInteractables);
            }
            else
            {
                contactInteractables.Add(other.gameObject.GetComponent<Interaction>());
            }
        }
        else if(other.gameObject.CompareTag("Interactable_Btn"))
        {
            contactInteractables_btn.Add(other.gameObject.GetComponent<Interaction>());
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Interactable"))
        {

            if (other.gameObject.name == "Magnet")
            {
                magnetTouch = false;

                contactInteractables.Remove(other.gameObject.GetComponent<Interaction>());
            }
            else if (other.gameObject.name == "FairyBarLong")
            {
                OnDropFairyBar();

                contactInteractables.Remove(other.gameObject.transform.GetChild(0).gameObject.GetComponent<Interaction>());
            }
            else
            {
                contactInteractables.Remove(other.gameObject.GetComponent<Interaction>());
            }
        }
        else if (other.gameObject.CompareTag("Interactable_Btn"))
        {
            contactInteractables_btn.Remove(other.gameObject.GetComponent<Interaction>());
        }
    }

    public void Pickup()
    {
        // Get nearest 
        currentInteractable = GetNearestInteractable();

        // Null check
        if (!currentInteractable)
            return;

        // Already held, check
        if (currentInteractable.activeHand)
            currentInteractable.activeHand.Drop();

        // Position
        currentInteractable.transform.position = transform.position;

        // Attach
        Rigidbody targetBody = currentInteractable.GetComponent<Rigidbody>();
        joint.connectedBody = targetBody;

        // Debug.Log(joint.anchor);

        // Set Active hand
        currentInteractable.activeHand = this;

        //check magnet
        if(magnetTouch && magnetActive)
        {
            magnetAnimator.SetBool("Hold", true);
        }
    }

    public void Drop()
    {
        // Null check
        if (!currentInteractable)
            return;

        // Apply velocity
        Rigidbody targetBody = currentInteractable.GetComponent<Rigidbody>();
        targetBody.velocity = pose.GetVelocity();
        targetBody.angularVelocity = pose.GetAngularVelocity();


        // Detach
        joint.connectedBody = null;

        // Clear
        currentInteractable.activeHand = null;
        currentInteractable = null;

        //check magnet
        if (!magnetTouch && magnetActive)
        {
            magnetAnimator.SetBool("Hold", false);
        }
    }

    public void TriggerButton()
    {
        // Get nearest 
        currentInteractable_btn = GetNearestInteractableBtn();

        // Null check
        if (!currentInteractable_btn)
            return;

        // Position
       if (transform.position.y <= 0.52f && transform.position.y >= 0.46f)
        {
            Rigidbody targetBody = currentInteractable_btn.GetComponent<Rigidbody>();
            joint.connectedBody = targetBody;
            
            currentInteractable_btn.transform.position = new Vector3(currentInteractable_btn.transform.position.x, transform.position.y - 0.12f, currentInteractable_btn.transform.position.z);
        }

        if (transform.position.y <= 0.49f)
        {
            btndown.SetBool("BtnDown", true);
            OnGetButtonDown();
        }
        
        // Detach
        joint.connectedBody = null;

        // Set Active hand
        currentInteractable_btn.activeHand = this;
    }

    public void UntriggerButton()
    {
        // Null check
        if (!currentInteractable_btn)
            return;
        
        // Clear
        currentInteractable_btn.activeHand = null;
        currentInteractable_btn = null;
    }

    private Interaction GetNearestInteractable()
    {
        Interaction nearest = null;
        float minDistance = float.MaxValue;
        float distance = 0.0f;

        foreach (Interaction interaction in contactInteractables)
        {
            distance = (interaction.transform.position - transform.position).sqrMagnitude;

            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = interaction;
            }
        }
        return nearest;
    }

    private Interaction GetNearestInteractableBtn()
    {
        Interaction nearest = null;
        float minDistance = float.MaxValue;
        float distance = 0.0f;

        foreach (Interaction interaction in contactInteractables_btn)
        {
            distance = (interaction.transform.position - transform.position).sqrMagnitude;

            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = interaction;
            }
        }
        return nearest;
    }

    // Spray related
    private void activeSpray(object sender, EventArgs args)
    {
        pickableSpray = true;
    }

    // Fairy Bar Related
    protected virtual void OnPickFairyBar()
    {
        if (pickFairyBar != null)
        {
            pickFairyBar(this, EventArgs.Empty);
        }
    }

    protected virtual void OnDropFairyBar()
    {
        if (dropFairyBar != null)
        {
            dropFairyBar(this, EventArgs.Empty);
        }
    }

    // Sound Related
    protected virtual void OnGetFairyWand()
    {
        if (getFairyWand != null)
        {
            getFairyWand(this, EventArgs.Empty);
        }
    }

    protected virtual void OnGetButtonDown()
    {
        if (getButtonDown != null)
        {
            getButtonDown(this, EventArgs.Empty);
        }
    }
}
