using Cysharp.Threading.Tasks;
using ExtensionMethods;
using UnityEngine;
using UtilityMethods;

public class CheetoAbility : Ability
{
    public CheetoAbilityData data;
    public float targetMoveSpeed = 25f;
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

    public override void ChargeAbilityUpdate(BossController bossController)
    {
        base.ChargeAbilityUpdate(bossController);
        bossController.targetTransform.position = bossController.targetTransform.position.WithZ(bossController.abilityStartTransform.position.z);
        bossController.targetTransform.position += Vector3.right * targetMoveSpeed * Time.deltaTime;
    }
}