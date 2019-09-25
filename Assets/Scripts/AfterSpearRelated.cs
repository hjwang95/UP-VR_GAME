using System.Text;
using UnityEngine;

public class AfterSpearRelated : MonoBehaviour
{
    public GameObject foot;

    private Animator magnetTouch;

    private void Start()
    {
        magnetTouch = GetComponent<Animator>();
    }


    public void AfterSpear()
    {
        if (magnetTouch.GetBool("MagnetTouch"))
        {
            foot.SetActive(true);
        }
        else
        {
            foot.SetActive(false);
        }
    }
}
