using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class FountainNumber : MonoBehaviour
{
    public float upForce = 1f;
    public float sideForce = .1f;
    float life = 1.0f;

    public bool destory;

    UnityEvent timeoutEvent = new UnityEvent();



    public void Start()
    {
        timeoutEvent.AddListener(kill);

        float xForce = Random.Range(-sideForce, sideForce);
        float yForce = Random.Range(upForce / 2f, upForce);
        float zForce = Random.Range(-sideForce, sideForce);

        Vector3 force = new Vector3(xForce, yForce, zForce);

        GetComponent<Rigidbody>().velocity = force;

    }

    public void Update()
    {
        life -= Time.deltaTime;
        if (life < 0)
        {
            //Destroy(transform.parent.gameObject);
        }

        if (life < 0 && timeoutEvent != null)
        {
            if (destory)
            {
                timeoutEvent.Invoke();
            }
        }
        
    }

    void kill()
    {
        Destroy(transform.parent.gameObject);
    }

}