using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerState))]
[RequireComponent(typeof(PlayerHealth))]
public class Player : MonoBehaviour
{
    [System.Serializable]
    public class MouseInput
    {
        public Vector2 Damping;
        public Vector2 Sensetivity;
        public bool LockMouse;
    }

    [SerializeField] SwatSoldier settings;
    [SerializeField] MouseInput mouseControl;
    [SerializeField] AudioController footSteps;
    [SerializeField] float minimumThreshold;

    public PlayerAim playerAim;

    InputController playerInput;

    CharacterController m_MoveController;
    Vector2 mouseInput;
    Vector3 previousPosition;
    PlayerShoot m_PlayerShoot;
    PlayerState m_PlayerState;
    PlayerHealth m_PlayerHealth;

    public CharacterController MoveController
    {
        get
        {
            if(m_MoveController == null)
            {
                m_MoveController = GetComponent<CharacterController>();
            }
            return m_MoveController;
        }
    }

    public PlayerShoot PlayerShoot
    {
        get
        {
            if (m_PlayerShoot == null)
            {
                m_PlayerShoot = GetComponent<PlayerShoot>();
            }
            return m_PlayerShoot;
        }
    }

    public PlayerHealth PlayerHealth
    {
        get
        {
            if (m_PlayerHealth == null)
            {
                m_PlayerHealth = GetComponent<PlayerHealth>();
            }
            return m_PlayerHealth;
        }
    }

    public PlayerState PlayerState
    {
        get
        {
            if (m_PlayerState == null)
            {
                m_PlayerState = GetComponent<PlayerState>();
            }
            return m_PlayerState;
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        playerInput = GameManager.instance.GetInputController;
        GameManager.instance.LocalPlayer = this;

        if (mouseControl.LockMouse)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerHealth.IsAlive || GameManager.instance.IsPaused)
            return;

        Move();
        LookAround();
    }

    private void LookAround()
    {
        mouseInput.x = Mathf.Lerp(mouseInput.x, playerInput.mouseInput.x, 1f / mouseControl.Damping.x);
        mouseInput.y = Mathf.Lerp(mouseInput.y, playerInput.mouseInput.y, 1f / mouseControl.Damping.y);

        transform.Rotate(Vector3.up * mouseInput.x * mouseControl.Sensetivity.x);

        playerAim.setAngle(mouseInput.y * mouseControl.Sensetivity.y);
    }

    public void Move()
    {
        float moveSpeed = settings.RunSpeed;

        if (playerInput.isWalking)
            moveSpeed = settings.WalkSpeed;

        if (playerInput.isSprinting)
            moveSpeed = settings.SprintSpeed;

        if (playerInput.isCrouching)
            moveSpeed = settings.CrouchSpeed;

        if (PlayerState.MoveState == PlayerState.EMoveState.COVER)
            moveSpeed = settings.WalkSpeed;

        Vector2 direction = new Vector2(playerInput.vertical * moveSpeed, playerInput.horizontal * moveSpeed);
        MoveController.SimpleMove(transform.forward * direction.x + transform.right * direction.y);
        //MoveController.SimpleMove(direction);

        if (Vector3.Distance(transform.position,previousPosition) > minimumThreshold)
            footSteps.Play();

        previousPosition = transform.position;
    }
}
