using UnityEngine;

public class LevelDoor : MonoBehaviour
{

    [SerializeField]
    public short door = 0; //1 = Right, 2 = Up, Negative = Reverse

    [SerializeField]
    private GameObject full;
    [SerializeField]
    private GameObject side1;
    [SerializeField]
    private GameObject side2;

    public void CloseDoor()
    {
        full.SetActive(true);
        side1.SetActive(false);
        side2.SetActive(false);
    }

    public void OpenDoor()
    {
        full.SetActive(false);
        side1.SetActive(true);
        side2.SetActive(true);
        this.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {

            this.transform.parent.parent.gameObject.GetComponent<LevelController>().DoorHit(door);

        }

    }

}
