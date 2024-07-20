using System.Collections.Generic;
using UnityEngine;

public class Environment: MonoBehaviour
{
    public Transform PlayerSpot;
    public ExitPortal ExitPortal;
    public List<Transform> EnemySpots = new List<Transform>();
}
