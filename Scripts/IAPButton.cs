using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IAPButton : MonoBehaviour
{
    [SerializeField] private int coins = 10;
    [SerializeField] private TextMeshProUGUI coinsText;
    private Button button;
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => BuyIAP());
        coinsText.SetText(coins.ToString());
    }

    void BuyIAP()
    {
        //ToDo (here is the IAP logic)
        MoneyManager.Instance.AnimatedAddingMoney(coins, transform.position);
    }
}
