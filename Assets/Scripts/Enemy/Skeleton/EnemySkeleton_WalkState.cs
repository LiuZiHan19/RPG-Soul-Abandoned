using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeleton_WalkState : EnemySkeleton_GroundedState
{
    public EnemySkeleton_WalkState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        skeleton.SetVelocity(skeleton.FacingDir * skeleton.moveSpeed, rb.velocity.y);

        if (skeleton.IsWallDetected())
        {
            skeleton.Flip();
            stateMachine.ChangeState(skeleton.idleState);
        }
    }
}
