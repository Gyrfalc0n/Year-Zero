using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MyTools : MonoBehaviour
{
    public static string GetPath(GameObject obj)
    {
        string rawPath = AssetDatabase.GetAssetPath(obj);
        rawPath = rawPath.Substring(17);
        rawPath = rawPath.Substring(0, rawPath.Length - 7);
        return rawPath;
    }
}
