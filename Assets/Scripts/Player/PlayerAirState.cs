using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (player.IsWallDetected()) stateMachine.ChangeState(player.wallSlideState);

        if (player.xInput != 0) player.SetVelocity(player.moveSpeed * player.xInput, player.rb.velocity.y);

        if (player.IsGroundDetected())
            stateMachine.ChangeState(player.idleState);
    }
}
