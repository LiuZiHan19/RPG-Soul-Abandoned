using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricOrbController : MonoBehaviour
{
    private float moveSpeed;
    private float aliveDuration;
    private float aliveCounter;

    private void Start()
    {
        aliveCounter = aliveDuration;
    }

    void Update()
    {
        aliveCounter -= Time.deltaTime;
        if (aliveCounter <= 0) Destroy(gameObject);
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
    }

    public void Setup(float moveSpeed, float aliveDuration)
    {
        this.moveSpeed = moveSpeed;
        this.aliveDuration = aliveDuration;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>())
        {
            PlayerManager.Instance.player.Stats.DoMagicDamage(collision.GetComponent<Enemy>().Stats);
        }
    }
}
