using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if (player.Skill.Dash.isUnlockStartClone)
            player.Skill.Clone.Release(player.transform.position);
        stateTimer = player.dashDuration;
    }

    public override void Exit()
    {
        base.Exit();
        player.SetVelocity(0, player.rb.velocity.y);
        if (player.Skill.Dash.isUnlockEndClone)
            player.Skill.Clone.Release(player.transform.position);
    }

    public override void Update()
    {
        base.Update();

        player.SetVelocity(player.DashDir * player.dashSpeed, 0);

        if (stateTimer < 0) stateMachine.ChangeState(player.idleState);

        if (player.CanDashToWallSlide())
            stateMachine.ChangeState(player.wallSlideState);
    }
}
