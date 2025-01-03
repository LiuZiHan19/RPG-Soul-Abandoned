using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicOrbController : MonoBehaviour
{
    private float moveSpeed;
    private float sinChangeSpeed;
    private float moveSize;
    private float sin;
    private float aliveDuration;
    private float aliveCounter;

    private void Start()
    {
        aliveCounter = aliveDuration;
    }

    void Update()
    {
        Alive();

        SinMove();
    }

    #region SinMove
    private void SinMove()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

        sin += Time.deltaTime * sinChangeSpeed;
        transform.Translate(Vector3.up * moveSize * Time.deltaTime * Mathf.Sin(sin));
    }
    #endregion

    #region Setup
    public void Setup(float moveSpeed, float changeFrequency, float moveSize, float aliveDuration)
    {
        this.moveSpeed = moveSpeed;
        this.sinChangeSpeed = changeFrequency;
        this.moveSize = moveSize;
        this.aliveDuration = aliveDuration;
    }
    #endregion

    #region Alive
    private void Alive()
    {
        aliveCounter -= Time.deltaTime;
        if (aliveCounter < 0)
            Destroy(gameObject);
    }
    #endregion

    #region OnTriggerEnter2D
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>())
        {
            PlayerManager.Instance.player.Stats.DoMagicDamage(collision.GetComponent<Enemy>().Stats);
        }
    }
    #endregion
}
