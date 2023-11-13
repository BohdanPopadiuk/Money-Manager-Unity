using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PageAnimation : MonoBehaviour
{
    [Range(0.0f, 2.0f)]
    [SerializeField] private float animDuration = .25f;
    [SerializeField] private Ease scaleEasy = Ease.InOutBack;
    
    [SerializeField] private CanvasGroup background;
    [SerializeField] private Transform[] animatedElements;
    
    private List<Vector3> animatedElementsDefaultScale = new List<Vector3>();
    private Vector3 closedScale = new (.5f, .5f, .5f);

    public void Start()
    {
        background.alpha = 0;
        background.blocksRaycasts = false;
        for (int i = 0; i < animatedElements.Length; i++)
        {
            animatedElementsDefaultScale.Add(animatedElements[i].localScale);
            animatedElements[i].localScale = closedScale;
        }
    }

    public void OpenPage()
    {
        background.blocksRaycasts = true;
        background.DOFade(1, animDuration);
        for (int i = 0; i < animatedElements.Length; i++)
        {
            animatedElements[i].DOScale(animatedElementsDefaultScale[i], animDuration)
                .SetEase(scaleEasy);
        }
    }

    public void ClosePage()
    {
        background.DOFade(0, animDuration);
        foreach (var element in animatedElements)
        {
            element.DOScale(closedScale, animDuration).SetEase(scaleEasy);
        }
        StartCoroutine(ClosePageOnCompleteCoroutine());
    }

    private IEnumerator ClosePageOnCompleteCoroutine()
    {
        yield return new WaitForSeconds(animDuration);
        background.blocksRaycasts = false;
        ClosePageOnComplete();
    }

    public virtual void ClosePageOnComplete()
    {
        
    }
}
