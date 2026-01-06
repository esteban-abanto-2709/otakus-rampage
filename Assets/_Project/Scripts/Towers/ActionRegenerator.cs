using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionRegenerator : MonoBehaviour {

    [SerializeField] private Regenerator brain = null;

    private void OnTriggerStay(Collider other) {
        if(other.gameObject.CompareTag("PhysicTower")) {
            float num = brain.tower.Count;
            Transform posTemp = other.transform.parent;
            ForEnemy temp = posTemp.GetComponent<ForEnemy>();

            if(temp == null || brain.InList(temp))
                return;

            brain.tower.Add(temp);

            if(num != brain.tower.Count && !brain.IsActive)
                brain.EmitionWater();
        }
    }
}