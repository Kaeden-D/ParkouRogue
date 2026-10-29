using UnityEngine;

public class Pausable : MonoBehaviour
{

    [SerializeField]
    protected bool paused = false;

    public void SetPause(bool pause)
    {
        paused = pause;
    }

}
