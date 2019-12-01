using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public float horizontal;
    public float vertical;
    public Vector2 mouseInput;
    public bool fire1;
    public bool fire2;
    public bool reload;
    public bool isWalking;
    public bool isSprinting;
    public bool isCrouching;
    public bool mouseWheelUp;
    public bool mouseWheelDown;
    public bool coverToggle;
    internal bool escape;

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        fire1 = Input.GetButton("Fire1");
        fire2 = Input.GetButton("Fire2");
        reload = Input.GetKeyDown(KeyCode.R);
        coverToggle = Input.GetKeyDown(KeyCode.F);
        isWalking = Input.GetKey(KeyCode.LeftAlt);
        isSprinting = Input.GetKey(KeyCode.LeftShift);
        isCrouching = Input.GetKey(KeyCode.C);
        mouseWheelUp = Input.GetAxis("Mouse ScrollWheel") > 0;
        mouseWheelDown = Input.GetAxis("Mouse ScrollWheel") < 0;
        escape = Input.GetKeyDown(KeyCode.Escape);
    }
}
