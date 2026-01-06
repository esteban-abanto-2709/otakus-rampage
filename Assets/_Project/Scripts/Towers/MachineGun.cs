using UnityEngine;

public class MachineGun : Attack {

    [SerializeField] private Transform[] head = new Transform[3];
    private Transform Currenthead => head[(int)level];

    [SerializeField] private Transform[] pointSpawn = new Transform[3];
    private Transform CurrentPointPosition => pointSpawn[(int)level];
    [SerializeField] private Transform target = null;

    [SerializeField] private float force = 10f;
    [SerializeField] private float impactForce = 10f;

    protected override void Attack() {
        ReElectiontTarget();
        LookInY(target.position);
        manager.GiveBulletTower(CurrentPointPosition, force, currentDamage, impactForce);
    }

    private void LookInY(Vector3 _target) {
        Vector3 temp = _target;
        temp.y = Currenthead.position.y;
        Currenthead.LookAt(temp);
    }

    private void ReElectiontTarget() {
        if(enemy.Count != 0) {
            float minDist = 50f;
            for(int i = 0;i < enemy.Count;i++) {
                float dista = Vector3.Distance(transform.position, enemy[i].transform.position);
                if(dista < minDist) {
                    minDist = dista;
                    target = enemy[i].transform;
                }
            }
        }
    }

}