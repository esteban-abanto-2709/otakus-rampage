using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    private PlayerMotor compMotor;
    private PlayerStats compStats;

    public GameObject panelDamage;

    private bool isGod = false;

    private bool playerIsDead = false;
    public bool PlayerIsDead => playerIsDead;

    private void Awake() {
        compMotor = GetComponent<PlayerMotor>();
        compStats = GetComponent<PlayerStats>();

        compMotor.PosCamera(compStats.PosCamera);
        isGod = false;
        playerIsDead = false;
    }

    public void Move(float _xMov, float _zMov) {
        Vector3 _movHorizontal = transform.right * _xMov;
        Vector3 _movVertical = transform.forward * _zMov;
        Vector3 _velocity = (_movHorizontal + _movVertical).normalized;
        Vector3 final = _velocity * compStats.SpeedMove;
        compMotor.Move(final);
    }

    public void Rotate(float _yRot) {
        float final = _yRot * compStats.LookSensitivity;
        Vector3 _rotation = Vector3.up * final;
        compMotor.Rotate(_rotation);
    }

    public void RotateCamera(float _xRot) {
        float final = _xRot * compStats.LookSensitivity;
        compMotor.RotateCamera(final);
    }

    private void OnTriggerEnter(Collider other) {
        if(playerIsDead)
            return;

        if(other.gameObject.CompareTag("MoneyBag")) {
            BagMoney temp = other.GetComponent<BagMoney>();

            if(temp != null) {
                float val = temp.AmountCash;
                compStats.ModiCurrentCash(val);
                temp.GoPool();
            }
        }

        if(other.gameObject.CompareTag("DamagePlayer")) {
            if(isGod)
                return;

            isGod = true;
            panelDamage.SetActive(true);
            StartCoroutine(TimerGod());

            float damage = 0f;

            AttackEnemy scrTemp = other.GetComponent<AttackEnemy>();
            if(scrTemp)
                damage = scrTemp.Damage;

            compStats.ModiCurrentHealth(damage * -1);

            compStats.FeedBackHealth();

            if(compStats.CurrentHealth <= 0)
                playerIsDead = true;
        }
    }

    IEnumerator TimerGod() {
        yield return new WaitForSeconds(compStats.TimeGod);
        isGod = false;
        panelDamage.SetActive(false);
    }

    private void OnTriggerStay(Collider other) {
        if(playerIsDead)
            return;

        if(other.gameObject.CompareTag("Shower")) {
            Shower temp = other.GetComponent<Shower>();
            if(temp != null) {
                if(temp.IsActive) {
                    float val = Time.deltaTime * (compStats.MaxHealth / temp.TimeAction);
                    compStats.ModiCurrentHealth(val);
                    compStats.FeedBackHealth();
                }
            }
        }
    }

    //Test
    public void MaxHealth() {
        compStats.Inicializar();
        compStats.FeedBackHealth();
    }
}