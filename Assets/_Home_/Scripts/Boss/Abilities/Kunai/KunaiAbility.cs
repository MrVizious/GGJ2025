using UnityEngine;

public class KunaiAbility : Ability
{
    public override bool hasChargeMethod => false;
    public KunaiAbilityData data;
    [HideInInspector]
    public override AbilityData abilityData => data;
}
