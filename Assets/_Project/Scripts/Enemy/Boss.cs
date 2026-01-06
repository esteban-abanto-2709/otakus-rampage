using UnityEngine;

public class Boss : EnemyController {

    private const string ANI_ATTACK1 = "isAttack1";
    private const string ANI_ATTACK2 = "isAttack2";

    [SerializeField] private int numersAttack = 3;
    private int currentAttacks = 0;

    protected override void PerformAttack() {
        base.PerformAttack();
        compAni.SetBool(ANI_ATTACK, true);
    }

    private void AttackBoss()
    {
        currentAttacks++;

        if (currentAttacks > numersAttack)
            compAni.SetBool(ANI_ATTACK2, true);
        else
            compAni.SetBool(ANI_ATTACK1, true);
    }

    /*
    public override void EventAttack() {
        base.EventAttack();
        compAni.SetBool(ANI_ATTACK2, false);
        compAni.SetBool(ANI_ATTACK1, false);
    }
    */
}