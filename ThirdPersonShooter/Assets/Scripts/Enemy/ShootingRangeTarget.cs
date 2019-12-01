using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingRangeTarget : Destructable
{
    [SerializeField] float rotationSpeed;
    [SerializeField] float repairTime;

    Quaternion originalRotation;
    Quaternion targetRotation;
    bool requiredRotation;

    private void Awake()
    {
        originalRotation = transform.rotation;
    }

    public override void Die()
    {
        base.Die();
        targetRotation = Quaternion.Euler(transform.right * 90);
        requiredRotation = true;
        GameManager.instance.Timer.Add(() =>
        {
            targetRotation = originalRotation;
            requiredRotation = true;
        }, repairTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (!requiredRotation)
            return;

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        if (transform.rotation == targetRotation)
            requiredRotation = false;
    }
}
