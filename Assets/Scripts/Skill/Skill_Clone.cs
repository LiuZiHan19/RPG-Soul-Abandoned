using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Clone : Skill
{
    [Header("Unlock Info")]
    public bool isUnlock;
    public bool isUnlockDuplicate;

    [Header("Clone")]
    [SerializeField] private float losingSpeed;
    [SerializeField] private float aliveDuration;
    [SerializeField] private float duplicateChance;

    public void Release(Vector3 position)
    {
        GameObject clone = Instantiate(Resources.Load<GameObject>(ResConst.Skill.SkillClone), position, Quaternion.identity);
        clone.GetComponent<PlayerCloneController>().Setup(losingSpeed, aliveDuration, duplicateChance);
    }
}
