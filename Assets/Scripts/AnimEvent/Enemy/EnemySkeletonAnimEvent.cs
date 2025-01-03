using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeletonAnimEvent : EnemyAnimEvent
{
    private Enemy_Skeleton skeleton;
    protected override void Awake()
    {
        base.Awake();
        skeleton = enemy as Enemy_Skeleton;
    }

    private void CanStunned() => skeleton.CanStunned();
    private void CannotStunned() => skeleton.CannotStunned();
}
