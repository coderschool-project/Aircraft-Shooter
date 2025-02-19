using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 3f;
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
        minY = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y - padding;
        maxY = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y + padding;
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.up);
        if (transform.position.y < minY || transform.position.y > maxY)
        {
            Destroy(gameObject);
        }
    }

}
