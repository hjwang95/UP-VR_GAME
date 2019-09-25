using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnableUI : MonoBehaviour 
{
	public GameObject[] hiddenElements;
    void Awake()
    {
        for (int i = 0; i < hiddenElements.Length; i++)
		{
			hiddenElements[i].SetActive(false);
		}	
	}

	void OnTriggerEnter(Collider other)
	{
        if (transform.gameObject.name == "DogHouse")
        {
            int randomNumber = Random.Range(0, 100);
            if (randomNumber < 80)
            {
                int i = -1;
                if (other.gameObject.name == "Stick")
                {
                    int randomNumber2 = Random.Range(0, 100);
                    if (randomNumber2 < 70)
                    {
                        hiddenElements[0].SetActive(true);
                        i = 0;
                    }
                    else
                    {
                        hiddenElements[2].SetActive(true);
                        i = 2;
                    }
                }
                else
                {
                    hiddenElements[1].SetActive(true);
                    i = 1;
                }

                StartCoroutine(activeTimeout(i));
            }
        }

        else
        {
            for (int i = 0; i < hiddenElements.Length; i++)
            {
                hiddenElements[i].SetActive(true);
                if (hiddenElements[i].name == "Hint_BreakBubble")
                {
                    hiddenElements[i].GetComponent<Animator>().SetBool("HintRotate", true);
                }
                if (hiddenElements[i].name == "Hint_Spray" || hiddenElements[i].name == "Hint_Spray (1)")
                {
                    hiddenElements[i].GetComponent<Animator>().SetBool("HintDown", true);
                }
            }
        }
	}

    void OnTriggerExit(Collider col)
	{
        if (transform.gameObject.name != "DogHouse")
        {
            for (int i = 0; i < hiddenElements.Length; i++)
            {
                hiddenElements[i].SetActive(false);
            }
        }
	}


    IEnumerator activeTimeout(int i)
    {
        yield return new WaitForSeconds(5);
        hiddenElements[i].SetActive(false);
    }

}
