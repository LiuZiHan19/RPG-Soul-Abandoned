using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgniteStrikeFx : MonoBehaviour
{
    public void Setup(Transform target)
    {
        transform.position = target.position;
    }
}
