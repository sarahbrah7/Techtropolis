using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    //allows for gravity to be applied to the object
    Rigidbody rb;

    //checks if the dice has landed
    bool hasLanded;
    //checks if the dice hasbeen thrown
    bool thrown;
    //stores the set position of the dice
    Vector3 initPosition;
    //holds the dice sides and their values
    public DiceSide[] diceSides;
    public int diceValue;

    //commences when the gameplay starts
    void Start()
    {
        //stores the set deafult position of the dice
        initPosition = transform.position;
        //keeps gravity off as dice has not yet been rolled
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    //function would demostrate physical dice roll
    public void RollDice()
    {
        //the dice is reset
        Reset();
        //if it has not been thrown or landed yet then
        if(!thrown && !hasLanded)
        {
            //the dice would then be thrown
            thrown = true;
            //gravity is set to true meaning dice is thrown to the ground
            rb.useGravity = true;
            //demonstrates a simulation of the dice beign thrown
            rb.AddTorque(Random.Range(0, 500), Random.Range(0, 500), Random.Range(0, 500));
        }

        //if it has been thrown and has landed
        else if(thrown && hasLanded)
        {
            //Reset Dice
            Reset();
        }
    }

    //this would reset the dice's attributes
    void Reset()
    {
        //would be set to the default position
        transform.position = initPosition;
        //gravity is set to false
        rb.isKinematic = false;
        //the dice has not been thrown or landed as of yet
        thrown = false;
        hasLanded = false;
        rb.useGravity = false;
    }

    //intiates wheneverthe gameplay updates
    void Update()
    {
        //checks to see if there has been an invalid throw
        if (rb.IsSleeping() && !hasLanded && thrown)
        {
            hasLanded = true;
            rb.useGravity = false;
            rb.isKinematic = true;

            //if so then the output is checked
            // ... to see if it is valid
            SideValueCheck();
        }
        //if there is a valid output of 0
        else if(rb.IsSleeping() && hasLanded && diceValue == 0)
        {
            //the dice is rolled again
            RollAgain();
        }
    }

    //rolls the dice again if there is an invalid output
    void RollAgain()
    {
        //the dice is reset
        Reset();
        //then the dice is rolled once again
        thrown = false;
        rb.useGravity = true;
        rb.AddTorque(Random.Range(0, 500), Random.Range(0, 500), Random.Range(0, 500));
    }

    //checks for the output from rolling the dice
    void SideValueCheck()
    {
        //the default output is set
        diceValue = 0;
        //checks throguh each dice sid to see if they have been landed on
        foreach(DiceSide side in diceSides)
        {
            if (side.OnGround())
            {
                diceValue = side.sideValue;
                //send result to gameManager
                GameManager.instance.RollDice(diceValue);
            }
        }
    }
}
