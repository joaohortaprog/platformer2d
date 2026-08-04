using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{

    public ProjectileBase prefabProjectile;
    public Transform positionToShoot;


    public float timeBetweenShots = 0.25f;

    public Transform playerSideReference;

    private Coroutine _currentCoroutine;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            _currentCoroutine = StartCoroutine(StartShoot());
        }

        else if (Input.GetKeyUp(KeyCode.S))
        {
            if(_currentCoroutine != null) 
                StopCoroutine(_currentCoroutine);
        }
    }

    IEnumerator StartShoot()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(timeBetweenShots);
        }
    }

    public void Shoot()
    {
        //Debug.Log("ShootPosition: " + positionToShoot.position);

        var projectile = Instantiate(prefabProjectile);
        projectile.transform.position = positionToShoot.position;
        projectile.side = playerSideReference.transform.localScale.x;

        //Debug.Log("Projectile: " + projectile.transform.position);
    }

}
