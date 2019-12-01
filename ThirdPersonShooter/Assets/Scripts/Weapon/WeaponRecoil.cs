using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Shooter))]
public class WeaponRecoil : MonoBehaviour
{
    [System.Serializable]
    public struct Layer
    {
        public AnimationCurve curve;
        public Vector3 direction;
    }

    [SerializeField] Layer[] layers;
    [SerializeField] float recoilSpeed;
    [SerializeField] float recoilCooldown;
    [SerializeField] float strenght;

    Shooter m_Shooter;
    Shooter Shooter
    {
        get
        {
            if (m_Shooter == null)
                m_Shooter = GetComponent<Shooter>();

            return m_Shooter;
        }
    }

    Crosshair m_Crosshair;
    Crosshair Crosshair
    {
        get
        {
            if (m_Crosshair == null)
                m_Crosshair = GameManager.instance.LocalPlayer.playerAim.GetComponentInChildren<Crosshair>();

            return m_Crosshair;
        }
    }

    float nextRecoilCooldown;
    float recoilActiveTime;

    // Update is called once per frame
    void Update()
    {
        if(nextRecoilCooldown > Time.time)
        {
            //we are holding the fire button
            recoilActiveTime += Time.deltaTime;
            float percentage = getPercentage();

            Vector3 recoilAmount = Vector3.zero;
            for(int i = 0; i < layers.Length; i++)
                recoilAmount += layers[i].direction * layers[i].curve.Evaluate(percentage);

            this.Shooter.AimTargetOffset = Vector3.Lerp(Shooter.AimTargetOffset, Shooter.AimTargetOffset + recoilAmount, strenght * Time.deltaTime);
            this.Crosshair.ApplyScale(percentage * Random.Range(strenght * 7, strenght * 9));
        }
        else
        {
            //we are not holding fire button
            recoilActiveTime -= Time.deltaTime;
            if (recoilActiveTime < 0)
                recoilActiveTime = 0;

            this.Crosshair.ApplyScale(getPercentage());

            if(recoilActiveTime == 0)
            {
                this.Shooter.AimTargetOffset = Vector3.zero;
                this.Crosshair.ApplyScale(0);
            }
        }
    }

    public void Activate()
    {
        nextRecoilCooldown = Time.time + recoilCooldown;
    }

    float getPercentage()
    {
        float percentage = recoilActiveTime / recoilSpeed;
        return Mathf.Clamp01(percentage);
    }
}
