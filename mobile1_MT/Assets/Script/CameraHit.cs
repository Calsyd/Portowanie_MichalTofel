using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHit : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));

            if (rayHit.transform.GetComponent<Shield>() != null)
            {
                rayHit.transform.GetComponent<Shield>().Click();
                Debug.Log("Click Shield");
            }

            Debug.Log(rayHit.transform.name);
        }
    }
}
