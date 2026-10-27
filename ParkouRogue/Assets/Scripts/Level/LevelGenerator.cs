using Chapter.State;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    //public short door = 0; //1 = Right, 2 = Up, Negative = Reverse

    [SerializeField]
    private LevelHandler levelHandler;
    [SerializeField]
    private GameObject Level_Prefabs;

    private static Dictionary<GameObject, int> LevelPrefabs = new Dictionary<GameObject, int>(); 
    private const string TargetScriptType = "LevelController";

    private void Start()
    {

        foreach (Transform level in Level_Prefabs.transform)
        {
            LevelPrefabs.Add(level.gameObject, level.GetComponent<LevelController>().GetWeight());
        }

        /*
        string[] guids = AssetDatabase.FindAssets("t:Prefab");

        foreach (string guid in guids)
        {

            string path = AssetDatabase.GUIDToAssetPath(guid);

            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);

            if (prefab != null)
            {
                if (prefab.GetComponentInChildren(System.Type.GetType(TargetScriptType)) != null || prefab.GetComponent(TargetScriptType) != null)
                {
                    Debug.Log("Prefab");
                    LevelPrefabs.Add(prefab, prefab.GetComponent<LevelController>().GetWeight());
                }
            }

        }
        */

    }

    public void GenerateLevel(short door)
    {
        SpawnLevel(door, DecideLevel());
    }

    public bool SpawnLevel(short door, GameObject level)
    {

        Vector3Int loc = levelHandler.GetCurLevelPos();
        if (door == 0)
        {
            return false;
        }
        else if (door % 2 == 0)
        {
            loc += new Vector3Int(0, door / 2, 0);
        }
        else
        {
            loc += new Vector3Int(door, 0, 0);
        }

        if (loc.x < 0 || loc.y < 0 || levelHandler.CheckLevel(loc))
            return false;

        GameObject space = new GameObject("Level " + loc.x + "," + loc.y);
        space.transform.SetParent(this.transform, true);

        space.transform.localPosition = loc;
        space.transform.localScale = Vector3.one;

        GameObject spawnedLevel = Instantiate(level);
        spawnedLevel.transform.SetParent(space.transform, false);

        spawnedLevel.transform.localPosition = Vector3.zero;

        levelHandler.AddLevel(space);

        return true;
    }

    public GameObject DecideLevel()
    {
        int weights = 0;
        foreach (var (level, weight) in LevelPrefabs)
        {
            weights += weight;
        }

        weights = Random.Range(0, weights) + 1;

        foreach (var (level, weight) in LevelPrefabs)
        {
            weights -= weight;
            if (weights <= 0)
            {
                return level;
            }
        }
        return null;
    }

}
