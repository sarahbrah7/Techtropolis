using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sprite : MonoBehaviour
{

    public Transform[] waypoints;

    [SerializeField]
    public float moveSpeed = 1f;

    [HideInInspector]
    public int waypointIndex = 0;

    public bool moveAllowed = false;

    private void Start()
    {
        transform.position = waypoints[waypointIndex].transform.position;
    }

    private void Update()
    {
        if (moveAllowed)
            Move();
    }

    private void Move()
    {
        if (waypointIndex <= waypoints.Length - 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, moveSpeed * Time.deltaTime);

            if (transform.position == waypoints[waypointIndex].transform.position)
            {
                waypointIndex += 1;
            }
        }
    }

}
