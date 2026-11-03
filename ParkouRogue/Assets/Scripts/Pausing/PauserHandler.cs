using System.Collections.Generic;
using UnityEngine;

public class PauserHandler : MonoBehaviour
{

    public void Pause()
    {
        Time.timeScale = 0f; // Pause the game time
    }

    public void Resume()
    {
        Time.timeScale = 1f; // Resume the game time
    }

    private void OnGUI()
    {

        if (GUILayout.Button("Pause"))
        {
            Pause();
        }
        if (GUILayout.Button("Resume"))
        {
            Resume();
        }

    }

}
