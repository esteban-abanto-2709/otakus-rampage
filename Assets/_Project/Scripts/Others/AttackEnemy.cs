using UnityEngine;

public class AttackEnemy : MonoBehaviour {

    [SerializeField] private EnemyStats scrStats = null;

    public float Damage => scrStats.PowerAttack;
}