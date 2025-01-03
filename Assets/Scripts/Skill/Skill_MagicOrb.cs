using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_MagicOrb : Skill
{
    [Header("Unlock Info")]
    public bool isUnlock;

    [Header("MagicOrb")]
    [SerializeField] float moveSpeed;
    [SerializeField] float sinChangeSpeed;
    [SerializeField] float moveSize;
    [SerializeField] float aliveDuration;

    public void Release(Transform trans)
    {
        GameObject orb = Instantiate(Resources.Load<GameObject>(ResConst.Skill.MagicOrb), trans.position, Quaternion.identity);
        GameObject orb2 = Instantiate(Resources.Load<GameObject>(ResConst.Skill.MagicOrb), trans.position, Quaternion.identity);
        orb.GetComponent<MagicOrbController>().Setup(moveSpeed, sinChangeSpeed, moveSize, aliveDuration);
        orb2.GetComponent<MagicOrbController>().Setup(moveSpeed, sinChangeSpeed, moveSize, aliveDuration);
        orb2.transform.Rotate(0, 180, 0);
    }
}
