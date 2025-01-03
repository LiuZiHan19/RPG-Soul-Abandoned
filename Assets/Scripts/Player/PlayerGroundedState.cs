using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player,
        stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        // AirState
        if (player.IsGroundDetected() == false) stateMachine.ChangeState(player.airState);

        // JumpState
        if (Input.GetKeyDown(KeyCode.Space) && player.IsGroundDetected()) stateMachine.ChangeState(player.jumpState);

        // AttackState
        if (Input.GetMouseButtonDown(0)) stateMachine.ChangeState(player.attackState);

        // Cunterattack
        if (Input.GetKeyDown(KeyCode.Q) && player.Skill.CounterAttack.isUnlock &&
            player.Skill.CounterAttack.CanUseSkill()) stateMachine.ChangeState(player.counterAttackState);

        // AimState
        if (Input.GetMouseButton(1) && player.Skill.ThrowSword.isUnlock && player.Skill.ThrowSword.CanUseSkill() &&
            player.Skill.ThrowSword.hasSword) stateMachine.ChangeState(player.aimState);

        // return sword
        if (Input.GetMouseButtonDown(1) && player.Skill.ThrowSword.hasSword == false)
            player.Skill.ThrowSword.curThrowSwordController.Return();
    }
}