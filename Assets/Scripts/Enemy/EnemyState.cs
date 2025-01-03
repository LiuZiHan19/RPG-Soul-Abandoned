using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected float stateTimer;
    protected string animBoolName;
    protected Rigidbody2D rb;
    protected Enemy enemy;
    public bool triggerCalled;
    protected EnemyStateMachine stateMachine;
    protected Player player;

    public EnemyState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName)
    {
        this.enemy = enemy;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        enemy.Animator.SetBool(animBoolName, true);
        player = PlayerManager.Instance.player;
        rb = enemy.rb;
        triggerCalled = false;
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
    }

    public virtual void Exit()
    {
        enemy.Animator.SetBool(animBoolName, false);
    }
}
