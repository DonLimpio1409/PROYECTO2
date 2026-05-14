using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Button : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private MeshRenderer meshRenderer;
    private Color originalColor;
    public Color hoverColor = Color.red;
    public UnityEvent onClick;

    void Start() {
        meshRenderer = GetComponent<MeshRenderer>();
        originalColor = meshRenderer.material.color;
    }

    public void OnPointerClick(PointerEventData eventData) {
        onClick.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData) {
        meshRenderer.material.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData) {
        meshRenderer.material.color = originalColor;
    }
}