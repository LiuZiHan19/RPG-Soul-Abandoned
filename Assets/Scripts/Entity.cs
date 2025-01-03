using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public abstract class Entity : MonoBehaviour
{
    public Vector2 knockbackForce;
    public Vector2 thunderStrikeForce;

    public bool IsFlip { get; private set; } = true;
    public bool IsBusy { get; private set; }
    public int FacingDir { get; private set; } = 1;
    public bool IsFacingRight { get; private set; } = true;
    public UnityAction OnFlipped { get; set; }
    public bool isDead;

    public EntityFx Fx { get; private set; }
    public Animator Animator { get; private set; }
    public Rigidbody2D rb { get; private set; }

    [Header("Check")] [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected float groundCheckDisance;
    [SerializeField] protected LayerMask whatIsWall;
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected Transform attackCheck;
    [SerializeField] protected float attackRadius;

    protected virtual void Awake()
    {
        ParseComponent();
    }

    protected virtual void Start()
    {
        InitStates();
    }

    protected virtual void Update()
    {
    }

    #region Velocity

    public virtual void SetVelocity(float x, float y)
    {
        if (IsBusy) return;
        rb.velocity = new Vector2(x, y);
        FlipController();
    }

    public void ZeroVelocity()
    {
        if (IsBusy) return;
        rb.velocity = Vector2.zero;
    }

    #endregion

    #region Flip

    public void SetFlip(bool isFlip) => IsFlip = isFlip;

    public void Flip()
    {
        if (IsFlip == false) return;
        FacingDir = -FacingDir;
        IsFacingRight = !IsFacingRight;
        transform.Rotate(0, 180, 0);
        OnFlipped?.Invoke();
    }

    protected void FlipController()
    {
        if (rb.velocity.x > 0 && IsFacingRight == false || rb.velocity.x < 0 && IsFacingRight) Flip();
    }

    #endregion

    #region Knockback

    public void Knockback(int attackerFacingDir, Vector2 force) =>
        StartCoroutine(KnockbackCor(attackerFacingDir, force));

    private IEnumerator KnockbackCor(int attackerFacingDir, Vector2 force)
    {
        IsFlip = false;
        IsBusy = true;
        SetVelocity(force.x * attackerFacingDir, force.y);
        yield return new WaitForSeconds(0.25f);
        IsBusy = false;
        IsFlip = true;

        ZeroVelocity();
    }

    #endregion

    #region Check

    public bool IsGroundDetected()
        => Physics2D.Raycast(groundCheck.position, Vector3.down, groundCheckDisance, whatIsGround);

    public bool IsWallDetected()
        => Physics2D.Raycast(wallCheck.position, Vector3.right * FacingDir, wallCheckDistance, whatIsWall);

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * groundCheckDisance);
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3.right * FacingDir * wallCheckDistance));
        Gizmos.DrawWireSphere(attackCheck.position, attackRadius);
    }

    public Collider2D[] AttackCheck() => Physics2D.OverlapCircleAll(attackCheck.position, attackRadius);

    #endregion

    #region Elements

    public virtual void ApplyIgnite()
    {
        RemoveIgnite();
        RemoveIce();
        RemoveThunder();
        var obj = Instantiate(Resources.Load<GameObject>("Fx/IgniteStrikeFx"));
        obj.GetComponent<IgniteStrikeFx>().Setup(transform);
    }

    public virtual void RemoveIgnite()
    {
    }

    public virtual void ApplyIce()
    {
        RemoveIgnite();
        RemoveIce();
        RemoveThunder();
        var obj = Instantiate(Resources.Load<GameObject>("Fx/IceStrikeFx"));
        obj.GetComponent<IceStrikeFx>().Setup(transform);
    }

    public virtual void RemoveIce()
    {
    }

    public virtual void ApplyThunder()
    {
        RemoveIgnite();
        RemoveIce();
        RemoveThunder();
        var obj = Instantiate(Resources.Load<GameObject>("Fx/ThunderStrikeFx"));
        obj.GetComponent<ThunderStrikeFx>().Setup(transform);
    }

    public virtual void RemoveThunder()
    {
    }

    #endregion

    #region Busy

    public IEnumerator BusyFor(float busyTime)
    {
        IsBusy = true;
        yield return new WaitForSeconds(busyTime);
        IsBusy = false;
    }

    public void SetBusy(bool isBusy) => IsBusy = isBusy;

    #endregion

    public virtual void Die()
    {
        isDead = true;
    }

    protected virtual void ParseComponent()
    {
        Animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        Fx = GetComponent<EntityFx>();
    }

    protected virtual void InitStates()
    {
    }

    protected virtual void Dispose()
    {
    }

    protected virtual void OnDestroy()
    {
        Dispose();
    }
}