using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public int bulletID;


    public int dmg;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    int bulletsLeft;
    bool shooting=false, readyToShoot=true, reloading=false;

    public LayerMask whatIsEnemy;

    private void Awake()
    {
        bulletsLeft = magazineSize;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        if (readyToShoot && !reloading)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation).GetComponentInChildren<Animator>().Play("Bullet" + bulletID.ToString());
            bulletsLeft--;
            readyToShoot = false;
            Invoke("ResetShot", timeBetweenShooting);
        }
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }

    public void Reload()
    {
        if (bulletsLeft < magazineSize && !reloading)
        {
            reloading = true;
            Invoke("ReloadFinished", timeBetweenShooting);
        }
    }
    private void ReloadFinished()
    {
        reloading = false;
        bulletsLeft = magazineSize;
    }
}
