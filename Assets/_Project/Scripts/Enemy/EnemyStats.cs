using UnityEngine;

public class EnemyStats : EntityStats {

    [SerializeField] private EnemyManager scrManager = null;

    [SerializeField] private Transform posRocket = null;
    [SerializeField] private Transform posPlayer = null;
    [SerializeField] private eEntityState state = eEntityState.Idle;
    [SerializeField] private eTargetEnemy typeTarget = eTargetEnemy.rocket;

    [Header("Money")]
    [SerializeField] private int min = 10;
    [SerializeField] private int max = 100;

    public int AmountMoney => Random.Range(min, max);

    [Header("Distans")]
    [SerializeField] private float distanFind = 15f;

    [SerializeField] private float distanWaifu = 3f;
    [SerializeField] private float distanRocket = 4f;
    [SerializeField] private float distanPlayer = 2f;

    [SerializeField] private float distanCurrent = 5f;

    [SerializeField] private float timeStunt = 1f;
    [SerializeField] private float powerAttack = 10f;
    public float PowerAttack => powerAttack;
    [SerializeField] private float cooldownAttack = 10f;
    public float CooldownAttack => cooldownAttack;

    private bool isAttack = true;
    public bool IsAttack => isAttack;
    public void SetIsAttack(bool _val) => isAttack = _val;

    public float DistanFind => distanFind;

    public float DistanCurrent => distanCurrent;
    public void SetDistanCurrent(eTargetEnemy _type) {
        typeTarget = _type;

        switch(typeTarget) {
            case eTargetEnemy.rocket:
                distanCurrent = distanRocket;
                break;
            case eTargetEnemy.player:
                distanCurrent = distanPlayer;
                break;
            case eTargetEnemy.waifu:
                distanCurrent = distanWaifu;
                break;
        }
    }

    private bool waifuEffect = false;
    public bool WaifuEffect => waifuEffect;
    public void SetWaifuEffect(bool _val) => waifuEffect = _val;

    public override void Inicializar() {
        base.Inicializar();
        ModiState(eEntityState.Idle);
        NormalStats();
        DobleStats();
    }

    private float dobleSpeed;
    private float dobleAttack;
    private float dobleMaxHealth;

    private float normalSpeed;
    private float normalAttack;
    private float normalMaxHealth;

    private void DobleStats() {
        dobleMaxHealth = normalMaxHealth * 2;
        dobleAttack = normalAttack * 2;
        dobleSpeed = normalSpeed * 2;
    }

    private void NormalStats() {
        normalSpeed = speedMove;
        normalAttack = powerAttack;
        normalMaxHealth = healthMax;
    }

    public void StartDoble() {
        speedMove = dobleSpeed;
        powerAttack = dobleAttack;
        healthMax = dobleMaxHealth;
        healthCurrent *= 2;
    }

    public void StartNormal() {
        speedMove = normalSpeed;
        powerAttack = normalAttack;
        healthCurrent /= 2;
        healthMax = normalMaxHealth;
    }

    public void GetManager(EnemyManager _manager) => scrManager = _manager;
    public EnemyManager Manager => scrManager;

    public void GetPosRocket(Transform _pos) => posRocket = _pos;
    public Transform PosRocket => posRocket;

    public void GetPosPlayer(Transform _pos) => posPlayer = _pos;
    public Transform PosPlayer => posPlayer;

    public eEntityState State => state;
    public void ModiState(eEntityState _mode) => state = _mode;
    public float TimeStunt => timeStunt;

    public override void ModiCurrentHealth(float value) {
        base.ModiCurrentHealth(value);
    }
}