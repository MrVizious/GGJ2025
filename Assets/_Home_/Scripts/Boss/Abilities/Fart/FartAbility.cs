using Cysharp.Threading.Tasks;
using ExtensionMethods;
using UnityEngine;
using UtilityMethods;

public class FartAbility : Ability
{
    public FartAbilityData data;
    public float targetMoveSpeed = 25f;
    [HideInInspector]
    public override AbilityData abilityData => data;

    public override bool hasChargeMethod => false;

    public override async UniTask<bool> Perform(BossController bossController)
    {
        if (!await base.Perform(bossController)) return false;
        GameObject newFart = Instantiate(data.fartPrefab, data.fartPrefab.transform.position, data.fartPrefab.transform.rotation);
        Destroy(newFart, 6f);
        return true;
    }
}