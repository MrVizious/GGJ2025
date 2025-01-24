using UnityEngine;

public class KunaiAbility : Ability
{
    public KunaiAbilityData data;
    [HideInInspector]
    public override AbilityData abilityData => data;
}
