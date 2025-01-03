using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimBaseEvent : MonoBehaviour
{
    protected Entity entity;

    protected virtual void Awake() => entity = GetComponentInParent<Entity>();

    protected abstract void AnimTrigger();
    protected abstract void AnimAttack();
}
