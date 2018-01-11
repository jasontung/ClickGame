using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GameDataBase
{
    public static void Save(string key, object data)
    {
        string jsonStr = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(key, jsonStr);
    }

    public static T Load<T>(string key)
    {
        string jsonStr = PlayerPrefs.GetString(key, string.Empty);
        if (string.IsNullOrEmpty(jsonStr))
            return Activator.CreateInstance<T>();
        return JsonUtility.FromJson<T>(jsonStr);
    }

    public static void Clear()
    {
        PlayerPrefs.DeleteAll();
    }
}
