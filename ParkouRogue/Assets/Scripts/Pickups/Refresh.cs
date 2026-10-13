using Chapter.State;
using UnityEngine;

public class Refresh : Pickup
{

    public override void CollisionDetected(Collider collision)
    {
        Debug.Log("Refresh Collected");
        collision.GetComponent<PlayerController>().AbilityReset();
        base.CollisionDetected(collision);
    }

}
