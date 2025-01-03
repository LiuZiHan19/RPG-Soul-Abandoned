using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCloneAnimEvent : MonoBehaviour
{
    private PlayerCloneController playerCloneController;

    private void Awake()
    {
        playerCloneController = GetComponentInParent<PlayerCloneController>();
    }

    private void AnimAttack() => playerCloneController.Attack();
}
