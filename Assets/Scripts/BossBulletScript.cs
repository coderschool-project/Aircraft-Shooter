using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 5f;
    float minY;
    float maxY;
    public float padding = 0.8f;
    void Start()
    {
        FindBoundaries();
    }
    private void FindBoundaries()
    {
        Camera gameCamera = Camera.main;
        minY = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        maxY = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector2.down);
        if (transform.position.y < minY || transform.position.y > maxY)
        {
            Destroy(gameObject);
        }
    }
}
