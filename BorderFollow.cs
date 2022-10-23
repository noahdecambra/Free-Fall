using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderFollow : MonoBehaviour
{
    public Transform target;
    public BoxCollider rightScreen;
    public BoxCollider leftScreen;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //has border follow the target object (player)
        if(target.position.y < transform.position.y)
        {
            Vector3 newPosition = new Vector3(transform.position.x, target.position.y, transform.position.z);
            transform.position = newPosition;
        }
    }    

    void OnTriggerEnter(Collider collision)
    {
        //checks if the collision is the player and whether it's x position is < or > to determine if it is on the left or right side of the screen
        if(collision.gameObject.tag == "Player")
        {
            if(target.position.x > 0f)
            {
                Vector3 leftBorder = new Vector3(leftScreen.center.x + 2f, target.position.y, 0f);
                target.transform.position = leftBorder;
            }
            else if(target.position.x < 0f)
            {
                Vector3 rightBorder = new Vector3(rightScreen.center.x - 2f, target.position.y, 0f);
                target.transform.position = rightBorder;
            }
        }
    }
}
