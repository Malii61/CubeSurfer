using UnityEngine;
using TMPro;

public class GoldTreasure : MonoBehaviour
{
    [SerializeField] int goldAmount;
    [SerializeField] Transform goldAmountCanvas;
    [SerializeField] TextMeshProUGUI goldText;
    private Animator anim;
    private void Start()
    {
        goldAmountCanvas.gameObject.SetActive(false);
        anim = GetComponent<Animator>();
    }
    public void Interact()
    {
        GetComponent<MeshRenderer>().enabled = false;
        goldAmountCanvas.gameObject.SetActive(true);
        goldText.text = "+" + goldAmount;
        anim.Play("RisingGold");
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
    public int GetTreasureAmount()
    {
        return goldAmount;
    }
}
