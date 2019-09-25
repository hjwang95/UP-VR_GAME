using System.Text;
using UnityEngine;

public class DestroyStickRelated : MonoBehaviour
{
    public GameObject mirrorArea;
    public GameObject foot;


    public void DestroyAnimator()
    {
        foot.SetActive(false);
        mirrorArea.GetComponent<BoxCollider>().enabled = false;
        this.GetComponent<Animator>().enabled = false;
    }
}
