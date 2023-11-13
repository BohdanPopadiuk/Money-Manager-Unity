using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class CoinCollecting : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyCounter;
    [SerializeField] private Transform pileOfCoins;
    [SerializeField] private Transform endCoinPoint;

    private Vector2[] initialPos;
    private Vector3[] initialRotation;
    
    private int coinCount;
    void Start()
    {
        coinCount = pileOfCoins.childCount;
        initialPos = new Vector2[coinCount];
        initialRotation = new Vector3[coinCount];
        
        for (int i = 0; i < coinCount; i++)
        {
            initialPos[i] = pileOfCoins.GetChild(i).localPosition;
            initialRotation[i] = pileOfCoins.GetChild(i).localEulerAngles;
        }
    }

    public void SetCoinsStartPosition(Vector3 position)
    {
        pileOfCoins.position = position;
    }
    
    public void CollectCoinsAnimation(int currentMoneyCount, int previousMoneyCount)
    {
        int counterUpdateStep = (currentMoneyCount - previousMoneyCount) / coinCount;
        
        float delay = 0f;

        for (int i = 0; i < coinCount; i++)
        {
            Transform coin = pileOfCoins.GetChild(i).transform;
            
            coin.DOScale(1f, 0.3f).SetDelay(delay).SetEase(Ease.OutBack);
            coin.DOMove(new Vector2(endCoinPoint.position.x, endCoinPoint.position.y), 0.6f)
                .SetDelay(delay + 0.5f).SetEase(Ease.InBack);
            coin.DORotate(Vector3.zero, 0.5f).SetDelay(delay + 0.5f).SetEase(Ease.Flash);
            coin.DOScale(0f, 0.3f).SetDelay(delay + 1.5f).SetEase(Ease.OutBack);
            
            int newMoneyCount = previousMoneyCount + counterUpdateStep * i;
            if (i + 1 >= coinCount) newMoneyCount = currentMoneyCount;
            StartCoroutine(GraduallyUpdateCoinCount(newMoneyCount, delay));

            delay += 0.1f;

            coin.localPosition = initialPos[i];
            coin.localEulerAngles = initialRotation[i];
        }
    }
    
    private IEnumerator GraduallyUpdateCoinCount(int newMoneyCount, float delay)
    {
        yield return new WaitForSecondsRealtime(delay + 1.1f);
        moneyCounter.transform.DOScale(1.1f, 0.1f).SetLoops(2, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
        moneyCounter.SetText(newMoneyCount.ToString());
    }
}
