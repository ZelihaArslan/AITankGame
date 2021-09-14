using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBehaviour : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    float forceAmount = 1200f; //ne kadarlık güc istiyoruz
    float timeFromLastShoot; //


    public void Shoot(float shootFreq )
    {
        if ((timeFromLastShoot+=Time.deltaTime)>=1f/shootFreq) //sn nin 5 de 1 inden sonra diger bombayı at
        {
            InstantiateBullet();
            timeFromLastShoot = 0; //bir defa calışmasın diye
        }



    }
    public void Shoot()
    {
        InstantiateBullet();
    }


    private void InstantiateBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().AddForce(forceAmount * transform.forward);
    }
}
