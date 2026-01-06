using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    private Transform posTarget;

    public eTypeEnemy type;

    protected Animator compAni;
    private NavMeshAgent compAgent;
    private Rigidbody compRb;
    private EnemyStats scrStats;

    [SerializeField] private SphereCollider damageTrigger = null;

    protected const string ANI_WALK = "isWalk";
    protected const string ANI_ATTACK = "isAttack";
    protected const string ANI_DEAD = "isDead";

    //Lectura de vida por las torres
    public float HealthRead => scrStats.CurrentHealth;
    protected float CooldownAttack => scrStats.CooldownAttack;

    private void Awake() {
        compRb = GetComponent<Rigidbody>();
        compAgent = GetComponent<NavMeshAgent>();
        scrStats = GetComponent<EnemyStats>();
        compAni = GetComponent<Animator>();
    }

    //Metodo llamado por la waifu
    public void ChangeTarget(Transform _pos) {
        scrStats.SetWaifuEffect(true);
        scrStats.SetDistanCurrent(eTargetEnemy.waifu);
        posTarget = _pos;
    }

    //Metodo llamado por la waifu
    public void DefaultTarget() {
        scrStats.SetWaifuEffect(false);
        posTarget = scrStats.PosRocket;
    }

    private void Update() {
        if(scrStats.Manager.IsStopGame) {
            compAgent.velocity = Vector3.zero;
            return;
        }

        TargetDefinition();
        transform.LookAt(posTarget.position);
        compAgent.speed = scrStats.SpeedMove;

        if(scrStats.State == eEntityState.Death) {
            compAgent.velocity = Vector3.zero;
            compRb.velocity = Vector3.zero;
            return;
        }

        if(scrStats.State != eEntityState.Stunt) {
            float distan = Vector3.Distance(transform.position, posTarget.position);
            if(distan <= scrStats.DistanCurrent) {
                compAgent.velocity = Vector3.zero;
                scrStats.ModiState(eEntityState.Attack);
                compAni.SetBool(ANI_WALK, false);

                if(scrStats.IsAttack)
                    PerformAttack();

            } else {
                scrStats.ModiState(eEntityState.Walk);
                compAgent.SetDestination(posTarget.position);
                compAni.SetBool(ANI_WALK, true);
            }
        } else {
            compAgent.velocity = Vector3.zero;
        }
    }

    private void TargetDefinition() {
        if(scrStats.WaifuEffect)
            return;

        float distan = Vector3.Distance(transform.position, scrStats.PosPlayer.position);

        if(distan <= scrStats.DistanFind) {
            posTarget = scrStats.PosPlayer;
            scrStats.SetDistanCurrent(eTargetEnemy.player);
        } else {
            posTarget = scrStats.PosRocket;
            scrStats.SetDistanCurrent(eTargetEnemy.rocket);
        }
    }

    public void ModiHealth(float amount) {
        scrStats.ModiCurrentHealth(amount);

        if(scrStats.CurrentHealth <= 0f) {
            //Activa animacion de Muerte
            compAni.SetBool(ANI_DEAD, true);
            scrStats.ModiState(eEntityState.Death);
        }
    }

    //Llamado desde Update
    protected virtual void PerformAttack() {
        scrStats.SetIsAttack(false);
        damageTrigger.enabled = true;
    }

    //Llamar desde linea de animacion
    public virtual void EventAttack() {
        compAni.SetBool(ANI_ATTACK, false);
        StartCoroutine(CoolDownAttack());
        damageTrigger.enabled = false;
    }

    IEnumerator CoolDownAttack() {
        yield return new WaitForSeconds(CooldownAttack);
        scrStats.SetIsAttack(true);
    }

    //Llamar desde linea de animacion
    public void EventDead() {
        scrStats.Manager.GiveMoneyBag(transform, scrStats.AmountMoney);
        DeadOfState();
        compAni.SetBool(ANI_DEAD, false);
    }

    private void DeadOfState()
    {
        switch (type)
        {
            case eTypeEnemy.Fase_I:
                scrStats.Manager.Pool(this.gameObject);
                break;

            case eTypeEnemy.Boss:
                scrStats.Manager.PoolBoss(this.gameObject);
                break;
        }
    }

    IEnumerator Damage(float _damage, float _impact) {
        scrStats.ModiState(eEntityState.Stunt);
        compAgent.velocity = Vector3.zero;
        yield return new WaitForSeconds(scrStats.TimeStunt);
        scrStats.ModiState(eEntityState.Idle);
        ModiHealth(_damage * -1);
        compRb.velocity = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("BulletPlayer")) {
            Bullet temp = other.gameObject.GetComponent<Bullet>();
            if(temp != null) {
                float damage = temp.Damage;
                float impact = temp.ImpactForce;
                StartCoroutine(Damage(damage, impact));
            }
        }

        if(other.gameObject.CompareTag("BulletTower")) {
            BulletTower temp = other.gameObject.GetComponent<BulletTower>();
            if(temp != null) {
                float damage = temp.Damage;
                float impact = temp.ImpactForce;
                StartCoroutine(Damage(damage, impact));
            }
        }

        if(other.gameObject.CompareTag("PowerUpEnemy")) {
            scrStats.StartDoble();
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.CompareTag("PowerUpEnemy")) {
            scrStats.StartNormal();
        }
    }
}