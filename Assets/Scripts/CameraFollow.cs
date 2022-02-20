using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform playerTransform;
    public float speed;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    // Start is called before the first frame update
    private void Start()
    {
        //camera is in same position as player
        transform.position = playerTransform.position;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        //if player is not dead
        if (playerTransform != null)
        {
            //restrain player transform between min x variable and max x variable
            float clampedX = Mathf.Clamp(playerTransform.position.x, minX, maxX);
            float clampedY = Mathf.Clamp(playerTransform.position.y, minY, maxY);

            //smoothly move from one position to the other by a certain speed
            transform.position = Vector2.Lerp(transform.position, new Vector2(clampedX,clampedY), speed);
        }
    }
}
