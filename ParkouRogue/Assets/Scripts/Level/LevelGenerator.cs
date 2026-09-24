using Chapter.State;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    public short door = 0; //1 = Right, 2 = Up, Negative = Reverse

    private static readonly IDictionary<GameObject, int> LevelPrefabs;
    private const string TargetScriptType = "LevelController";

    private void Start()
    {

        string[] guids = AssetDatabase.FindAssets("t:Prefabs");

        foreach (string guid in guids)
        {

            string path = AssetDatabase.GUIDToAssetPath(guid);

            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);

            if (prefab != null)
            {
                if (prefab.GetComponentInChildren(System.Type.GetType(TargetScriptType)) != null || prefab.GetComponent(TargetScriptType) != null)
                {
                    LevelPrefabs.Add(prefab, prefab.GetComponent<LevelController>().GetWeight());
                }
            }

        }

    }

    public bool GenerateLevel(GameObject Level)
    {
        GameObject level = new GameObject("Level " + ",");

        return false;
    }

}
