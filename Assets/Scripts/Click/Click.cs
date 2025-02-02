using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class Click : MonoBehaviour, IPointerClickHandler
{
    Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Vector3 clickPosition = eventData.position;

        Ray ray = cam.ScreenPointToRay(clickPosition);
        RaycastHit[] hits = Physics.RaycastAll(ray);

        foreach (RaycastHit hit in hits) 
        {
            Debug.Log(hit.collider.gameObject);
        }
    }
}
