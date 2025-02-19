using System.Collections;
using UnityEngine;

public class BossShootingScript : MonoBehaviour
{
    public GameObject bossBulletPrefab;
    public Transform[] gunPoint; 
    public float fireRate = 1.0f;
    
    public GameObject bulletFlash;
    public GameObject bossExplosionPrefab;
    public float speed = 1f;

    public int HP = 10;

    float barSize = 1.0f;
    float damage = 0;
    public HealthBar healthBar;

    public GameObject damageEffectPrefab;
    public GameObject coinPrefab;

    public AudioClip bulletSound;
    public AudioClip damageSound;
    public AudioClip explosionSound;
    public AudioSource AudioSource;

    public float minY;
    public float padding = 0.8f;

    void Start()
    {
        if (bulletFlash) bulletFlash.SetActive(false);
        StartCoroutine(FireContinuously());
        damage = barSize / HP;
        FindBoundaries();
    }

    private void FindBoundaries()
    {
        Camera gameCamera = Camera.main;
        minY = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y-padding;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            AudioSource.PlayOneShot(damageSound, 0.5f);
            DamageHealthBar();
            GameObject damageEffect = Instantiate(damageEffectPrefab, collision.transform.position, Quaternion.identity);
            Destroy(damageEffect, 0.1f);   
            Destroy(collision.gameObject); // Destroy the bullet on hit
            
            if (HP <= 0)
            {
                AudioSource.PlayClipAtPoint(explosionSound,Camera.main.transform.position,0.5f);
                Explode();
            }
        }
        else if(collision.gameObject.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(explosionSound, Camera.main.transform.position, 0.5f);
            Explode();
        }
    }

    void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector2.down);
        if (transform.position.y < minY)
        {
            Destroy(gameObject);
        }
    }

    void Fire()
    {
        foreach (Transform point in gunPoint)
        {
            _ = Instantiate(bossBulletPrefab, point.position, Quaternion.identity);
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            yield return new WaitForSeconds(fireRate);
            Fire();
            AudioSource.PlayOneShot(bulletSound, 0.5f);
            if (bulletFlash)
            {
                bulletFlash.SetActive(true);
                yield return new WaitForSeconds(0.1f);
                bulletFlash.SetActive(false);
            }
        }
    }

    void DamageHealthBar()
    {
        if (HP > 0)
        {
            HP--;
            barSize -= damage;
            healthBar.SetSize(barSize);
        }
    }

    void Explode()
    {
        GameObject explosion = Instantiate(bossExplosionPrefab, transform.position, Quaternion.identity);
        Destroy(explosion, 0.4f);
        Destroy(gameObject); // Destroy after effects are spawned
        if(coinPrefab)
        {
            Instantiate(coinPrefab, transform.position, Quaternion.identity);
        }
    }
}
