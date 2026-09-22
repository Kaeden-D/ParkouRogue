using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject player;
    public GameObject camera;

    private void Update()
    {
        camera.transform.position = CameraPos();
    }

    public Vector3 CameraPos()
    {
        Vector3 pos = new Vector3
            (40f * (float)(int)(player.transform.position.x / 40f) + 20f,
            22.5f * (float)(int)(player.transform.position.y / 22.5f) + 11.25f,
            -5f);

        return pos;
    }

}
