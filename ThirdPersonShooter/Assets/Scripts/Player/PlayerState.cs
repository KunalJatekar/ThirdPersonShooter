using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public enum EMoveState
    {
        WALKING,
        RUNNING,
        CROUCHING,
        SPRINTING,
        COVER
    }

    public enum EWeaponState
    {
        IDLE,
        FIRING,
        AIMING,
        AIMEDFIRING
    }

    public EMoveState MoveState;
    public EWeaponState WeaponState;

    bool isInCover = false;

    InputController m_InputController;

    public InputController InputController
    {
        get
        {
            if (m_InputController == null)
                m_InputController = GameManager.instance.GetInputController;

            return m_InputController;
        }
    }

    private void Awake()
    {
        GameManager.instance.EventBus.AddListener("CoverToggle", ToggleCover);
    }

    void ToggleCover()
    {
        isInCover = !isInCover;
    }

    void Update()
    {
        setMoveState();
        setWeaponState();
    }

    void setWeaponState()
    {
        WeaponState = EWeaponState.IDLE;

        if (InputController.fire1)
            WeaponState = EWeaponState.FIRING;

        if (InputController.fire2)
            WeaponState = EWeaponState.AIMING;

        if (InputController.fire1 && InputController.fire2)
            WeaponState = EWeaponState.AIMEDFIRING;
    }

    void setMoveState()
    {
        MoveState = EMoveState.RUNNING;

        if (InputController.isWalking)
            MoveState = EMoveState.WALKING;

        if (InputController.isSprinting)
            MoveState = EMoveState.SPRINTING;

        if (InputController.isCrouching)
            MoveState = EMoveState.CROUCHING;

        if (isInCover)
            MoveState = EMoveState.COVER;
    }
}
