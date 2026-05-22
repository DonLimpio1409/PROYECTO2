using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using System.Collections.Generic;

public class UIAnimationManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> mainMenuButtons;
    [SerializeField] private List<GameObject> otherMenuButtons;
    
    private float scaleMultiplier = 1.15f;
    private float animationDuration = 0.2f;

    private void Start()
    {
        foreach (GameObject button in mainMenuButtons)
        {
            ButtonHoverZoom buttonHoverAnimation = button.AddComponent<ButtonHoverZoom>();
            buttonHoverAnimation.Setup(scaleMultiplier, animationDuration);
        }
        foreach (GameObject button in otherMenuButtons)
        {
            ButtonHoverZoom buttonHoverAnimation = button.AddComponent<ButtonHoverZoom>();
            buttonHoverAnimation.Setup(scaleMultiplier, animationDuration);
        }
    }
}
public class ButtonHoverZoom : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private float targetScaleMultiplier;
    private float duration;
    private Vector3 originalScale;

    public void Setup(float scaleMultiplier, float animDuration)
    {
        targetScaleMultiplier = scaleMultiplier;
        duration = animDuration;
        originalScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOKill();
        transform.DOScale(originalScale * targetScaleMultiplier, duration).SetEase(Ease.OutBack).SetUpdate(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOKill();
        transform.DOScale(originalScale, duration).SetEase(Ease.OutQuad).SetUpdate(true);
    }

    private void OnDisable()
    {
        transform.DOKill();
        transform.localScale = originalScale;
    }
}