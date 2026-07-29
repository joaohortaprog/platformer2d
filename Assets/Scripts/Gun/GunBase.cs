using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{

    public ProjectileBase prefabProjectile;
    public Transform positionToShoot;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        Debug.Log("ShootPosition: " + positionToShoot.position);

        var projectile = Instantiate(prefabProjectile);

        projectile.transform.position = positionToShoot.position;

        Debug.Log("Projectile: " + projectile.transform.position);
    }

}
