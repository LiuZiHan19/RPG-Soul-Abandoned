using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    [Header("Cooldown")]
    [SerializeField] protected float cooldown;
    protected float cooldownTimer;
    protected Player player { get; private set; }

    protected virtual void Start()
    {
        player = PlayerManager.Instance.player;
    }

    public bool CanUseSkill()
    {
        if (cooldownTimer < 0)
        {
            cooldownTimer = cooldown;
            return true;
        }
        return false;
    }

    protected virtual void Update()
    {
        cooldownTimer -= Time.deltaTime;
    }
}
