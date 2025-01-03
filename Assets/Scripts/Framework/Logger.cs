using System;
using UnityEngine;

public static class Logger
{
    public static void Log(object message)
    {
        Debug.Log(FormatLogMessage("INFO", message));
    }

    public static void LogWarning(object message)
    {
        Debug.LogWarning(FormatLogMessage("WARNING", message));
    }

    public static void LogError(object message)
    {
        Debug.LogError(FormatLogMessage("ERROR", message));
    }

    public static void LogFormat(string format, params object[] args)
    {
        Debug.LogFormat(FormatLogMessage("INFO", string.Format(format, args)));
    }

    private static string FormatLogMessage(string logLevel, object message)
    {
        return $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{logLevel}] {message}";
    }
}