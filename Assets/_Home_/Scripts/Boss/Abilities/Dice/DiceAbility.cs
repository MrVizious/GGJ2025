using Cysharp.Threading.Tasks;
using ExtensionMethods;
using UnityEngine;
using UtilityMethods;

public class DiceAbility : Ability
{
    public DiceAbilityData data;
    public float targetMoveSpeed = 25f;
    [HideInInspector]
    public override AbilityData abilityData => data;

    public override bool hasChargeMethod => true;

    public override async UniTask<bool> Perform(BossController bossController)
    {
        if (!await base.Perform(bossController)) return false;
        GameObject newCheeto = Instantiate(data.dicePrefab, bossController.targetTransform.position, data.dicePrefab.transform.rotation);
        Destroy(newCheeto, 7f);
        return true;
    }
    public override void ChargeAbilityUpdate(BossController bossController)
    {
        base.ChargeAbilityUpdate(bossController);
        bossController.targetTransform.position = bossController.targetTransform.position.WithZ(bossController.abilityStartTransform.position.z);
        bossController.targetTransform.position += Vector3.right * targetMoveSpeed * Time.deltaTime;
    }
}