using Cysharp.Threading.Tasks;
using ExtensionMethods;
using UnityEngine;
using UtilityMethods;

public class MonsterAbility : Ability
{
    public MonsterAbilityData data;
    public float targetMoveSpeed = 25f;
    [HideInInspector]
    public override AbilityData abilityData => data;

    public override bool hasChargeMethod => true;

    public override async UniTask<bool> Perform(BossController bossController)
    {
        if (!await base.Perform(bossController)) return false;
        GameObject newMonster = Instantiate(data.monsterPrefab, bossController.targetTransform.position, data.monsterPrefab.transform.rotation);
        Destroy(newMonster, 7f);
        return true;
    }

    public override void ChargeAbilityUpdate(BossController bossController)
    {
        base.ChargeAbilityUpdate(bossController);
        bossController.targetTransform.position = bossController.targetTransform.position.WithZ(bossController.abilityStartTransform.position.z);
        bossController.targetTransform.position += Vector3.right * targetMoveSpeed * Time.deltaTime;
    }
}