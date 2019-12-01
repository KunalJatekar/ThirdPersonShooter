using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float timeToLive;
    [SerializeField] float damage;
    [SerializeField] Transform bulletHole;

    Vector3 destination;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timeToLive);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDestinationReached())
        {
            Destroy(gameObject);
            return;
        }

        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        if (destination != Vector3.zero)
            return;

        RaycastHit hit;

        if(Physics.Raycast(transform.position, transform.forward, out hit, 5f))
        {
            checkDestructable(hit);
        }
    }

    private void checkDestructable(RaycastHit hitInfo)
    {
        var destructable = hitInfo.transform.GetComponent<Destructable>();

        if (hitInfo.transform.tag != "Player" || hitInfo.transform.tag != "MainCamera" || hitInfo.transform.tag != "Player")
        {
            destination = hitInfo.point + hitInfo.normal * .00015f;

            Transform hole = (Transform) Instantiate(bulletHole, destination, Quaternion.LookRotation(hitInfo.normal) * Quaternion.Euler(0, 180f, 0));
            hole.SetParent(hitInfo.transform);
        }

        if (destructable == null)
            return;

        destructable.TakeDamage(damage);
    }

    bool isDestinationReached()
    {
        if (destination == Vector3.zero)
            return false;

        Vector3 directionToDestination = destination - transform.position;
        float dot = Vector3.Dot(directionToDestination, transform.forward);

        if (dot < 0)
            return true;

        return false;
    }
}
