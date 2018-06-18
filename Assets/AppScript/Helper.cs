using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonHelper
{
    public static T[] getJsonArray<T>(string json)
    {
        string newJson = "{ \"array\": " + json + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
        return wrapper.array;
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] array;
    }
}

[System.Serializable]
public class JPertanyaan
{
    public int id;
    public string soal;
    public string jawaban;
    public string museum;
    public string petunjuk;
}
[System.Serializable]
public class JMuseum
{
    public int id;
    public string museum;
}
[System.Serializable]
public class JVersion
{
    public double version;
}