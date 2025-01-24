using UnityEngine;

public class CheetoAbility : Ability
{
    public CheetoAbilityData data;
    [HideInInspector]
    public override AbilityData abilityData => data;
}