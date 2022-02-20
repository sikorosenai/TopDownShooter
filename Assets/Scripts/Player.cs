using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    
    //dictates how fast player will move around in unity, it is public so that it is editable in unity.
    public float speed;
    
    //contains default unity physics
    private Rigidbody2D rb;
    private Animator anim;

    private Vector2 moveAmount;

    private SceneTransition sceneTransition;

    // Start is called before the first frame update
    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sceneTransition = FindObjectOfType<SceneTransition>();
    }

    //fixed update function gets called every physics frame
    private void FixedUpdate()
    {
        //.fixedDeltaTime is used so that movement is framerate independent
        rb.MovePosition(rb.position + moveAmount * Time.fixedDeltaTime);
    }

    // Update is called once per frame
    private void Update()
    {
        //Vector2 variable is used to detect users input in x and y (only x and y are needed as this is a 2D game)
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        //the .normalized part of the function is so that the player does not go faster when moving diagonally
        moveAmount = moveInput.normalized * speed;

        //if player is moving then set the animation is running = true
        if (moveInput != Vector2.zero)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
    }

    

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        UpdateHealthUI(health);

        //if player health is less than 0 then destroy player object
        if (health <= 0)
        {
            Destroy(gameObject);
            sceneTransition.LoadScene("Death");
        }
    }

    public int health;
    //public array of heart images
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    void UpdateHealthUI(int currentHealth)
    {
        //while i variable is less than the length of hearts
        for (int i = 0; i < hearts.Length; i++)
        { 
            //if i is less than players current  health make heart array index i full heart
            if (i < currentHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }

    public void Heal(int healAmount)
    {
        // If health is max
        if (health + healAmount > 5)
        {
            // Set player health to normal max value
            health = 5;
        }
        else
        {
            // Add health
            health += healAmount;
        }
        UpdateHealthUI(health);
    }

    //destroy game object with weapon tag
    //instantiate new weapon at current position of player, pass in parent object
    public void ChangeWeapon(Weapon weaponToEquip)
    {
        Destroy(GameObject.FindGameObjectWithTag("Weapon"));
        Instantiate(weaponToEquip, transform.position , transform.rotation, transform);
    }

}
