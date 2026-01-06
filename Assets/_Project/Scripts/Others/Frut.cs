using UnityEngine;

public class Frut : MonoBehaviour {

    [SerializeField] private PoolManager manager = null;

    [SerializeField] private float health = 30f;
    public void SetManager(PoolManager _val) => manager = _val;

    private void Damage(float amount) {
        health -= amount;
        if(health <= 0)
            manager.Pool_OtakuFrut(this.gameObject);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("BulletPlayer")) {
            Bullet temp = other.GetComponent<Bullet>();
            float dmg = temp.Damage;
            Damage(dmg);
        }
    }
}