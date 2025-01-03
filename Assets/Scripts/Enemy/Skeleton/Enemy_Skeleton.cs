using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Skeleton : Enemy
{
    #region States

    public EnemySkeleton_IdleState idleState { get; private set; }
    public EnemySkeleton_WalkState walkState { get; private set; }
    public EnemySkeleton_HitState hitState { get; private set; }
    public EnemySkeleton_AttackState attackState { get; private set; }
    public EnemySkeleton_DeadState deadState { get; private set; }
    public EnemySkeleton_BattleState battleState { get; private set; }
    public EnemySkeleton_StunnedState stunnedState { get; private set; }

    #endregion

    #region Stunned

    [Header("Stunned")] [SerializeField] private GameObject stunnedWindow;
    public bool canBeStunned;
    public float stunnedDuration;
    public Vector2 stunnedForce;

    #endregion

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
    }

    protected override void InitStates()
    {
        base.InitStates();

        idleState = new EnemySkeleton_IdleState(this, stateMachine, "Idle");
        walkState = new EnemySkeleton_WalkState(this, stateMachine, "Walk");
        hitState = new EnemySkeleton_HitState(this, stateMachine, "Hit");
        attackState = new EnemySkeleton_AttackState(this, stateMachine, "Attack");
        deadState = new EnemySkeleton_DeadState(this, stateMachine, "Dead");
        battleState = new EnemySkeleton_BattleState(this, stateMachine, "Walk");
        stunnedState = new EnemySkeleton_StunnedState(this, stateMachine, "Stunned");
    }

    #region Stunned

    public void CanStunned()
    {
        canBeStunned = true;
        stunnedWindow.SetActive(true);
    }

    public void CannotStunned()
    {
        canBeStunned = false;
        CloseStunnedWindow();
    }

    public void Stunned()
    {
        CloseStunnedWindow();
        Fx.InvokeRepeating("RedBlink", 0, .1f);
        stateMachine.ChangeState(stunnedState);
        SetVelocity(stunnedForce.x * PlayerManager.Instance.player.FacingDir, stunnedForce.y);
    }

    private void CloseStunnedWindow()
    {
        stunnedWindow.SetActive(false);
    }

    #endregion

    public override void Die()
    {
        if (isDead) return;
        base.Die();
        stateMachine.ChangeState(deadState);
    }

    protected override void DropItem()
    {
        base.DropItem();
        for (int i = 0; i < dropTimes; i++)
        {
            var itemObj = Instantiate(Resources.Load<GameObject>(ResConst.InventoryItem), transform.position,
                Quaternion.identity);
            var itemSc = itemObj.GetComponent<InventoryWorldItem>();
            itemSc.Refresh(DataHelper.LoadItemDataRandomly());
            itemSc.Pop();
        }
    }
}