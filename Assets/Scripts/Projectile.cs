using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public int damage;

    public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        //destroy projectile once liftime has finished
        Invoke("DestroyProjectile", lifeTime);
        
    }

    // Update is called once per frame
    void Update()
    {
        //vector2.up meaning forward
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void DestroyProjectile()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    //detect collisions
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if colliding with enemy call take damage function, and destroy projectile
        if(collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().TakeDamage(damage);
            DestroyProjectile();
        }
    }
}
