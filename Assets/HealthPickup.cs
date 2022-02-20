using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    //Reference to player script
    Player playerScript;
    public int healAmount;

    private void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    
    // If player has collided with pickup
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            // Call player scripts Heal function and pass in the amount you want to heal the player by
            playerScript.Heal(healAmount);
            // Remove Heart pickup from scene
            Destroy(gameObject);
        }
    }
}
