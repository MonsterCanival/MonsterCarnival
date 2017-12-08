using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BehaviableState {

    public bool bCanAttack { get; set; }

    public bool bIsOwned { get; set; }

    public bool bCanControl { get; set; }
    public bool bCanSingleSkill { get; set; }
    public bool bCanMultipleSkill { get; set; }
    public bool bCanSpeedSkill { get; set; }

    public BehaviableState(int flag)
    {
        bCanAttack = true;
        bCanControl = true;
        bCanSingleSkill = true;
        bCanMultipleSkill = true;
        bCanSpeedSkill = true;
        bIsOwned = false;
    }

    public void SetBCanAttackTrue()
    {
        bCanAttack = true;
    }
    public void SetBCanAttackFalse()
    {
        bCanAttack = false;
    }

    public void SetBIsOwnedTrue()
    {
        bIsOwned = true;
    }
    public void SetbIsOwnedFalse()
    {
        bIsOwned = false;
    }

    public void SetBCanControlTrue()
    {
        bCanControl = true;
    }
    public void SetBCanControlFalse()
    {
        bCanControl = false;
    }

    public void SetbCanSingleSkillTrue()
    {
        bCanSingleSkill = true;
    }
    public void SetbCanSingleSkillFalse()
    {
        bCanSingleSkill = false;
    }

    public void SetbCanMultipleSkillTrue()
    {
        bCanMultipleSkill = true;
    }
    public void SetbCanMultipleSkillFalse()
    {
        bCanMultipleSkill = false;
    }

    public void SetbCanSpeedSkillTrue()
    {
        bCanSpeedSkill = true;
    }
    public void SetbCanSpeedSkillFalse()
    {
        bCanSpeedSkill = false;
    }

}
