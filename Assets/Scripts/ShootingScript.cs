using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    public GameObject laserBullet;
    public Transform SpawnBulletPointLeft;
    public Transform SpawnBulletPointRight;
    public float fireRate = 1.0f;

    public GameObject bulletFlash;


    public AudioSource laserSound;

    // Start is called before the first frame update
    void Start()
    {
        bulletFlash.SetActive(false);
        StartCoroutine(FireContinuously());
    }

    // Update is called once per frame
    void Update()
    {
    }
    void Fire()
    {
            GameObject bullet = Instantiate(laserBullet, SpawnBulletPointLeft.position, Quaternion.identity);
            GameObject bullet2 = Instantiate(laserBullet, SpawnBulletPointRight.position, Quaternion.identity);
    }

    IEnumerator FireContinuously()
    {
        while(true)
        {
            yield return new WaitForSeconds(fireRate);
            Fire();
            laserSound.Play();
            if (bulletFlash)
            {
                bulletFlash.SetActive(true);
                yield return new WaitForSeconds(0.4f);
                bulletFlash.SetActive(false);
            }

        }
    }
}
