using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeleton_AttackState : EnemySkeletonState
{
    public EnemySkeleton_AttackState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        skeleton.StartCoroutine("BusyFor", .2f);
    }

    public override void Update()
    {
        base.Update();
        skeleton.ZeroVelocity();
        if (triggerCalled)
            stateMachine.ChangeState(skeleton.battleState);
    }
}
