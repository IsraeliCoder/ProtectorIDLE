using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastChosenFloor : MonoBehaviour
{
    public float maxRaycastDistance; // Maximum distance of the raycast
    [SerializeField]private GameObject objPressed;
    [SerializeField]private GameObject tempObj;

    void Update()
    {
        // Cast a ray from the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Perform the raycast
        if (Physics.Raycast(ray, out hit, maxRaycastDistance))
        {
            if (Input.GetMouseButtonDown(0))
            {
                objPressed = hit.collider.gameObject;

                if (tempObj == null)
                    tempObj = objPressed;

                // Check if the hit object has the specified tag
                if (hit.collider.CompareTag("TileCube"))
                {
                    if(tempObj != objPressed)
                    {
                        for (int i = 0; i < tempObj.transform.childCount; i++)
                        {
                            GameObject child = tempObj.transform.GetChild(i).gameObject;
                            child.SetActive(false);
                        }

                        tempObj = objPressed;
                    }

                    // Get the children of the clicked object
                    for (int i = 0; i < objPressed.transform.childCount; i++)
                    {
                        GameObject child = objPressed.transform.GetChild(i).gameObject;

                        if(!child.activeSelf)
                        {
                            // Set the child object's active state
                            child.SetActive(true);
                        }
                        else if(child.activeSelf)
                        {
                            child.SetActive(false);
                        }
                    }
                }
            }
        }
    }
}
