using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ThrowSwordController : MonoBehaviour
{
    private float returnSpeed;
    private float moveSpeed;
    private Vector2 dir;
    private Rigidbody2D rb;
    private Collider2D cd;
    private bool isReturn;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<Collider2D>();
    }

    private void Start()
    {
        rb.velocity = dir * moveSpeed;
    }
 
    private void Update()
    {
        if (isReturn)
        {
            rb.velocity = (PlayerManager.Instance.player.transform.position - transform.position).normalized *
                          returnSpeed;
        }
    }

    public void Setup(float moveSpeed, Vector2 dir, float returnSpeed)
    {
        this.moveSpeed = moveSpeed;
        this.dir = dir;
        this.returnSpeed = returnSpeed;
    }

    public void Return()
    {
        transform.parent = null;
        cd.enabled = true;
        rb.constraints = RigidbodyConstraints2D.None;
        rb.isKinematic = true;
        isReturn = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isReturn)
        {
            if (collision.GetComponent<Player>())
            {
                PlayerManager.Instance.player.SwordReturned(transform);
                Destroy(gameObject, .2f);
            }
        }
        else
        {
            if (collision.GetComponent<Enemy>())
            {
                cd.enabled = false;
                rb.isKinematic = true;
                rb.velocity = Vector2.zero;
                transform.SetParent(collision.transform, true);
                PlayerManager.Instance.player.Stats.DoMagicDamage(collision.GetComponent<Enemy>().Stats);
            }

            if (collision.GetComponent<TilemapCollider2D>())
            {
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }
    }
}