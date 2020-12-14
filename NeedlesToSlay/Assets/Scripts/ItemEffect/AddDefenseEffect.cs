using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreatorKitCode;

public class AddDefenseEffect : UsableItem.UsageEffect
{
    public int DefenseAmount = 10;

    public override bool Use(CharacterData user)
    {
        user.Stats.ChangeHealth(DefenseAmount);
        return true;
    }
}
