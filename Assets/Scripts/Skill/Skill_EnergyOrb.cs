using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_EnergyOrb : Skill
{
    [Header("Unlock Info")]
    public bool isUnlock;

    [Header("EnergyOrb")]
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float radius;
    [SerializeField] private float number;
    [SerializeField] private float waitTime;
    [SerializeField] private float aliveDuration;

    public IEnumerator Release(Transform trans)
    {
        for (int i = 0; i < number; i++)
        {
            GameObject newObj = Instantiate(Resources.Load<GameObject>(ResConst.Skill.EnergyOrb), trans.position, Quaternion.identity);
            newObj.GetComponent<EnergyOrbController>().Setup(rotationSpeed, radius, trans, aliveDuration);
            yield return new WaitForSeconds(waitTime);
        }
    }
}
