using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject projectile;
    public Transform shotPoint;
    public float timeBetweenShots;

    private float shotTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //equal to the direction between the mouse position and the weapon position 
        //have to use screen to world point as mouse position returns a pixel coordinate but transform returns a world point
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //take direction and transform it into an angle
        //mathf.atan2 returns the angle in radians of the direction, then convert radians into degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //convert angle into a unity rotation (a quaternion)
        //pass in angle and on which axis you want to rotate the object, for 2D use the Z axis (.forward)
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        //Quaternion rotationBullet = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        //set rotation of weapon = to new rotation
        transform.rotation = rotation;

        //when left mouse button pressed
        if(Input.GetMouseButton(0))
        {
            //game time is more than shotTime
            if (Time.time >= shotTime)
            {
                Instantiate(projectile, shotPoint.position, rotation);
                //recalculate shot time
                shotTime = Time.time + timeBetweenShots;
            }
        }
    }
}

