using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform ShootPoint;
    public float force = 30;
    public float lifeTime = 3;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
    }
    private void Fire()
    {
        var bullet = Instantiate(bulletPrefab, ShootPoint.position, Quaternion.identity);
        var rigidBody = bullet.GetComponent<Rigidbody>();
        rigidBody.AddForce(ShootPoint.forward * force, ForceMode.Impulse);
        StartCoroutine(DestroyBulletAfterTime(bullet, lifeTime));

    }
    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);

    }
}
;