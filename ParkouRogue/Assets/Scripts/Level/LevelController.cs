using UnityEngine;
using UnityEngine.InputSystem.XR;

public class LevelController : MonoBehaviour
{

    [SerializeField]
    private int Weight;

    [SerializeField]
    private LevelHandler levelHandler;

    [SerializeField]
    private LevelDoor doorUp;
    [SerializeField]
    private LevelDoor doorDown;
    [SerializeField]
    private LevelDoor doorRight;
    [SerializeField]
    private LevelDoor doorLeft;

    private void Start()
    {
        levelHandler = this.transform.parent.parent.GetComponent<LevelHandler>();
        OpenDoors(Vector3Int.RoundToInt(this.transform.parent.localPosition));
        CloseDoors();
    }

    public void DoorHit(short door) //1 = Right, 2 = Up, Negative = Reverse
    {
        levelHandler.LevelGenerate(door);
    }

    public void CloseDoors()
    {
        if (this.transform.position.x == 0)
            doorLeft.CloseDoor();
        if (this.transform.position.y == 0)
            doorDown.CloseDoor();
    }

    public void OpenDoors(Vector3Int loc)
    {
        GameObject level = null;
        Debug.Log(loc);
        if (level = levelHandler.GetLevel(loc + new Vector3Int(-1, 0, 0)))
        {
            OpenDoor(-1);
            level.GetComponent<LevelController>().OpenDoor(1);
        }
        if (level = levelHandler.GetLevel(loc + new Vector3Int(1, 0, 0)))
        {
            OpenDoor(1);
            level.GetComponent<LevelController>().OpenDoor(-1);
        }
        if (level = levelHandler.GetLevel(loc + new Vector3Int(0, -1, 0)))
        {
            OpenDoor(-2);
            level.GetComponent<LevelController>().OpenDoor(2);
        }
        if (level = levelHandler.GetLevel(loc + new Vector3Int(0, 1, 0)))
        {
            OpenDoor(2);
            level.GetComponent<LevelController>().OpenDoor(-2);
        }
    }

    public void OpenDoor(short door)
    {
        switch (door)
        {
            case -2:
                doorDown.OpenDoor();
                break;
            case -1:
                doorLeft.OpenDoor();
                break;
            case 1:
                doorRight.OpenDoor();
                break;
            case 2:
                doorUp.OpenDoor();
                break;
        }
    }

    public int GetWeight()
    {
        return Weight;
    }

}
