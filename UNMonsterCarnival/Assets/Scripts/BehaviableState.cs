using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BehaviableState {

    public bool bCanAttack { get; set; }
    public bool bCanControl { get; set; }

    public BehaviableState(int flag)
    {
        bCanAttack = true;
        bCanControl = true;
    }
}
