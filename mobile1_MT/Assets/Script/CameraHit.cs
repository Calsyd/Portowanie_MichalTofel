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
            if (rayHit == false)
                return;
            if (rayHit.transform.TryGetComponent(out Shield shield))
            {
                shield.Click();
            }

            Debug.Log(rayHit.transform.name);
        }
    }
}
