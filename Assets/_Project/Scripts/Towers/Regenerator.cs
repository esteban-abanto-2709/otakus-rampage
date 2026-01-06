using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regenerator : Tower {

    [SerializeField] private int[] water = new int[3];
    private int waterGiven => water[(int)level];

    public List<ForEnemy> tower = new List<ForEnemy>();

    public bool InList(ForEnemy element) {
        bool state = false;
        foreach (ForEnemy control in tower) {
            if (element == control)
                state = true;
        }
        return state;
    }

    public bool IsActive => isActive;

    public void EmitionWater() {
        isActive = true;
        RemoveTowerOutRange();

        if (tower.Count != 0)
            StartCoroutine(GiveWater());
        else
            isActive = false;
    }

    private void RemoveTowerOutRange() {
        for (int i = 0; i < tower.Count; i++) {
            ForEnemy temp = tower[i];
            if (temp == null) {
                tower.Remove(temp);
                i--;
            } else {
                float dist = Vector3.Distance(transform.position, temp.transform.position);
                if (dist >= 15f) {
                    tower.Remove(temp);
                    i--;
                }
            }
        }
    }

    private void ShotWater() {
        for (int i = 0; i < tower.Count; i++) {
            ForEnemy temp = tower[i];
            temp.GetWater(waterGiven);
        }
    }

    private IEnumerator GiveWater() {
        ShotWater();
        yield return new WaitForSeconds(currentCoolDown);
        EmitionWater();
    }
}