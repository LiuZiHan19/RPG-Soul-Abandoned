using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player,
        stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();
        player.SetVelocity(player.xInput * player.moveSpeed, player.rb.velocity.y);
        if (player.xInput == 0) stateMachine.ChangeState(player.idleState);
    }
}