using Chapter.State;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{

    [SerializeField]
    private PlayerController player;

    [SerializeField]
    private LevelGenerator levelGenerator;

    private static Dictionary<Vector3Int, GameObject> Levels = new Dictionary<Vector3Int, GameObject>();

    private void Start()
    {
        AddLevel(this.transform.Find("Level 0,0").gameObject);
    }

    public void LevelGenerate(short door)
    {
        levelGenerator.GenerateLevel(door);
    }

    public void AddLevel(GameObject level)
    {
        if (CheckLevel(Vector3Int.RoundToInt(level.transform.position)))
            return;

        Levels.Add(Vector3Int.RoundToInt(level.transform.localPosition), level.transform.GetChild(0).gameObject);
    }

    public bool CheckLevel(Vector3Int pos)
        { return Levels.ContainsKey(pos); }

    public GameObject GetLevel(Vector3Int pos)
    { 
        if (Levels.ContainsKey(pos))
            return Levels[pos];

        return null;
    }

    public Vector3Int GetCurLevelPos()
    {
        Vector3 playerPos = player.GetPlayerPos();
        return new Vector3Int(
            (int)(playerPos.x / 40f),
            (int)(playerPos.y / 22.5f),
            0);
    }

}
