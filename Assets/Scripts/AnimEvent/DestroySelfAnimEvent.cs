using UnityEngine;

public class DestroySelfAnimEvent : MonoBehaviour
{
    public void DestroySelf()
    {
        Destroy(transform.parent.gameObject);
    }
}