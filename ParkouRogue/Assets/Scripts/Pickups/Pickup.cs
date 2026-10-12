using UnityEngine;

public class Pickup : MonoBehaviour
{

    public virtual void CollisionDetected(Collider collision)
    {
        Debug.Log("Collision Detected");
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {

            CollisionDetected(collision);

        }

    }
}
