using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeleton_GroundedState : EnemySkeletonState
{
    public EnemySkeleton_GroundedState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();
        if (enemy.IsPlayerDetected())
        {
            stateMachine.ChangeState(skeleton.battleState);
        }
    }
}
