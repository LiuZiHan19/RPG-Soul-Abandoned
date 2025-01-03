using UnityEngine;

public class PlayerCloneController : MonoBehaviour
{
    [SerializeField] private float attackRadius;
    [SerializeField] private Transform attackCheck;
    [SerializeField] private float findClosestRadius;
    [SerializeField] private float duplicateChance;
    [SerializeField] private LayerMask whatIsEnemy;

    private float losingSpeed;
    private float aliveDuration;
    private float aliveCounter;
    private Transform closestTarget;

    private SpriteRenderer sr;
    private Animator animator;

    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        aliveCounter = aliveDuration;
    }

    private void Update()
    {
        Losing();
    }

    #region Setup

    public void Setup(float losingSpeed, float aliveDuration, float duplicateChance)
    {
        this.losingSpeed = losingSpeed;
        this.aliveDuration = aliveDuration;
        this.duplicateChance = duplicateChance;
        animator.SetInteger("Attack", Random.Range(0, 3));
        FindCloestTarget();
    }

    #endregion

    #region Losing

    private void Losing()
    {
        aliveCounter -= Time.deltaTime;
        if (aliveCounter < 0)
        {
            sr.color = new Color(255, 255, 255, sr.color.a - losingSpeed * Time.deltaTime);
            if (sr.color.a <= 0)
                Destroy(gameObject);
        }
    }

    #endregion

    #region Attack

    public void Attack()
    {
        bool isAttacked = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackCheck.position, attackRadius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.GetComponent<Enemy>())
            {
                isAttacked = true;
                PlayerManager.Instance.player.Stats.DoMagicDamage(collider.GetComponent<Enemy>().Stats);
            }
        }

        if (isAttacked && SkillManager.Instance.Clone.isUnlockDuplicate)
        {
            float chance = Random.value;
            if (chance < duplicateChance)
                CloneDuplicate();
        }
    }

    #endregion

    #region FindCloestTarget

    private void FindCloestTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, findClosestRadius, whatIsEnemy);
        foreach (Collider2D cd in colliders)
        {
            if (closestTarget == null)
                closestTarget = cd.transform;

            if (Vector2.Distance(transform.position, cd.transform.position) <
                Vector2.Distance(transform.position, closestTarget.position))
                closestTarget = cd.transform;
        }

        if (closestTarget == null)
            return;
        if (closestTarget.position.x < transform.position.x)
            transform.Rotate(0, 180, 0);
    }

    #endregion

    #region CloneDuplicate

    private void CloneDuplicate()
    {
        Vector3 dir = closestTarget.position.x < transform.position.x ? Vector3.left : Vector3.right;
        SkillManager.Instance.Clone.Release(closestTarget.position + dir);
    }

    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackCheck.position, attackRadius);
        Gizmos.DrawWireSphere(transform.position, findClosestRadius);
    }
}