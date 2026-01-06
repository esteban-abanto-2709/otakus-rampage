using UnityEngine;

public class EntityStats : MonoBehaviour {

    [SerializeField] protected float speedMove = 10f;
    [SerializeField] protected float healthMax = 10f;
    protected float healthCurrent = 10f;

    private void Awake() {
        Inicializar();
    }

    public virtual void Inicializar() {
        healthCurrent = healthMax;
    }

    public float SpeedMove => speedMove;
    public float MaxHealth => healthMax;
    public float CurrentHealth => healthCurrent;

    public virtual void ModiCurrentHealth(float value) {
        healthCurrent += value;

        if(healthCurrent >= healthMax)
            healthCurrent = healthMax;

        if(healthCurrent <= 0)
            healthCurrent = 0;
    }
}