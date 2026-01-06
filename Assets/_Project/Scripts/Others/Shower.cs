using System.Collections;
using UnityEngine;

public class Shower : MonoBehaviour {

    private Animator compAni = null;

    [SerializeField] private ParticleSystem[] particles = new ParticleSystem[2];

    [SerializeField] private float timeAction = 30f;
    [SerializeField] private float timeCooldown = 180f;

    private bool isActive = false;
    private bool canActive = true;

    public bool IsActive => isActive;
    public float TimeAction => timeAction;

    private void Awake() {
        compAni = GetComponent<Animator>();
    }

    private IEnumerator CoolDown() {
        yield return new WaitForSeconds(timeCooldown);
        canActive = true;
        compAni.SetBool("isBlock", false);
    }

    private IEnumerator ShowerActive() {
        yield return new WaitForSeconds(timeAction);
        isActive = false;
        StartCoolDown();
    }

    private void StartCoolDown() {
        compAni.SetBool("isBlock", true);
        StartCoroutine(CoolDown());
    }

    private void StartActive() {
        canActive = false;
        isActive = true;
        particles[0].Play();
        particles[1].Play();
        StartCoroutine(ShowerActive());
    }

    private void OnTriggerEnter(Collider other) {
        if(!canActive)
            return;

        if(other.gameObject.CompareTag("Player")) {
            StartActive();
        }
    }
}