using UnityEngine;
using UnityEngine.EventSystems;

public class DragDropBlock : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 originalPosition;
    private Transform originalParent;
    private CanvasGroup canvasGroup;


    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Guarda la posici�n y el padre original para restaurarlo si es necesario
        originalPosition = transform.position;
        originalParent = transform.parent;
        // Evita que el objeto bloquee los raycasts mientras se arrastra
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Mueve el bloque siguiendo el puntero
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Si no se solt� en una zona v�lida, vuelve a la posici�n original
        if (transform.parent == originalParent)
            transform.position = originalPosition;

        canvasGroup.blocksRaycasts = true;
    }
}
