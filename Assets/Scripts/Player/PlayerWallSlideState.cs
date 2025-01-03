using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(player.wallJumpState);
            return;
        }

        if (player.yInput < 0)
            player.SetVelocity(player.rb.velocity.x, player.rb.velocity.y * player.wallSlideAccelerateSpeed);
        else
            player.SetVelocity(player.rb.velocity.x, player.rb.velocity.y * player.wallSlideSpeed);

        if (player.CanLeaveWallSlide()) stateMachine.ChangeState(player.idleState);
    }
}
