using UnityEngine;
using UnityEngine.EventSystems;

public class DraggablePanelScript : MonoBehaviour, IDragHandler
{
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }
}
