using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForEnemy : Tower {

    [SerializeField] private int[] maxWater = new int[3];
    protected float MaxWater => maxWater[(int)level];

    [SerializeField] protected float currentWater;
    private int lessWater = 1;

    protected List<EnemyController> enemy = new List<EnemyController>();

    [SerializeField] private Shader disolv = null;
    [SerializeField] private Color waterColor = Color.blue;

    private Material water_FB = null;
    [SerializeField] private MeshRenderer[] render = new MeshRenderer[0];
    [SerializeField] private float max_FB = 10f;
    [SerializeField] private float min_FB = -10f;

    private float currentWater_FB = 1f;

    //Quitar metodo
    private void Awake() {
        water_FB = new Material(disolv);
        water_FB.SetColor("_color", waterColor);
        foreach (MeshRenderer mr in render) {
            mr.material = water_FB;
        }

        ResetWater();
    }

    private void toUpdateWater_FB() {
        currentWater_FB = currentWater / MaxWater;
        float val = Mathf.Lerp(min_FB, max_FB, currentWater_FB);
        water_FB.SetFloat("_degrade", val);
    }

    public void ResetWater() {
        currentWater = MaxWater;

        toUpdateWater_FB();
    }

    public void GetWater(int _amount) {
        currentWater += _amount;

        if (currentWater >= MaxWater)
            ResetWater();

        toUpdateWater_FB();
    }

    private void LostWater() {
        currentWater -= lessWater;

        if (currentWater <= 0)
            currentWater = 0;

        toUpdateWater_FB();
    }

    private void RemoveEnemyDeath() {
        for (int i = 0; i < enemy.Count; i++) {
            EnemyController temp = enemy[i];
            if (temp.HealthRead == 0) {
                enemy.Remove(temp);
                i--;
            }
        }
    }

    private bool InList(EnemyController element) {
        bool state = false;
        foreach (EnemyController control in enemy) {
            if (element == control)
                state = true;
        }
        return state;
    }

    protected override void UpdateLevel() {
        base.UpdateLevel();
        ResetWater();
    }

    protected void ResetLostWater() {
        isActive = true;
        RemoveEnemyDeath();

        if (enemy.Count != 0 && currentWater >= lessWater) {
            StartCoroutine(LostWaterTime());
        } else {
            isActive = false;
            LackOfAmmunition();
        }
    }

    protected virtual void LackOfAmmunition() {

    }

    protected virtual void Attack() {

    }

    protected virtual void ActionWhenAddList(EnemyController temp) {

    }

    IEnumerator LostWaterTime() {
        LostWater();
        Attack();
        yield return new WaitForSeconds(currentCoolDown);
        ResetLostWater();
    }

    private void OnTriggerStay(Collider other) {
        if (other.gameObject.CompareTag("Enemy")) {
            float num = enemy.Count;
            EnemyController temp = other.GetComponent<EnemyController>();
            if (temp == null || InList(temp))
                return;

            if (currentWater >= lessWater) {
                enemy.Add(temp);
                ActionWhenAddList(temp);
            }

            if (enemy.Count != num && !isActive) {
                ResetLostWater();
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Enemy")) {
            EnemyController temp = other.GetComponent<EnemyController>();
            if (temp != null)
                enemy.Remove(temp);
        }
    }
}