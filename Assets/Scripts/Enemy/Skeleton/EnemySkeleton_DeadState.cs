using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeleton_DeadState : EnemySkeletonState
{
    public EnemySkeleton_DeadState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }
}
