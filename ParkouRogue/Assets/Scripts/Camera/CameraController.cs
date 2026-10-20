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
        Vector3 curScale = levelHand.transform.localScale;
        return new Vector3
            (curScale.x * curLevelPos.x + (curScale.x / 2),
            curScale.y * curLevelPos.y + (curScale.y / 2),
            -5f);
    }

}
