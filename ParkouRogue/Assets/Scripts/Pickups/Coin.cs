using Chapter.State;
using UnityEngine;

public class Coin : Pickup
{

    [SerializeField]
    private int coins;

    public void setCoins(int value)
    {
        coins = value;
    }

    public override void CollisionDetected(Collider collision)
    {
        Debug.Log("Coin Collected");
        collision.GetComponent<PlayerController>().ChangeCoins(coins);
        base.CollisionDetected(collision);
    }

}
