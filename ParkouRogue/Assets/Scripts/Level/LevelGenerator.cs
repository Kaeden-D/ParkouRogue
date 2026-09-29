using Chapter.State;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    public short door = 0; //1 = Right, 2 = Up, Negative = Reverse

    [SerializeField]
    private LevelHandler levelHandler;

    private static Dictionary<GameObject, int> LevelPrefabs = new Dictionary<GameObject, int>(); 
    private const string TargetScriptType = "LevelController";

    private void Start()
    {

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

    }

    public bool GenerateLevel(GameObject level)
    {

        Vector3 loc = levelHandler.GetCurLevelPos();
        if (door % 2 == 0)
        {
            loc += new Vector3(0f, door / 2f, 0f);
        }
        else
        {
            loc += new Vector3(door, 0f, 0f);
        }

        if (loc.x < 0 || loc.y < 0)
            return false;

        GameObject space = new GameObject("Level " + loc.x + "," + loc.y);
        space.transform.SetParent(this.transform, true);

        Instantiate(level).transform.SetParent(space.transform, false);
        
        space.transform.localPosition = loc;

        return true;
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Spawn Level"))
            GenerateLevel(LevelPrefabs.Keys.ElementAt(0));
    }

}
