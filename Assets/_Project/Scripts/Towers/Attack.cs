using UnityEngine;

public class Attack : ForEnemy {

    [SerializeField] private float[] damage = new float[3];
    protected float currentDamage => damage[(int)level];

    protected override void LackOfAmmunition() {
        while (enemy.Count != 0) {
            EnemyController temp = enemy[0];
            enemy.Remove(temp);
        }
    }
}