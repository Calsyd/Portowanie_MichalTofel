using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SquareMoovment : MonoBehaviour
{
    private Vector2 moove;
    public void Moovement(InputAction.CallbackContext context)
    {
        moove = context.ReadValue<Vector2>();
    }
    private void Update()
    {
        this.transform.position += (Vector3)moove * Time.deltaTime * 2;
    }
}
