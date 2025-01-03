using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_ElectricOrb : Skill
{
    [Header("Unlock Info")]
    public bool isUnlock;

    [Header("ElectricOrb")]
    [SerializeField] private int number;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float aliveDuration;

    public void Release(Transform trans)
    {
        float averageAngle = 360 / number;
        for (int i = 0; i < number; i++)
        {
            GameObject orb = Instantiate(Resources.Load<GameObject>(ResConst.Skill.ElectricOrb), trans.position,
                                          trans.rotation * Quaternion.AngleAxis(averageAngle * i, Vector3.forward));
            orb.GetComponent<ElectricOrbController>().Setup(moveSpeed, aliveDuration);
        }
    }
}
