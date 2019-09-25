using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FountainNumberUp : MonoBehaviour
{
    void OnEnable()
    {
        PuzzleDisplay.OnPuzzleCompleted += HandleOnPuzzleCompleted;
    }
    
    void HandleOnPuzzleCompleted()
    {
        this.transform.Find("Number").gameObject.SetActive(true);
    }
}
