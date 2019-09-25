using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorHit : MonoBehaviour
{
    [SerializeField]
    public GameObject GetHitEffect;
    private ContactPoint contact;
   

    //public static event EventHandler glassBreak;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit Mirror!!!!!!!!!!!!!\n");
        GetHitEffect.SetActive(true);
        //Debug.Log(GetHitEffect.transform.position);
       // Debug.Log(this.transform.position);
        contact = collision.contacts[0];
        Debug.Log(contact.point);
        GetHitEffect.transform.position = contact.point;
        GetHitEffect.GetComponent<ParticleSystem>().Play();
       // System.Threading.Thread.Sleep(2000);
      //  GetHitEffect.SetActive(false);

    }

   /* private void OnTriggerExit(Collider other)
    {
        GetHitEffect.SetActive(false);
        GetHitEffect.GetComponent<ParticleSystem>().Stop();
    }*/
}
