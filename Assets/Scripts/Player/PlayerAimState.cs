using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimState : PlayerState
{
    public PlayerAimState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player,
        stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();
        player.ZeroVelocity();
        Vector2 mouseScreenPos = Input.mousePosition;
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        
        if (mouseWorldPos.x > player.transform.position.x && player.IsFacingRight == false ||
            mouseWorldPos.x < player.transform.position.x && player.IsFacingRight)
        {
            player.Flip();
        }

        if (Input.GetMouseButtonUp(1))
        {
            //在松手时计算向量
            Vector2 dir = (mouseWorldPos - (Vector2)player.transform.position).normalized;
            player.Skill.ThrowSword.SetupDir(dir);
            stateMachine.ChangeState(player.throwState);
        }
    }
}