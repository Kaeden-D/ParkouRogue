using Chapter.State;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{

    [SerializeField]
    private PlayerController player;



    public Vector3 GetCurLevelPos()
    {
        Vector3 playerPos = player.GetPlayerPos();
        return new Vector3
            ((float)(int)(playerPos.x / 40f),
            (float)(int)(playerPos.y / 22.5f),
            0f);
    }

}
