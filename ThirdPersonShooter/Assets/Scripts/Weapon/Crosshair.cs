using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    /*[SerializeField] Texture2D image;
    [SerializeField] float size;

    void OnGUI()
    {
        if (GameManager.instance.LocalPlayer.PlayerState.WeaponState == PlayerState.EWeaponState.AIMING ||
            GameManager.instance.LocalPlayer.PlayerState.WeaponState == PlayerState.EWeaponState.AIMEDFIRING)
        {
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
            screenPosition.y = Screen.height - screenPosition.y;
            GUI.DrawTexture(new Rect(screenPosition.x - size / 2, screenPosition.y - size / 2, size, size), image);
        }
    }*/

    [SerializeField] float speed;

    public Transform Reticle;

    Transform crossTop;
    Transform crossBottom;
    Transform crossLeft;
    Transform crossRight;

    float reticleStartPoint;

    private void Start()
    {
        crossTop = Reticle.Find("Cross/Top").transform;
        crossBottom = Reticle.Find("Cross/Bottom").transform;
        crossLeft = Reticle.Find("Cross/Left").transform;
        crossRight = Reticle.Find("Cross/Right").transform;

        reticleStartPoint = crossTop.localPosition.y;
    }

    private void Update()
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        Reticle.transform.position = Vector3.Lerp(Reticle.transform.position, screenPosition, speed * Time.deltaTime);
    }

    public void ApplyScale(float scale)
    {
        crossTop.localPosition = new Vector3(0, reticleStartPoint + scale, 0);
        crossBottom.localPosition = new Vector3(0, -reticleStartPoint - scale, 0);
        crossLeft.localPosition = new Vector3(-reticleStartPoint - scale, 0, 0);
        crossRight.localPosition = new Vector3(reticleStartPoint + scale, 0, 0);
    }
}
