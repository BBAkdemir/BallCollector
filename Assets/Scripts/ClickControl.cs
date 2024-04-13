using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickControl : MonoBehaviour, IPointerMoveHandler, IPointerDownHandler
{
    Click click;
    private void Start()
    {
        click = GameObject.FindWithTag("LevelSystem").GetComponent<Click>();
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        click.MouseMovePosition = eventData.pointerCurrentRaycast.worldPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        click.MouseFirstPosition = eventData.pointerPressRaycast.worldPosition;
        click.SliderFirstPosition = click.SelectedObject.transform.position;
    }
}
