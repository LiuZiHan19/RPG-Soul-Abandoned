using UnityEngine;

public class ResourceLoader : Singleton<ResourceLoader>
{
    public T LoadRes<T>(string path) where T : Object
    {
        T res = Resources.Load<T>(path);
        if (res == null)
        {
            Logger.LogError("LoadByResource failed. Path: " + path);
            return null;
        }

        return res;
    }

    public GameObject LoadObject(string path)
    {
        GameObject obj = Resources.Load<GameObject>(path);
        if (obj == null)
        {
            Logger.LogError("LoadObject failed. Path: " + path);
            return null;
        }

        var insObj = GameObject.Instantiate(obj);
        return insObj;
    }
}