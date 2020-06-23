﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class GAction : MonoBehaviour
{
    public string actionName = "Action";
    public float cost = 1.0f;

    public GameObject target;
    public string targetTag;

    public float duration = 0f;

    public WorldState[] preConditions;
    public WorldState[] afterEffects;

    public NavMeshAgent agent;

    public Dictionary<string, int> preconditions;
    public Dictionary<string, int> effects;

    public WorldStates agentBeliefs;
    
    // action performing?
    public bool running = false;

    // constructor
    public GAction()
    {
        preconditions = new Dictionary<string, int>();
        effects = new Dictionary<string, int>();
    }

    public void Awake()
    {
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        if(preConditions != null)
        {
            foreach(WorldState ws in preConditions)
            {
                preconditions.Add(ws.key, ws.value);
            }
        }

        if (afterEffects != null)
        {
            foreach (WorldState ws in afterEffects)
            {
                effects.Add(ws.key, ws.value);
            }
        }
    }

    public bool IsAchievable()
    {
        return true;
    }

    public bool IsAchievableGiven(Dictionary<string, int> conditions)
    {
        foreach(KeyValuePair<string, int> p in preconditions)
        {
            if(!conditions.ContainsKey(p.Key))
            {
                return false;
            }
        }
        return true;
    }

    public abstract bool PrePerform();
    public abstract bool PostPerform();
}
