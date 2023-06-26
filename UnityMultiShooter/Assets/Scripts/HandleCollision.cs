using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleCollision : MonoBehaviour
{
    public bool isColliding = false;
    public int collisionCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Map")
        {
            isColliding = true;
            collisionCount++;
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.tag == "Map")
        {
            isColliding = false;
            collisionCount--;
        }
    }
}
