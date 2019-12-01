using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator anim;
    InputController inputController;
    PlayerAim m_playerAim;

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        inputController = GameManager.instance.GetInputController;
    }

    public PlayerAim PlayerAim
    {
        get
        {
            if (m_playerAim == null)
                m_playerAim = GameManager.instance.LocalPlayer.playerAim;

            return m_playerAim;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.IsPaused)
            return;

        anim.SetFloat("Vertical", inputController.vertical);
        anim.SetFloat("Horizontal", inputController.horizontal);

        anim.SetBool("isWalking", inputController.isWalking);
        anim.SetBool("isSprinting", inputController.isSprinting);
        anim.SetBool("isCrouching", inputController.isCrouching);

        anim.SetFloat("AimAngle", PlayerAim.getAngle());

        anim.SetBool("isAiming", 
            GameManager.instance.LocalPlayer.PlayerState.WeaponState == PlayerState.EWeaponState.AIMING ||
            GameManager.instance.LocalPlayer.PlayerState.WeaponState == PlayerState.EWeaponState.AIMEDFIRING);

        anim.SetBool("isInCover", GameManager.instance.LocalPlayer.PlayerState.MoveState == PlayerState.EMoveState.COVER);
    }
}
