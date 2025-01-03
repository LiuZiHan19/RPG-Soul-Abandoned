using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCatchState : PlayerState
{
    public PlayerCatchState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player,
        stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetFlip(false);
    }

    public override void Update()
    {
        base.Update();
        if (triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
        else
        {
            player.SetVelocity(-player.FacingDir * 5f, player.rb.velocity.y);
        }
    }

    public override void Exit()
    {
        base.Exit();
        player.SetFlip(true);
    }
}