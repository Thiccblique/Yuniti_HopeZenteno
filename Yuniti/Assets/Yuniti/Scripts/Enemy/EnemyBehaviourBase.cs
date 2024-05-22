using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviourBase : MonoBehaviour
{
    public static EnemyBehaviourBase instance;

    public bool hasLooped = false;
    public bool hasHitFirstPoint = false;
    public bool isGoingClockwise = false;
    public bool hasHitSpecialPoint = false;
}
