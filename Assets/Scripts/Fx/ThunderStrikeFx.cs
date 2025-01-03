using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderStrikeFx : MonoBehaviour
{
    [SerializeField] private float _createHeight;
    [SerializeField] private float _moveSpeed;
    private bool _isExploded;

    private Transform _target;
    private Animator _anim;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _anim = transform.Find("Animator").GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
    }

    public void Setup(Transform target)
    {
        _target = target;
        transform.position = target.position + Vector3.up * _createHeight;
    }

    private void Move()
    {
        if (_isExploded == false)
        {
            _rb.velocity = (_target.position - transform.position).normalized * _moveSpeed;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _rb.velocity = Vector2.zero;
        _isExploded = true;
        if (other.GetComponent<Enemy>())
        {
            transform.position = other.transform.position;
        }

        _anim.SetTrigger("Explode");
    }
}