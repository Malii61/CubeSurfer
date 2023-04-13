using UnityEngine;
using TMPro;

public class GoldTreasure : MonoBehaviour
{
    [SerializeField] int goldAmount;
    [SerializeField] Transform goldAmountCanvas;
    [SerializeField] TextMeshProUGUI goldText;
    private void Start()
    {
        goldAmountCanvas.gameObject.SetActive(false);
    }
    public void Interact()
    {
        GetComponent<MeshRenderer>().enabled = false;
        goldAmountCanvas.gameObject.SetActive(true);
        goldText.text = "+" + goldAmount;
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
