using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WPTracker : MonoBehaviour
{
    public GameObject[] waypointsAvailable;
    public bool isDivergent;

    public GameObject[] getWaypoints()
    {
        return waypointsAvailable;
    }
}
