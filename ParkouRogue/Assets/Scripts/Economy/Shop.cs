using UnityEngine;

public class Shop : MonoBehaviour
{

    [SerializeField]
    private GameObject shopUI;

    [SerializeField]
    private PauserHandler pauseHandler;

    [SerializeField]
    private int baseRerollPrice;
    [SerializeField]
    private int curRerollPrice;

    public void EnterShop()
    {
        pauseHandler.Pause();
        shopUI.SetActive(true);
        Reroll();
        curRerollPrice = baseRerollPrice;
    }

    public void exitShop()
    {
        shopUI.SetActive(false);
        pauseHandler.Resume();
    }

    public void Reroll()
    {
        curRerollPrice += 1;
    }

}
