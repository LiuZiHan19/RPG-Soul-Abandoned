using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    //[SerializeField] private float parallaxEffect;
    private GameObject cam;
    //private float xPosition;
    private float length;

    [SerializeField] private float moveSpeed;

    private void Awake()
    {
        cam = GameObject.Find("Main Camera");
        //xPosition = transform.position.x;

        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void Update()
    {
        //Just move towards forward 
        //float distanceToMove = cam.transform.position.x * parallaxEffect;
        //transform.position = new Vector3(xPosition + distanceToMove, transform.position.y);

        //Endless background
        //float distanceMoved = cam.transform.position.x * (1 - parallaxEffect);
        //if (distanceMoved > xPosition + length)
        //    xPosition = xPosition + length;
        //else if (distanceMoved < xPosition - length)
        //    xPosition = xPosition - length;

        //follow
        transform.position = new Vector3(cam.transform.position.x * moveSpeed, transform.position.y, transform.position.y);

        //endless
        if (cam.transform.position.x - transform.position.x >= length)
            transform.position = cam.transform.position;
    }
}
