using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [System.Serializable]
    public class CameraRig
    {
        public Vector3 CameraOffset;
        public float CrouchHeight;
        public float Damping;
    }

    [SerializeField] CameraRig defaultCamera;
    [SerializeField] CameraRig aimCamera;

    Player localPlayer;
    Transform cameraLookTarget;
    
    void Awake()
    {
        GameManager.instance.OnLocalPlayerJoin += HandeledOnLocalPlayerJoined;
    }

    void HandeledOnLocalPlayerJoined(Player player)
    {
        localPlayer = player;
        cameraLookTarget = localPlayer.transform.Find("AimingPivot");

        if (cameraLookTarget == null)
            cameraLookTarget = localPlayer.transform;
    }

    void LateUpdate()
    {
        if (localPlayer == null)
            return;

        CameraRig cameraRig = defaultCamera;

        if (localPlayer.PlayerState.WeaponState == PlayerState.EWeaponState.AIMING || localPlayer.PlayerState.WeaponState == PlayerState.EWeaponState.AIMEDFIRING)
            cameraRig = aimCamera;

        float targetHeight = cameraRig.CameraOffset.y +
            (localPlayer.PlayerState.MoveState == PlayerState.EMoveState.CROUCHING ? cameraRig.CrouchHeight : 0);

        Vector3 targetPosition = cameraLookTarget.position + localPlayer.transform.forward * cameraRig.CameraOffset.z
                                + localPlayer.transform.up * targetHeight
                                + localPlayer.transform.right * cameraRig.CameraOffset.x;

        Vector3 collisionDestination = cameraLookTarget.position + localPlayer.transform.up * targetHeight - localPlayer.transform.forward * .5f;
        Debug.DrawLine(collisionDestination, targetPosition, Color.blue);
        HandleCameraCollision(collisionDestination, ref targetPosition);

        transform.position = Vector3.Lerp(transform.position, targetPosition, cameraRig.Damping * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, cameraLookTarget.rotation, cameraRig.Damping * Time.deltaTime);
    }

    void HandleCameraCollision(Vector3 toTarget, ref Vector3 fromTarget)
    {
        RaycastHit hit;
        if (Physics.Linecast(toTarget, fromTarget, out hit))
        {
            Vector3 hitPoint = new Vector3(hit.point.x + hit.normal.x * .2f, hit.point.y, hit.point.z + hit.normal.z * .2f);
            fromTarget = new Vector3(hitPoint.x, fromTarget.y, hitPoint.z);
        }
    }
}
