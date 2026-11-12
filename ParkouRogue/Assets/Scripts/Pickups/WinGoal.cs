using Chapter.State;
using Unity.VisualScripting;
using UnityEngine;

public class WinGoal : Pickup
{
    
    private void Start()
    {
        MoveGoal(1);
    }

    private void MoveGoal(int ante)
    {
        float xAxis = 0, yAxis = 0;

        yAxis = Mathf.Pow(Random.value, 2) * Mathf.Sqrt(Mathf.Pow(ante + 1, 3));
        Debug.Log(yAxis);
        xAxis = Mathf.Sqrt(Mathf.Pow(ante + 1, 3) - Mathf.Pow(yAxis, 2));
        Vector3 newPos = new Vector3((int)xAxis, (int)yAxis, 0f) * 35 + new Vector3(17.5f, 17.5f, 0);
        Debug.Log(newPos);
        this.transform.position = newPos;
    }

    public override void CollisionDetected(Collider collision)
    {
        Debug.Log("Win Goal Reached");
        PlayerController player = collision.GetComponent<PlayerController>();

        MoveGoal(player.GetAnte());
        player.ChangeAnte(1);

        base.CollisionDetected(collision);
        this.gameObject.SetActive(true);
    }

    /*private void OnGUI()
    {

        if (GUILayout.Button("Add"))
        {
            TestAnte++;
            MoveGoal(TestAnte);
        }
        if (GUILayout.Button("Minus"))
        {
            TestAnte--;
            MoveGoal(TestAnte);
        }

    }*/

}
