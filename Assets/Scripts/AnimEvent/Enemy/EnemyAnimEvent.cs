using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimEvent : AnimBaseEvent
{
    protected Enemy enemy;

    protected override void Awake()
    {
        base.Awake();
        enemy = entity as Enemy;
    }

    protected override void AnimTrigger() => enemy.stateMachine.currentState.triggerCalled = true;

    protected override void AnimAttack()
    {
        Collider2D[] colliders = enemy.AttackCheck();

        foreach (Collider2D cd in colliders)
        {
            if (cd.GetComponent<Player>())
            {
                enemy.Stats.DoMagicDamage(cd.GetComponent<Player>().Stats);
                cd.GetComponent<Player>().Knockback(enemy.FacingDir, enemy.knockbackForce);
            }
        }
    }
}