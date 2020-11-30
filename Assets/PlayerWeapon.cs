using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    private const string BULLET_PREFAB_PATH = "_Prefabs/Bullet";
    public Transform ShootPoint;
    public float force = 30;
    public float lifeTime = 3;


    bool _isReloading = false;

    public int maxAmmo = 10;

    private int _ammo;
    int ammo
    {
        get => _ammo;
        set
        {
            if (value != _ammo)
            {
                OnAmmoChanged?.Invoke(value);
                _ammo = value;
            }
        }
    }
    bool reloading = false;

    public void Start()
    {
        ammo = maxAmmo;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
    }
    private void Fire()
    {

        if (reloading) return;
        if (ammo == 0)
        {
            StartCoroutine(Reload());
            return;
        }
        ammo -= 1;

        var bullet = ObjectPooler.GetPooledObject(BULLET_PREFAB_PATH);
        bullet.transform.position = ShootPoint.position;
        bullet.transform.rotation = Quaternion.identity;
        bullet.SetActive(true);
        var rigidBody = bullet.GetComponent<Rigidbody>();
        rigidBody.AddForce(ShootPoint.forward * force, ForceMode.Impulse);

    }
    IEnumerator Reload()
    {
        _isreloading = true;
        OnReload?.Invoke(true);
        var wait = new WaitForSeconds(.30f);

        while (ammo < maxAmmo)
        {
            ammo += 1;
            yield return wait;
        }
        _isreloading = false;
        OnReload?.Invoke(false);
        ammo = maxAmmo;
    }
}
;
