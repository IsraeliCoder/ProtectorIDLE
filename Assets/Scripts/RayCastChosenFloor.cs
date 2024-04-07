using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastChosenFloor : MonoBehaviour
{
    public Color colorToChange; // The color to change the objects to
    public LayerMask layerMask; // The layer mask to filter which objects are affected by the raycast
    public float maxRaycastDistance; // Maximum distance of the raycast
    [SerializeField]Camera cam;

    public Color tempOriginRender;
    public GameObject objPressed;

    void Update()
    {
        // Cast a ray from the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Perform the raycast
        if (Physics.Raycast(ray, out hit, maxRaycastDistance, layerMask))
        {
            if (Input.GetMouseButtonDown(0))
            {
                objPressed = hit.collider.gameObject;
                tempOriginRender = hit.collider.GetComponent<Renderer>().material.color;

                // Check if the hit object has the specified tag
                if (hit.collider.CompareTag("TileCube"))
                {
                    // Change the color of the hit object
                    Renderer renderer = hit.collider.GetComponent<Renderer>();
                    if (renderer != null)
                    {
                        renderer.material.color = colorToChange;
                    }
                }
            }
            else if (Input.GetMouseButtonUp(0) && tempOriginRender != null && objPressed != null)
            {
                if (hit.collider.CompareTag("TileCube"))
                {
                    // Change the color of the hit object
                    Renderer renderer = objPressed.GetComponent<Renderer>();
                    if (renderer != null)
                    {
                        renderer.material.color = tempOriginRender;
                    }
                }
            }
        }
    }
}
