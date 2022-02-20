using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public float speed;
    public float timeBetweenAttacks;
    public int damage;

    //chance for pickup to drop
    public int pickupChance;
    //contains all pickups
    public GameObject[] pickups;

    //this is because we do not want to see it in the unity inspector just allow the enemies to access the players transform
    [HideInInspector]
    public Transform player;

    //handling enemy health and taking damage
    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;

        //if enemy dies
        if (health<= 0)
        {
            //101 because random.range is exclusive
            int randomNumber = Random.Range(0, 101);
            if(randomNumber < pickupChance)
            {
                //random pickup from pickup array
                GameObject randomPickup = pickups[Random.Range(0, pickups.Length)];
                //spawn where enemy died
                Instantiate(randomPickup, transform.position, transform.rotation);
            }
            Destroy(gameObject);
        }
    }

   
    // Start is called before the first frame update.
    //virtual allows subclasses to override this function.
    public virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}