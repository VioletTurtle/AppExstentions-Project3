using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup))]
public class ItemDragHandler : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] protected ItemSlotUI itemSlotUI = null;
    [SerializeField] protected HotbarItemEvent onMouseStartHoverItem = null;
    [SerializeField] protected VoidEvent onMouseEndHoverItem = null;

    private CanvasGroup canvasGroup = null;
    private Transform origanlParent = null;
    private bool isHovering = false;

    public ItemSlotUI ItemSlotUI=> itemSlotUI;

    private void Start() => canvasGroup = GetComponent<CanvasGroup>();

    private void OnDisable()
    {
        if(isHovering)
        {
            onMouseEndHoverItem.Raise();
            isHovering = false;
        }
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            onMouseEndHoverItem.Raise();
            origanlParent = transform.parent;
            transform.SetParent(transform.parent.parent);
            canvasGroup.blocksRaycasts = false;
        }
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            transform.position = Input.mousePosition;
        }
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            transform.SetParent(origanlParent);
            transform.localPosition = Vector3.zero;
            canvasGroup.blocksRaycasts = true;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        onMouseStartHoverItem.Raise(ItemSlotUI.SlotItem);
        isHovering = false;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        onMouseEndHoverItem.Raise();
        isHovering = true;
    }
}
