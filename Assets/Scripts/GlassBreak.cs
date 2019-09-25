using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GlassBreak : MonoBehaviour

{
    [SerializeField]
    public GameObject ButtonHint;

    public static event EventHandler glassBreak;

    private void OnTriggerEnter(Collider collision)
    {
        transform.parent.Find("Shattered").gameObject.SetActive(true);
        gameObject.SetActive(false);

        OnGlassBreak();

        ButtonHint.SetActive(true);
    }

    protected virtual void OnGlassBreak()
    {
        if (glassBreak != null)
        {
            glassBreak(this, EventArgs.Empty);
        }
    }
}
