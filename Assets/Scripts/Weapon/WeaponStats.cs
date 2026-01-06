using UnityEngine;

public class WeaponStats : MonoBehaviour {

    private AudioSource compAudi;

    [SerializeField] private eTypeConsume consume = eTypeConsume.normal;
    [SerializeField] private eModeGun mode = eModeGun.manual;
    [SerializeField] private float coolDown = 50f;
    [SerializeField] private Transform posSpawn = null;

    [Header("Municion")]
    [SerializeField] private Transform barFeedBack = null;
    [SerializeField] private GameObject objBullet = null;
    [SerializeField] private float ammoMax = 30f;
    private float ammoCurrent = 30f;
    [SerializeField] private float ammoLess = 1f;

    [Header("Recarga")]
    [SerializeField] private AudioClip audiReload = null;
    [SerializeField] private Animator compAni = null;
    [SerializeField] private float reloadTime = 1f;

    [Header("Bullet")]
    [SerializeField] private AudioClip audiShoot = null;
    [SerializeField] private float damage = 10f;
    [SerializeField] private float impactForce = 10f;
    [SerializeField] private float forceShoot = 4f;

    public static float ammoGeneral = 300f;

    private void Awake() {
        compAudi = GetComponent<AudioSource>();

        ammoCurrent = ammoMax;
    }

    public float ReloadTime() {
        return reloadTime;
    }

    public float AmmoCurrent() {
        if(ammoCurrent <= 0)
            ammoCurrent = 0;

        return ammoCurrent;
    }

    public float CoolDown() {
        return coolDown;
    }

    float nextTimefire;
    public void LogicCoolDown(ref bool _value) {
        switch(mode) {
            case eModeGun.auto:
                if(nextTimefire > Time.time) {
                    _value = false;
                } else {
                    nextTimefire = Time.time + 1f / coolDown;
                    _value = true;
                }
                break;
            case eModeGun.manual:
                if(nextTimefire < Time.time)
                    _value = true;
                else
                    _value = false;

                nextTimefire = Time.time + 1f / coolDown;
                break;
        }
    }

    private void AudiShoot() {
        if(!audiShoot)
            return;

        compAudi.clip = audiShoot;
        compAudi.Play();
    }

    private void LogicShoot() {
        ammoCurrent -= ammoLess;
    }

    public void CompShoot() {
        LogicShoot();
        AudiShoot();
    }

    private void AudiReload() {
        if(!audiReload)
            return;

        compAudi.clip = audiReload;
        compAudi.Play();
    }

    public void LogicReload() {
        switch(consume) {
            case eTypeConsume.normal:
                float llenado = ammoMax - ammoCurrent;
                float amountReload = Mathf.Min(ammoGeneral, llenado);
                ammoCurrent += amountReload;
                ammoGeneral -= amountReload;
                break;

            case eTypeConsume.unlimied:
                ammoCurrent = ammoMax;
                break;
        }
    }

    public void CompReload() {
        LogicReload();
        AudiReload();
    }

    public void FeedBackAmmo() {
        float temp = ammoCurrent / ammoMax;
        barFeedBack.localScale = new Vector3(1f, temp, 1f);
    }

    public void AnimReload(bool _value) {
        compAni.SetBool("Reloading", _value);
    }

    public float ForceShoot() {
        return forceShoot;
    }

    public float Damage() {
        return damage;
    }

    public float ImpactForce() {
        return impactForce;
    }

    public GameObject Bullet() {
        return objBullet;
    }

    public Transform Spawn() {
        return posSpawn;
    }
}