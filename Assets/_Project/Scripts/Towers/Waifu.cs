using UnityEngine;

public class Waifu : ForEnemy {

    [SerializeField] private Transform waifuPanel = null;
    [SerializeField] private int maxEnemies = 6;

    private void Update() {
        Vector3 posLook = PosPlayer.position;
        posLook.y = waifuPanel.position.y;

        waifuPanel.LookAt(posLook);
    }

    protected override void LackOfAmmunition() {
        while (enemy.Count != 0) {
            EnemyController temp = enemy[0];
            temp.DefaultTarget();
            enemy.Remove(temp);
        }
    }

    protected override void ActionWhenAddList(EnemyController temp) {
        if(enemy.Count <= maxEnemies) {
            temp.ChangeTarget(transform);
        } else {
            temp.DefaultTarget();
            enemy.Remove(temp);
        }
    }

    private void OnDestroy() {
        LackOfAmmunition();
    }
}