using UnityEngine;

public class Bubbles : Attack {

    [SerializeField] private Transform particules = null;
    private Transform currentParticles {
        get {
            int i = 0;
            Transform temp = null;
            foreach(Transform part in particules) {
                if(i == (int)level) {
                    part.gameObject.SetActive(true);
                    temp = part;
                } else
                    part.gameObject.SetActive(false);
                i++;
            }

            return temp;
        }
    }

    protected override void Attack() {
        ActiveParticules(currentParticles);
        for(int i = 0; i < enemy.Count; i++) {
            EnemyController temp = enemy[i];
            temp.ModiHealth(currentDamage * -1);
        } 
    }

    private void ActiveParticules(Transform pos) {
        foreach(Transform particle in pos) {
            particle.GetComponent<ParticleSystem>().Play();
        }
    }
}