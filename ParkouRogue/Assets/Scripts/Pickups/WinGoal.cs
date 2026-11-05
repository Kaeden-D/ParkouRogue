using UnityEngine;

public class WinGoal : Pickup
{
    int TestAnte = 1;

    private void MoveGoal(int ante)
    {
        float xAxis = 0, yAxis = 0;

        yAxis = Random.Range(0, Mathf.Sqrt(Mathf.Pow(ante, 3)));
        Debug.Log(yAxis);
        xAxis = Mathf.Sqrt(Mathf.Pow(ante, 3) - Mathf.Pow(yAxis, 2));
        Vector3 newPos = new Vector3(xAxis, yAxis, 0f);
        this.transform.position = newPos;
    }

    public override void CollisionDetected(Collider collision)
    {
        Debug.Log("Win Goal Reached");
        //PlayerController player = collision.gameObject.GetComponent<PlayerController>();

        //MoveGoal(player.GetAnte());
        //player.ChangeAnte(1);

        base.CollisionDetected(collision);
    }

    private void OnGUI()
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

    }

}
