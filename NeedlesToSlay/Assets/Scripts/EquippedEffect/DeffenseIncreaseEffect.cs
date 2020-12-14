using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreatorKitCode;

public class DeffenseIncreaseEffect : EquipmentItem.EquippedEffect
{
     public float duration = 15.0f;
     public int DefenseChange = 15; 
     public Sprite EffectSprite;

     public override void Equipped(CharacterData user)
     {
          StatSystem.StatModifier modifier = new StatSystem.StatModifier();
          modifier.ModifierMode = StatSystem.StatModifier.Mode.Absolute;
          modifier.Stats.defense = DefenseChange;

     }
     
     public override void Removed(CharacterData user)
     {
          
     }
}
