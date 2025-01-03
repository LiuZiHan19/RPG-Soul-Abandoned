using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public bool interactable = true;

    public PlayerStats Stats { get; private set; }
    [Header("Basic")] public float moveSpeed;
    public float jumpForce;
    public float xInput { get; private set; }
    public float yInput { get; private set; }
    public SkillManager Skill { get; private set; }

    [Header("Dash")] public float dashDuration;
    public float dashSpeed;
    public float DashDir { get; private set; }

    [Header("WallSlide")] public float wallSlideSpeed;
    public float wallSlideAccelerateSpeed;

    [Header("WallJump")] public Vector2 wallJumpForce;

    [Header("Attack")] public Vector2[] slightMove;
    [SerializeField] private float resetTime;
    private float lastAttackTime;
    private int combo;

    private float _defaultMoveSpeed;
    private float _defaultJumpForce;
    private float _desfaultDashSpeed;
    private Vector2 _defaultWallJumpForce;
    private float _defaultWallSlideSpeed;
    private float _defaultWallSlideAccelerateSpeed;

    #region State

    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }
    public PlayerAttackState attackState { get; private set; }
    public PlayerCounterAttackState counterAttackState { get; private set; }
    public PlayerCounterAttackSucessfulState counterAttackSucessfulState { get; private set; }
    public PlayerAimState aimState { get; private set; }
    public PlayerThrowState throwState { get; private set; }
    public PlayerCatchState catchState { get; private set; }
    public PlayerDeadState deadState { get; private set; }

    #endregion

    protected override void Awake()
    {
        base.Awake();
        Stats = GetComponent<PlayerStats>();
        _defaultMoveSpeed = moveSpeed;
        _defaultJumpForce = jumpForce;
        _desfaultDashSpeed = dashSpeed;
        _defaultWallJumpForce = wallJumpForce;
        _defaultWallSlideSpeed = wallSlideSpeed;
        _defaultWallSlideAccelerateSpeed = wallSlideAccelerateSpeed;
    }

    protected override void Start()
    {
        base.Start();
        Skill = SkillManager.Instance;
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        if (interactable == false) return;
        base.Update();
        stateMachine.currentState.Update();
        GetInput();
        CheckForDash();
        CheckForMagicOrb();
        CheckForElectricOrb();
        CheckForEnergyOrb();
    }

    private void GetInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
    }

    #region Dash

    private void CheckForDash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && Skill.Dash.isUnlock && Skill.Dash.CanUseSkill() &&
            IsWallDetected() == false)
        {
            DashDir = Input.GetAxisRaw("Horizontal");
            if (DashDir != 0) stateMachine.ChangeState(dashState);
        }
    }

    #endregion

    #region Magic Energy Electric Orb

    public void CheckForMagicOrb()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (Skill.MagicOrb.isUnlock && Skill.MagicOrb.CanUseSkill())
            {
                Skill.MagicOrb.Release(transform);
            }
        }
    }

    public void CheckForEnergyOrb()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (Skill.EnergyOrb.isUnlock && Skill.EnergyOrb.CanUseSkill())
            {
                Skill.EnergyOrb.StartCoroutine("Release", transform);
            }
        }
    }

    public void CheckForElectricOrb()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (Skill.ElectricOrb.isUnlock && Skill.ElectricOrb.CanUseSkill())
            {
                Skill.ElectricOrb.Release(transform);
            }
        }
    }

    #endregion

    #region Attack

    public void AttackBegin()
    {
        //Attack Interval
        if (lastAttackTime + resetTime < Time.time)
            combo = 0;
        Animator.SetInteger("Combo", combo);
    }

    public void AttackOver()
    {
        combo++;
        if (combo > 2)
            combo = 0;
        lastAttackTime = Time.time;
    }

    public void SlightMove() => SetVelocity(slightMove[combo].x * FacingDir, slightMove[combo].y);

    #endregion

    #region Can Is

    public bool CanLeaveWallSlide()
    {
        if (IsGroundDetected() || (xInput != 0 && xInput != FacingDir) || IsWallDetected() == false)
        {
            return true;
        }

        return false;
    }

    public bool CanDashToWallSlide()
    {
        if (IsGroundDetected() == false && IsWallDetected())
            return true;
        return false;
    }

    #endregion

    protected override void InitStates()
    {
        base.InitStates();
        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Air");
        airState = new PlayerAirState(this, stateMachine, "Air");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallSlideState = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "Air");
        attackState = new PlayerAttackState(this, stateMachine, "Attack");
        counterAttackState = new PlayerCounterAttackState(this, stateMachine, "CounterAttack");
        counterAttackSucessfulState =
            new PlayerCounterAttackSucessfulState(this, stateMachine, "CounterAttackSucessful");
        aimState = new PlayerAimState(this, stateMachine, "Aim");
        throwState = new PlayerThrowState(this, stateMachine, "Throw");
        catchState = new PlayerCatchState(this, stateMachine, "Catch");
        deadState = new PlayerDeadState(this, stateMachine, "Dead");
    }

    public override void Die()
    {
        stateMachine.ChangeState(deadState);
    }

    public override void ApplyIce()
    {
        RemoveIce();
        moveSpeed *= .7f;
        jumpForce *= .7f;
        dashSpeed *= .7f;
        wallJumpForce *= .7f;
        wallSlideSpeed *= .7f;
        wallSlideAccelerateSpeed *= .7f;
        Animator.speed = .7f;
    }

    public override void RemoveIce()
    {
        moveSpeed = _defaultMoveSpeed;
        jumpForce = _defaultJumpForce;
        dashSpeed = _desfaultDashSpeed;
        wallJumpForce = _defaultWallJumpForce;
        wallSlideSpeed = _defaultWallSlideSpeed;
        wallSlideAccelerateSpeed = _defaultWallSlideAccelerateSpeed;
        Animator.speed = 1;
    }

    public override void SetVelocity(float x, float y)
    {
        rb.velocity = new Vector2(x, y);
        FlipController();
    }

    public void SwordReturned(Transform sword)
    {
        if (sword.position.x > transform.position.x && IsFacingRight == false ||
            sword.position.x < transform.position.x && IsFacingRight) Flip();

        Skill.ThrowSword.SetSword(true);
        stateMachine.ChangeState(catchState);
    }
}