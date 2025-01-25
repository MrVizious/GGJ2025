using Cysharp.Threading.Tasks;
using UnityEngine;
using UtilityMethods;

public class CheetoAbility : Ability
{
    public CheetoAbilityData data;
    [HideInInspector]
    public override AbilityData abilityData => data;

    public override async UniTask<bool> Perform(BossController bossController)
    {
        if (!await base.Perform(bossController)) return false;
        Cheeto newCheeto = (await VarietyPool.GetInstance()).Get<Cheeto>();
        newCheeto.transform.position = bossController.abilityStartTransform.position;
        UniTaskMethods.DelayedFunction(() => newCheeto.Release(), 5f).Forget();
        return true;
    }
}