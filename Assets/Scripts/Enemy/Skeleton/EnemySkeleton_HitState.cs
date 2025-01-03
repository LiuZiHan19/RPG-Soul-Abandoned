using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeleton_HitState : EnemySkeletonState
{
    public EnemySkeleton_HitState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }
}
