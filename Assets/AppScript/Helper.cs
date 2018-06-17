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
    public int id { get; set; }
    public string soal { get; set; }
    public string jawaban { get; set; }
    public string museum { get; set; }
    public string petunjuk { get; set; }
}
[System.Serializable]
public class JMuseum
{
    public int id { get; set; }
    public string museum { get; set; }
}
[System.Serializable]
public class JVersion
{
    public double version { get; set; }
}