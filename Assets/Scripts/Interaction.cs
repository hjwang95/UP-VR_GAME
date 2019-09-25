using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Interaction : MonoBehaviour
{
    [HideInInspector]
    public ViveAction activeHand = null;
}
