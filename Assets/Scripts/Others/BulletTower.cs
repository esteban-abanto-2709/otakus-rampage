using UnityEngine;

public class BulletTower : MonoBehaviour {

    private PoolManager manager = null;

    private Rigidbody compRb = null;
    private float force = 0f;
    private float damage = 0f;
    private float impactForce = 0f;

    public float Damage => damage;
    public float ImpactForce => impactForce;

    private void Awake() {
        compRb = GetComponent<Rigidbody>();
    }

    public void SetManager(PoolManager _manager) => manager = _manager;

    public void ReplaceForce(float _value) => force = _value;
    public void ReplaceDamage(float _value) => damage = _value;
    public void ReplaceImpact(float _value) => impactForce = _value;

    public void StartShoot(Transform spawn) {
        compRb.velocity = Vector3.zero;
        transform.rotation = spawn.rotation;

        Vector3 direct = transform.forward * force;
        compRb.AddForce(direct, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("DontDestroyBullet"))
            return;

        if(other.gameObject.CompareTag("PowerUpEnemy"))
            return;

        manager.GiveDestroyBullet(transform.position);
        manager.Pool_BulletTower(this.gameObject);
    }
}