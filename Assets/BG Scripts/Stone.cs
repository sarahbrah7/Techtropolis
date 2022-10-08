using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    [Header("ROUTES")]
    //holds the route the sprite should go around the board
    public Route commonRoute;
    //holds an infinite loop around the board
    public Route finalRoute;

    public List<Node> fullRoute = new List<Node>();

    [Header("NODES")]
    //the first node on the board
    //where the players start
    public Node startNode;

    public Node baseNode;
    //the nodes that the sprite is on
    public Node currentNode;
    public static Node exactNode;
    //the node that the sprite is moving to
    public Node goalNode;

    //the nodes which affects the player's sum
    public Node jackpotNode;
    public Node jailNode;
    public Node warningNode;

    //the position that the sprite is on
    int routePosition;
    //for the start position//
    int startNodeIndex;

    //rolled dice amount//
    int steps;
    //amoutn fo steps to do
    int refStep;
    //steps done overall
    int doneSteps;
    

    [Header("BOOLS")]

    //if the sprite has commenced playing
    public bool isOut;
    //which srpite is currently moving
    public bool isMoving;
    //for the human player//
    public bool hasTurn;


    //commences as the gameplay starts
    void Start()
    {
        //positions both sprites at the start node
        startNodeIndex = commonRoute.RequestPosition(startNode.gameObject.transform);
        //the sprite's full route would be the made
        CreateFullRoute();

        SetSelector(false);
    }

    //This is where i should consider the infinite loop//
    void CreateFullRoute()
    {
        //goes through every node in order of the childNode array
        for (int i = 0; i < commonRoute.childNodeList.Count; i++)
        {
            //sets them a position integer
            //compares the node position with the start node
            int tempPos = startNodeIndex + i;
            //avoids overflow of the list//
            tempPos %= commonRoute.childNodeList.Count;

            //adds on the node to the full route array
            fullRoute.Add(commonRoute.childNodeList[tempPos].GetComponent<Node>());
        }

        //creates the infinite route
        for (int i = 0; i < finalRoute.childNodeList.Count; i++)
        {
            //adds on the full route as the sprite iterates throguhout the board
            fullRoute.Add(finalRoute.childNodeList[i].GetComponent<Node>());
        }
        
    }

    //temporary update to roll dice
    //checked if the player would iterate throughout the board
    // NO LONGER NECESSARY
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space) && !isMoving)
        //{
        //    steps = Random.Range(1, 7);

        //    Debug.Log("Dice number " + steps);
        //    StartCoroutine(Move());
        //    if (doneSteps + steps < fullRoute.Count)
        //    {
        //        CreateFullRoute();
        //    }

        //}
    }

    //incharge of the sprite's movement around the board
    IEnumerator Move()
    {
        //sees if the sprite is already moving
        if (isMoving)
        {
            //woudl halt the action
            yield break;
        }

        //the sprite would now be allowed to move
        isMoving = true;

        //would move the sprite one step until the amount of steps to do is 0
        while (steps > 0)
        {
            //incremetns the sprite's position on the route
            routePosition++;
            //changs the set position  of the sprite
            Vector3 nextPos = fullRoute[routePosition].gameObject.transform.position;
            //if it hasn't reached the final position
            while (MoveToNextNode(nextPos)) { yield return null; }
            //waits for some time between steps
            yield return new WaitForSeconds(0.1f);
            //decrements the amoun of steps left
            steps--;
            //increments the amount of steps done
            doneSteps++;
            refStep = doneSteps;
        }

        //translates the steps done to represent the node that the player is on
        if (refStep > 14)
        {
            //uses mod function to find the node it represents
            refStep %= 14;

        }

        //shows on compiler log just as a check if it works as expected
        Debug.Log(refStep);
        //the exact node is the node is the specific node that the sprite is on
        exactNode = finalRoute.childNodeList[refStep].GetComponent<Node>();

        //the sprite is no longer moving
        isMoving = false;
        //the player chaneg its state to the money state
        GameManager.instance.state = GameManager.States.MONEY;

        

        
    }

    //physically moves the sprite to the next node
    bool MoveToNextNode(Vector3 goalPos)
    {
        //returns the physical moevement
        return goalPos != (transform.position = Vector3.MoveTowards(transform.position,goalPos,2f * Time.deltaTime));
        
    }
    //checks if the movement is possible
    //if it goes outside of the final route array
    public bool CheckPossible(int diceNumber)
    {
        //uses a temp varible to check the possible route
        int tempPos = routePosition + diceNumber;
        //checks if the route is greater than the full route
        if(tempPos >= fullRoute.Count)
        {
            return false;
        }

        return !fullRoute[tempPos].isTaken;

    }

    //would commence the sprites movement
    public void StartTheMove(int diceNumber)
    {
        steps = diceNumber;
        StartCoroutine(Move());
        //to create an infinite loop
        //checks if the total steps is greater than the full route 
        if (doneSteps + steps < fullRoute.Count)
        {
            //adds an extension ot the route
            CreateFullRoute();
        }

    }

    //----------HUMAN INPUT----------//

    public void SetSelector(bool on)
    {
        hasTurn = on;
         
    }


    
}
