using Chapter.State;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private LevelHandler levelHand;
    [SerializeField]
    private GameObject camera;

    private void Update()
    {
        camera.transform.position = GetNewCamPos();
    }

    public Vector3 GetNewCamPos()
    {
        Vector3 curLevelPos = levelHand.GetCurLevelPos();
        return new Vector3
            (40f * curLevelPos.x + 20f,
            22.5f * curLevelPos.y + 11.25f,
            -5f);
    }

}
