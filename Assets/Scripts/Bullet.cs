using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public Vector3 direction;
    public float speed = 1f;
    public float lifetime = 1000;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lifetime <= 0) 
        {
            GameObject.Destroy(this);
        }
        this.lifetime -= Time.deltaTime;

        this.transform.Translate(this.direction * this.speed * Time.deltaTime, Space.World);
    }
}
