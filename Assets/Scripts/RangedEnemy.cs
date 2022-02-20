using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
    public float stopDistance;
    private float attackTime;
    private Animator anim;
   

    // Override main enemy start function
    public override void Start()
    {
        // base.Start means the function still calls the start code from the enemy class
        base.Start();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (player != null)
        {
            // If too far away from player, move towards (a bit like lerp function), pass in current position, player position, and how fast
            if (Vector2.Distance(transform.position, player.position) > stopDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
            else
            {
                if (Time.time >= attackTime)
                {
                    //attack
                    
                    attackTime = Time.time + timeBetweenAttacks;
                    anim.SetTrigger("Attack");
                }
            }
        }
            
    }
    public Transform shotPoint;
    public GameObject enemyBullet;
    //copied from weapon script
    public void RangedAttack()
    {
        //direction of player from shotpoint
        Vector2 direction = player.position - shotPoint.position;
        //make that into a quaternion rotation
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotationBullet = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        shotPoint.rotation = rotationBullet;
        //instantiate the bullet prefab at the shot point, pointed towards the player
        Instantiate(enemyBullet, shotPoint.position, shotPoint.rotation);

    }
}
