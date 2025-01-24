using Cysharp.Threading.Tasks;
using UnityEngine;

public class CheetoAbility : Ability
{
    public CheetoAbilityData data;
    [HideInInspector]
    public override AbilityData abilityData => data;

    public override async UniTask<bool> Activate()
    {
        if (!await base.Activate()) return false;
        Debug.Log($"Cheeto launched", this);
        return true;
    }
}