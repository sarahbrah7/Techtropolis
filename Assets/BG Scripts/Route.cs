using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    Transform[] childNodes;
    //Created a dynamic array to store the nodes//
    public List<Transform> childNodeList = new List<Transform>();

    void Start()
    {
        //as gameplay commences then the nodes on the board ...
        //... is stored in an array
        FillNodes();

    }
    //will draw out a path between the nodes//
    void OnDrawGizmos()
    {
        // the route visible on the Unity builder would be green
        Gizmos.color = Color.green;
        FillNodes();

        //goes through every node in the childNodeList ...
        // ... then map out the sprite's route using gizmos
        for(int i=0; i < childNodeList.Count; i++)
        {
            Vector3 pos = childNodeList[i].position;
            //checks if the variable represents the nodes ...
            //... after the first one
            if (i > 0)
            {
                //records the position of the node
                Vector3 prev = childNodeList[i-1].position;
                //the route is physically made uing the green line
                Gizmos.DrawLine(prev, pos);
            }
        }
    }
    
    //this function creates an array with the board's nodes
    //in order
    void FillNodes()
    {
        //Empties the list//
        childNodeList.Clear();

        childNodes = GetComponentsInChildren<Transform>();
    
        //done in order that the nodes are on the board
        foreach (Transform child in childNodes)
        {
            if(child != this.transform)
            {
                //adds every node on the board onto the list
                childNodeList.Add(child);
            }
        }
    }

    //for the stone script//
    public int RequestPosition(Transform nodeTransform)
    {
        //checks which node the sprite is on
        return childNodeList.IndexOf(nodeTransform);
    }
}
