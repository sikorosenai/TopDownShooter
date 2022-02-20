using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    //variable I will use to deal damage to player
    private Player playerScript;
    private Vector2 targetPosition;

    public float speed;
    public int damage;
    // Start is called before the first frame update
    private void Start()
    {
        //find position of player when bullet is initially instantiated
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        targetPosition = playerScript.transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        //if inside of player
        if ((Vector2)transform.position == targetPosition)
        {
            Destroy(gameObject);
            
        }
        //else move towards player
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerScript.TakeDamage(damage);
            Destroy(gameObject);
        }
        
    }
}
