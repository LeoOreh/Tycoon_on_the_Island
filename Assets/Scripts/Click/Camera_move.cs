using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Camera_move : MonoBehaviour, IDragHandler
{
    Transform cam;
    Vector2 lastTouchPosition;

    Vector2 touchDeltaPosition;

    Vector3 move = Vector3.zero;
    Vector3 newPosition;

    void Start()
    {
        cam = Camera.main.transform;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        lastTouchPosition = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        touchDeltaPosition = eventData.position - lastTouchPosition;

        move.x = -touchDeltaPosition.x; 
        newPosition = cam.position + move;

        newPosition.x = Mathf.Clamp(newPosition.x, -2f, 3f);

        if(cam.position.x - newPosition.x > 0.1f || cam.position.x - newPosition.x < 0.1f) cam.position = Vector3.Lerp(cam.position, newPosition, 0.01f);

        lastTouchPosition = eventData.position;
    }
}