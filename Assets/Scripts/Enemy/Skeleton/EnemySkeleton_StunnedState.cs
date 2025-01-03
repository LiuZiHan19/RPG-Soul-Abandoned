using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeleton_StunnedState : EnemySkeletonState
{
    public EnemySkeleton_StunnedState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = skeleton.stunnedDuration;
    }

    public override void Exit()
    {
        base.Exit();
        skeleton.Fx.Invoke("CancelBlink", 0);
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
            stateMachine.ChangeState(skeleton.battleState);
    }
}
