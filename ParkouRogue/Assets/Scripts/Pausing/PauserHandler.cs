using System.Collections.Generic;
using UnityEngine;

public class PauserHandler : MonoBehaviour
{

    [SerializeField]
    private List<ObjectPauser> pausers = new List<ObjectPauser>();

    public void AddPauser(ObjectPauser pauser)
    {
        pausers.Add(pauser);
    }

    public void Pause()
    {
        foreach (ObjectPauser pauser in pausers)
        {
            pauser.PauseObject();
        }
    }

    public void Resume()
    {
        foreach (ObjectPauser pauser in pausers)
        {
            pauser.ResumeObject();
        }
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
