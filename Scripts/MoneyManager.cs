using TMPro;
using UnityEngine;
using DG.Tweening;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance;
    
    [SerializeField] private PageAnimation iapPage;
    [SerializeField] private TextMeshProUGUI moneyCounter;
    [SerializeField] private CoinCollecting coinCollecting;

    private int currentMoneyCount;
    private readonly string moneyKey = "MONEY";
    
    void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("Destroying duplicate MoneyManager object - only one is allowed per scene!");
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        currentMoneyCount = PlayerPrefs.GetInt(moneyKey);
        UpdateMoneyCounter();
    }

    public bool UseMoney(int price)
    {
        int moneyCount = currentMoneyCount - price;
        if (moneyCount >= 0)
        {
            currentMoneyCount = moneyCount;
            PlayerPrefs.SetInt(moneyKey, currentMoneyCount);
            UpdateMoneyCounter();
            return true;
        }
        iapPage.OpenPage();
        return false;
    }
    
    public void AddMoney(int moneyCount)
    {
        currentMoneyCount += moneyCount;
        PlayerPrefs.SetInt(moneyKey, currentMoneyCount);
        UpdateMoneyCounter();
    }

    public void AnimatedAddingMoney(int moneyCount)
    {
        int previousMoneyCount = currentMoneyCount;
        currentMoneyCount += moneyCount;
        PlayerPrefs.SetInt(moneyKey, currentMoneyCount);
        coinCollecting.CollectCoinsAnimation(currentMoneyCount, previousMoneyCount);
    }
    
    public void AnimatedAddingMoney(int moneyCount, Vector3 startAnimPos)
    {
        coinCollecting.SetCoinsStartPosition(startAnimPos);
        AnimatedAddingMoney(moneyCount);
    }

    void UpdateMoneyCounter()
    {
        moneyCounter.transform.DOScale(1.1f, 0.1f).SetLoops(2, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
        moneyCounter.SetText(currentMoneyCount.ToString());
    }
}
