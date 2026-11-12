using UnityEngine;

public class GoalPointer : MonoBehaviour
{

    [SerializeField]
    private GameObject goal;

    private void Update()
    {
        if (goal != null)
        {
            Vector3 direction = goal.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

}
