using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player,
        stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        player.AttackBegin();
        player.SlightMove();
        stateTimer = .1f;
    }

    public override void Exit()
    {
        base.Exit();
        player.AttackOver();
        player.SetBusy(false);
        // player.StartCoroutine("BusyFor", .17f);
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
            player.ZeroVelocity();
        if (triggerCalled) stateMachine.ChangeState(player.idleState);
    }
}