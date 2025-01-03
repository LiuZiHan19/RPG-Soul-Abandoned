using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Skill_ThrowSword : Skill
{
    [Header("Unlock Info")]
    public bool isUnlock;
    public bool isUnlockBounce;
    public bool isUnlockPierce;
    public bool isUnlockSpin;
    public bool hasSword;

    [Header("ThrowSword")]
    public ThrowSwordController curThrowSwordController;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float returnSpeed;
    private Vector2 dir;

    protected override void Start()
    {
        base.Start();
        hasSword = true;
    }

    public void Release(Vector3 position)
    {
        SetSword(false);
        GameObject throwSword = Instantiate(Resources.Load<GameObject>(ResConst.Skill.ThrowSword), position, Quaternion.identity);
        curThrowSwordController = throwSword.GetComponent<ThrowSwordController>();
        curThrowSwordController.Setup(moveSpeed, dir, returnSpeed);
    }

    public void SetupDir(Vector2 dir) => this.dir = dir;

    public void SetSword(bool hasSword) => this.hasSword = hasSword;
}