using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeleton_IdleState : EnemySkeleton_GroundedState
{
    public EnemySkeleton_IdleState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = skeleton.idleTime;
    }

    public override void Update()
    {
        base.Update();

        //Idle to walk
        if (stateTimer < 0)
            stateMachine.ChangeState(skeleton.walkState);
    }
}
