using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum States
{
    IDLE, NEUTRAL, ATTACK, MOVE_ATTACK
}

public struct BehaveState {
    public States Current { get; set; }

    public BehaveState(States states)
    {
        Current = states;
    }
}
