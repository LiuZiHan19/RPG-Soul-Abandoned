using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeletonState : EnemyState
{
    protected Enemy_Skeleton skeleton;

    public EnemySkeletonState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        skeleton = enemy as Enemy_Skeleton;
    }
}
