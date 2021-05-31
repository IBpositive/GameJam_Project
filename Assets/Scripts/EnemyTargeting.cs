using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyTargeting : MonoBehaviour
{
    private void Awake()
    {
        gameObject.GetComponent<AIDestinationSetter>().target = GameObject.Find("Player").transform;
    }
}
