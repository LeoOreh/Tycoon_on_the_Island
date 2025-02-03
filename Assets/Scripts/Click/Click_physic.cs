using UnityEngine;
using UnityEngine.EventSystems;

public class Click_physic : MonoBehaviour, IPointerClickHandler
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
