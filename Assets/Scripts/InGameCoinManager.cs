using UnityEngine;
using TMPro;
public class InGameCoinManager : MonoBehaviour
{
    public static InGameCoinManager Instance { get; private set; }
    private int coinAmount = 0;
    [SerializeField] TextMeshProUGUI coinAmountText;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        CollectorCube.Instance.OnCoinCollected += CollectorCube_OnCoinCollected;
        coinAmountText.text = coinAmount.ToString();
    }

    private void CollectorCube_OnCoinCollected(object sender, CollectorCube.OnCoinCollectedEventArgs e)
    {
        coinAmount += e.coinAmount;
        coinAmountText.text = coinAmount.ToString();
    }
    private void OnDestroy()
    {
        CollectorCube.Instance.OnCoinCollected -= CollectorCube_OnCoinCollected;
    }
    public int GetCoinAmount()
    {
        return coinAmount;
    }
}
