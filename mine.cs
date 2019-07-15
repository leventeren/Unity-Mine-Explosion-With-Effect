using UnityEngine;
using System.Collections;

public class Mine : MonoBehaviour
{
    // debug mode -> show radius
    public bool debug = false;
    public bool ImpactAddForce = false;
    [Space]
    public float radius = 2;
    public float power = 10;
    public float damage = 100;
    public float removeAfterTime = 1;
    [Space]
    public GameObject ImpactEffect;
    private Collider[] colliders;

    public void Update()
    {
        Vector3 explosionPosition = transform.position;
        colliders = Physics.OverlapSphere(explosionPosition, radius);

        foreach (Collider hit in colliders)
        {
            if (!hit) { continue; }
            // if gameobject have rigidbody addon and have enemy tag !!!
            if (hit.GetComponent<Rigidbody>() && hit.gameObject.tag=="Enemy")
            {
                // add force gameobject
                if (ImpactAddForce) hit.GetComponent<Rigidbody>().AddExplosionForce((power), explosionPosition, (radius), 0);
                // send damage message to gameobject with ApplyDamage
                hit.gameObject.SendMessage("ApplyDamage", damage, SendMessageOptions.DontRequireReceiver);
                // create impact effect
                if (ImpactEffect) Instantiate(ImpactEffect, transform.position, Quaternion.Euler(-90, 0, 0));
                // destroy mine gameobject
                Destroy(gameObject, removeAfterTime);
            }
        }
    }
    void OnDrawGizmos()
    {
        if (debug)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position, radius);
        }
    }
}