using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NewPlayerInput : MonoBehaviour, IInput
{
    public Action<Vector2> OnMovementInput { get; set; }
    public Action<Vector3> OnMovementDirectionInput { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        GetMovementInput();
        GetMovementDirection();
    }

    private void GetMovementInput()
    {
        var CameraFowrardDirection = Camera.main.transform.forward;
        var DirectionToMoveIn = Vector3.Scale(CameraFowrardDirection, (Vector3.right + Vector3.forward));
        OnMovementDirectionInput?.Invoke(DirectionToMoveIn);
    }

    private void GetMovementDirection()
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        OnMovementInput?.Invoke(input);
    }
}
