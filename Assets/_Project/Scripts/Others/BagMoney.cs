using UnityEngine;

public class BagMoney : MonoBehaviour {

    [SerializeField] private PoolManager manager = null;

    private float amountCash = 0f;
    public float AmountCash => amountCash;
    public void SetManager(PoolManager _val) => manager = _val;
    public void SetAmounCash(float amount) => amountCash = amount;
    public void GoPool() => manager.Pool_ManeyBag(this.gameObject);
}