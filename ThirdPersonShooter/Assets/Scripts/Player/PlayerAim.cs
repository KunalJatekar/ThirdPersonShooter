using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    [SerializeField] float minAngle;
    [SerializeField] float maxAngle;

    public void setAngle(float amount)
    {
        float clampAngle = GetClampAngle(amount);
        transform.eulerAngles = new Vector3(clampAngle, transform.eulerAngles.y, transform.eulerAngles.z);
    }

    private float GetClampAngle(float amount)
    {
        float newAngle = checkAngle(transform.eulerAngles.x - amount);
        float clampAngle = Mathf.Clamp(newAngle, minAngle, maxAngle);
        return clampAngle;
    }

    public float getAngle()
    {
        return checkAngle(transform.eulerAngles.x);
    }

    public float checkAngle(float value)
    {
        float angle = value - 180;

        if (angle > 0)
            return angle - 180;

        return angle + 180;
    }
}
