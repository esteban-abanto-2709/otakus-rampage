using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour {

    private WeaponController scrWeapon = null;

    private Rigidbody compRb = null;
    private float force = 0f;
    private float damage = 0f;
    private float impactForce = 0f;

    private void Awake() {
        compRb = GetComponent<Rigidbody>();
    }

    public void AddController(WeaponController _value) => scrWeapon = _value;

    public void ReplaceForce(float _value) => force = _value;
    public void ReplaceDamage(float _value) => damage = _value;
    public void ReplaceImpact(float _value) => impactForce = _value;

    public float Damage => damage;
    public float ImpactForce => impactForce;

    public void StartShoot(Vector3 _posValue) {
        compRb.velocity = Vector3.zero;
        Vector3 target = _posValue;
        if(target != Vector3.zero)
            transform.LookAt(target);

        Vector3 direct = transform.forward * force;
        compRb.AddForce(direct, ForceMode.Impulse);
    }

    public void Pooling() => scrWeapon.Pool(this.gameObject);

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("PhysicTower")) {
            Transform posTemp = other.transform.parent;
            ForEnemy temp = posTemp.GetComponent<ForEnemy>();
            if(temp == null)
                return;

            float val = damage / 10f;
            temp.GetWater((int)val);
        }

        scrWeapon.GiveDestroyBullet(transform.position);
        Pooling();
    }
}