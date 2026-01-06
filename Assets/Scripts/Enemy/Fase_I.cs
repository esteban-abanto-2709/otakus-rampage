using UnityEngine;

public class Fase_I : EnemyController {

    protected override void PerformAttack() {
        base.PerformAttack();
        compAni.SetBool(ANI_ATTACK, true);
    }
}