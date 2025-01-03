using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCounterAttackSucessfulState : PlayerState
{
    public PlayerCounterAttackSucessfulState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();
        if (triggerCalled)
            stateMachine.ChangeState(player.idleState);
    }
}
