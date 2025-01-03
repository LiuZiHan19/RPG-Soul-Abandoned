using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeleton_BattleState : EnemySkeletonState
{
    private float angryCounter;

    public EnemySkeleton_BattleState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        angryCounter = skeleton.angryTime;
    }

    public override void Update()
    {
        base.Update();

        if (enemy.CanAttack())
            stateMachine.ChangeState(skeleton.attackState);

        skeleton.ChaseFlip();

        if (skeleton.IsPlayerDetected() == false)
            angryCounter -= Time.deltaTime;
        if (angryCounter < 0)
            stateMachine.ChangeState(skeleton.idleState);

        skeleton.SetVelocity(skeleton.FacingDir * skeleton.battleMoveSpeed, rb.velocity.y);
    }
}
