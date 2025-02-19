using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float speed = 5.0f;
    public float padding = 0.8f;
    float minX;
    float maxX;
    float minY;
    float maxY;
    public int HP = 4;
    public GameObject playerExplosionPrefab;

    float barSize = 1.0f;
    float damage = 0;
    public PlayerHealthBar healthBar;

    public GameObject damageEffectPrefab;
    public CoinCount coinCount;

    public GameController gameController;

    public AudioClip damageSound;
    public AudioSource AudioSource;
    public AudioClip explosionSound;
    public AudioClip coinSound;

    // Start is called before the first frame update
    void Start()
    {
        FindBouderies();
        damage = barSize / HP;
    }
    void FindBouderies()
    {
        Camera gameCamera = Camera.main;
        minX = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        maxX = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        minY = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        maxY = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }

    // Update is called once per frame
    void Update()
    {
        //float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        //float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        //float newXPos = Mathf.Clamp(transform.position.x + deltaX, minX, maxX);
        //float newYPos = Mathf.Clamp(transform.position.y + deltaY, minY, maxY);
        //transform.position = new Vector2(newXPos, newYPos);
        if(Input.GetMouseButton(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = Vector2.Lerp(transform.position, mousePos, speed * Time.deltaTime);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            AudioSource.PlayOneShot(damageSound,0.5f);
            DamageHealthBar();
            Destroy(collision.gameObject);
            GameObject damageEffect = Instantiate(damageEffectPrefab, collision.transform.position, Quaternion.identity);
            Destroy(damageEffect, 0.1f);
            if (HP <= 0)
            {
                AudioSource.PlayClipAtPoint(explosionSound, Camera.main.transform.position);
                Explode();
                gameController.GameOver();
            }
        }
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Player collided with enemy");
            AudioSource.PlayClipAtPoint(explosionSound, Camera.main.transform.position);
            Explode();
        }
        if (collision.gameObject.CompareTag("Coin"))
        {
            AudioSource.PlayOneShot(coinSound, 0.5f);
            coinCount.AddCoin();
            Destroy(collision.gameObject);
        }
    }
    void Explode()
    {
        Destroy(gameObject);
        GameObject explosion = Instantiate(playerExplosionPrefab, transform.position + new Vector3(0, 0.25f), Quaternion.identity);
        explosion.transform.rotation = Quaternion.Euler(0, 0, 90);
        Destroy(explosion, 0.75f);
    }

    void DamageHealthBar()
    {
        HP--;
        barSize -= damage;
        healthBar.SetSize(barSize);

    }
}
