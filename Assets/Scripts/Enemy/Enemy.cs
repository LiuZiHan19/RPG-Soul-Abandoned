using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using static UnityEngine.EventSystems.EventTrigger;

public class Enemy : Entity
{
    #region Check

    [SerializeField] protected float backCheckDistance;
    [SerializeField] protected float playerCheckDistance;
    [SerializeField] protected Transform playerCheck;
    [SerializeField] protected LayerMask whatIsPlayer;

    #endregion

    [SerializeField] private float _healthBarHeight;
    [Space(10)] public float idleTime;
    public float moveSpeed;
    public float battleMoveSpeed;
    public float attackDistance;
    public float angryTime;
    [SerializeField] protected int dropTimes;
    private float _defaultMoveSpeed;
    private float _defaultBattleMoveSpeed;

    public Player player { get; private set; }
    public EnemyStats Stats { get; private set; }
    public EnemyStateMachine stateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        Stats = GetComponent<EnemyStats>();
    }

    protected override void Start()
    {
        base.Start();
        CreateHealthBar();
        _defaultMoveSpeed = moveSpeed;
        _defaultBattleMoveSpeed = battleMoveSpeed;
    }

    public override void ApplyIce()
    {
        base.ApplyIce();
        moveSpeed *= .7f;
        battleMoveSpeed *= .7f;
        Animator.speed = .7f;
    }

    public override void RemoveIce()
    {
        base.RemoveIce();
        moveSpeed = _defaultMoveSpeed;
        battleMoveSpeed = _defaultBattleMoveSpeed;
        Animator.speed = 1;
    }

    protected override void InitStates()
    {
        player = PlayerManager.Instance.player;
        stateMachine = new EnemyStateMachine();
        base.InitStates();
    }

    public void ChaseFlip()
    {
        if (player.transform.position.x > transform.position.x && IsFacingRight == false ||
            player.transform.position.x < transform.position.x && IsFacingRight)
            Flip();
    }

    public bool CanAttack()
        => Vector2.Distance(transform.position, player.transform.position) < attackDistance ? true : false;

    public override void Die()
    {
        base.Die();
        DropItem();
    }

    #region Check

    public bool IsPlayerDetected()
    {
        Vector3 dir = Vector3.right * FacingDir;
        return Physics2D.Raycast(playerCheck.position + -dir * backCheckDistance, dir, playerCheckDistance,
            whatIsPlayer);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Vector3 dir = Vector3.right * FacingDir;
        Gizmos.DrawLine(playerCheck.position + -dir * backCheckDistance,
            playerCheck.position + dir * playerCheckDistance + -dir * backCheckDistance);
    }

    #endregion

    private void CreateHealthBar()
    {
        GameObject healthBar = ResourceLoader.Instance.LoadObject(ResConst.GameView.HealthBarView);
        healthBar.transform.SetParent(transform, false);
        healthBar.transform.position =
            new Vector3(transform.position.x, transform.position.y + _healthBarHeight, transform.position.z);
        healthBar.GetComponent<HealthBarView>().Initialize();
    }

    protected virtual void DropItem()
    {
    }
}