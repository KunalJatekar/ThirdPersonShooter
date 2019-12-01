using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shared.Exstension;

[RequireComponent(typeof(SphereCollider))]
public class Scanner : MonoBehaviour
{
    [SerializeField] float scanSpeed;
    [SerializeField] [Range(0,360)] float fieldOfView;
    [SerializeField] public LayerMask mask;

    SphereCollider rangeTrigger;

    public float ScanRange
    {
        get
        {
            if(rangeTrigger == null)
                rangeTrigger = GetComponent<SphereCollider>();

            return rangeTrigger.radius;
        }
    }

    public event System.Action OnScanReady;

    void Awake()
    {
        PrepareScanner();
    }

    void PrepareScanner()
    {
        GameManager.instance.Timer.Add(() =>{
            if (OnScanReady != null)
                OnScanReady();
        }, scanSpeed);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + GetViewAngle(fieldOfView / 2) * GetComponent<SphereCollider>().radius);
        Gizmos.DrawLine(transform.position, transform.position + GetViewAngle(-fieldOfView / 2) * GetComponent<SphereCollider>().radius);
    }

    Vector3 GetViewAngle(float angle)
    {
        float radian = (angle + transform.eulerAngles.y) * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), 0, Mathf.Cos(radian));
    }

    public List<T> ScanForTargets<T>()
    {
        List<T> targets = new List<T>();
        Collider[] results = Physics.OverlapSphere(transform.position, ScanRange);

        for(int i = 0; i < results.Length; i++)
        {
            var player = results[i].transform.GetComponent<T>();

            if (player == null)
                continue;

            if (!transform.IsInLineOfSight(results[i].transform.position, fieldOfView, mask, Vector3.up))
                continue;

            targets.Add(player);
        }

        //if(targets.Count == 1)
        //{
        //    selectedTarget = targets[0];
        //}
        //else
        //{
        //    //check for the closest target...
        //    float closestTarget = rangeTrigger.radius;

        //    foreach(var possibleTarget in targets)
        //    {
        //        if (Vector3.Distance(transform.position, possibleTarget.transform.position) < closestTarget)
        //            selectedTarget = possibleTarget;
        //    }

        //}

        PrepareScanner();

        return targets;
    }
}
