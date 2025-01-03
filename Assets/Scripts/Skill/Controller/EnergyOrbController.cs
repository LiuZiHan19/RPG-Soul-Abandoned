using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyOrbController : MonoBehaviour
{
    private float rotationSpeed = 5.0f;
    private float radius = 2.0f;
    private Vector3 axis = Vector3.forward; // ��ת��
    private float angle; // ��ǰ�Ƕ�
    private Transform player;
    private float aliveDuration;
    private float aliveCounter;

    private void Start()
    {
        aliveCounter = aliveDuration;
    }

    void Update()
    {
        aliveCounter -= Time.deltaTime;
        if (aliveCounter < 0)
            Destroy(gameObject);

        // ���������λ�ã�ʹ��ʼ���������Χ
        angle += rotationSpeed * Time.deltaTime;
        Vector3 offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
        transform.position = player.position + offset;

        // Χ�������ת���������ǽ�������ΪVector3.up����ʾ��Y����ת
        transform.RotateAround(player.position, axis, rotationSpeed * Time.deltaTime);
    }

    public void Setup(float rotationSpeed, float radius, Transform player, float aliveDuration)
    {
        this.rotationSpeed = rotationSpeed;
        this.radius = radius;
        this.player = player;
        this.aliveDuration = aliveDuration;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>())
        {
            PlayerManager.Instance.player.Stats.DoMagicDamage(collision.GetComponent<Enemy>().Stats);
        }
    }
}
