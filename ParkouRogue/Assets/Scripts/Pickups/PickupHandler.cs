using UnityEngine;

public class PickupHandler : MonoBehaviour
{

    [SerializeField]
    private GameObject prefCoin;
    [SerializeField]
    private GameObject prefRefresh;

    

    public void GenerateCoin(Vector3 pos, int value)
    {
        GameObject coin = Instantiate(prefCoin);
        coin.transform.SetParent(this.transform);
        coin.GetComponent<Coin>().setCoins(value);
        coin.transform.position = pos;
    }

    public void GenerateRefresh(Vector3 pos)
    {
        GameObject refresh = Instantiate(prefRefresh);
        refresh.transform.SetParent(this.transform);
        refresh.transform.position = pos;
    }

}
