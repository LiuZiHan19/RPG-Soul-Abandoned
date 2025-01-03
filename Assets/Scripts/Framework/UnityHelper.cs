using UnityEngine;

public static class UnityHelper
{
    #region Time Management

    public static void PauseTime()
    {
        Time.timeScale = 0f;
    }

    public static void ResumeTime()
    {
        Time.timeScale = 1f;
    }

    public static void SetTimeScale(float scale)
    {
        Time.timeScale = scale;
    }

    #endregion

    public static void SetParent(Transform parent, Transform child, bool worldPosStays = false)
    {
        if (parent == null)
        {
            Logger.LogWarning("Set parent failed. The parent is null.");
            return;
        }

        if (child == null)
        {
            Logger.LogWarning("Set parent failed. The child is null.");
            return;
        }

        child.SetParent(parent, worldPosStays);
    }
}