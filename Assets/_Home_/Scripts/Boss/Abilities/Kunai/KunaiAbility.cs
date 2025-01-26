using Cysharp.Threading.Tasks;
using UnityEngine;

public class KunaiAbility : Ability
{
    public override bool hasChargeMethod => false;
    public KunaiAbilityData data;
    [HideInInspector]
    public override AbilityData abilityData => data;
    public override async UniTask<bool> Perform(BossController bossController)
    {
        if (!await base.Perform(bossController)) return false;
        GameObject newCheeto = Instantiate(data.kunaiPrefab, bossController.abilityStartTransform.position, data.kunaiPrefab.transform.rotation);
        Destroy(newCheeto, 7f);
        return true;
    }
}