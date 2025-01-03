using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoParallaxBackground : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private void Update()
    {
        transform.position = transform.position + Vector3.right * moveSpeed * Time.deltaTime;
        if (transform.position.x > 20)
            transform.position = new Vector3(-20, transform.position.y);
    }
}
