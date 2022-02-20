using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MeleeEnemy : Enemy 
//derive from the enemy class
{
    public float stopDistance;
    //time between attacks so that enemy doesn't hit every frame
    private float attackTime;
    public float attackSpeed;

    // Update is called once per frame
    void Update()
    {
        //if player is not dead
        if (player != null)
        {
            //if too far away from player, move towards (a bit like lerp function), pass in current position, player position, and how fast
            if(Vector2.Distance(transform.position, player.position) > stopDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
            else
            {
                if(Time.time >= attackTime)
                {
                    //attack
                    StartCoroutine(Attack());
                    attackTime = Time.time + timeBetweenAttacks;
                }
            }
        }
    }

    //can't call a function as it would last a single frame, a co-routine operates over a set period of time
    IEnumerator Attack()
    {
        //get player script, make player take damage
        player.GetComponent<Player>().TakeDamage(damage);
        //position before leap
        Vector2 originalPosition = transform.position;
        Vector2 targetPosition = player.position;

        //how much of the animation is done yet
        float percent = 0;
        //if percent = 1 animation is complete
        while(percent <= 1)
        {
            //time.deltatime adds onto the percent every frame.
            percent += Time.deltaTime * attackSpeed;
            //Variable enables to go to target postion and return to original position based on percent
            float formula = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector2.Lerp(originalPosition, targetPosition, formula);
            //skip to next frame instead of finishing after the first one
            yield return null;
        }
    }
}
