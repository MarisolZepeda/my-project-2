using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class BulletBehaviour : MonoBehaviour
{
    private Rigidbody _rb;
    private Rigidbody rb
    {
        get
        {
            if (_rb == null)
            {
                _rb = gameObject.GetComponent<Rigidbody> ();
            }
            return _rb;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        print("hit " + other.name + "!");
        gameObject.SetActive(false);
        rb.velocity = Vector3.zero;
    }

}
