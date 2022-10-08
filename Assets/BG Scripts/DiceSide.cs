using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSide : MonoBehaviour
{
    //checks if dice is on the ground
    bool onGround;
    //holds the value of each side
    public int sideValue;

    //checks to see which side of the dice is on the ground
    void OnTriggerStay(Collider col)
    {
        if(col.CompareTag("Ground"))
        {
            onGround = true;
        }
    }

    //then every other side is then identified as ...
    //not being on the ground
    void OnTriggerExit(Collider col)
    {
        if(col.CompareTag("Ground"))
        {
            onGround = false;
        }
    }

    //if the side is on the ground
    //then the dice side would be identified 
    //as such
    public bool OnGround()
    {
        return onGround;
    }
}
