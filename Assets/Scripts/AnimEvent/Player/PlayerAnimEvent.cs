using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimEvent : AnimBaseEvent
{
    private Player player;

    protected override void Awake()
    {
        base.Awake();
        player = entity as Player;
    }

    protected override void AnimTrigger() => player.stateMachine.currentState.triggerCalled = true;

    #region Attack

    protected override void AnimAttack()
    {
        Collider2D[] colliders = player.AttackCheck();

        foreach (Collider2D cd in colliders)
        {
            if (cd.GetComponent<Enemy>())
            {
                player.Stats.DoMagicDamage(cd.GetComponent<EnemyStats>());
            }
        }
    }

    #endregion

    #region CounterAttack

    private void AnimCounterAttack()
    {
        bool isSucess = false;
        Collider2D[] colliders = player.AttackCheck();
        foreach (Collider2D cd in colliders)
        {
            if (cd.GetComponent<Enemy_Skeleton>() && cd.GetComponent<Enemy_Skeleton>().canBeStunned)
            {
                isSucess = true;
                player.stateMachine.ChangeState(player.counterAttackSucessfulState);
                cd.GetComponent<Enemy_Skeleton>().Stunned();
            }
        }

        if (isSucess == false)
            player.stateMachine.ChangeState(player.idleState);
    }

    #endregion

    #region ThrowSword

    private void CreateThrowSword()
    {
        player.Skill.ThrowSword.Release(player.transform.position);
    }

    #endregion
}