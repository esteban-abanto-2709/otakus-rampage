using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    [SerializeField] private GameController controller = null;
    [SerializeField] private MenuManager menu = null;

    [SerializeField] private eLevelFase level = eLevelFase.nivel_0;

    [SerializeField] private string nextLevel = " ";

    [SerializeField] private float preparationTime = 30f;
    [SerializeField] private float attackTime = 180f;
    [SerializeField] private float breakTime = 90f;

    [Header("FeedBack Level")]
    [SerializeField] private Shader disolv = null;
    [SerializeField] private Color waterColor = Color.blue;

    private Material water_FB = null;
    [SerializeField] private MeshRenderer render = null;
    [SerializeField] private float max_FB = 10f;
    [SerializeField] private float min_FB = -10f;

    private float currentWater_FB = 1f;

    private float totalTime = 0f;
    private float currentTime = 0f;

    private void Awake() {
        totalTime = (attackTime * 3) + (breakTime * 2) + preparationTime;
        currentTime = 0f;

        water_FB = new Material(disolv);
        water_FB.SetColor("_color", waterColor);
        render.material = water_FB;
    }

    private void toUpdateWater_FB() {
        currentWater_FB = currentTime / totalTime;
        float val = Mathf.Lerp(min_FB, max_FB, currentWater_FB);
        water_FB.SetFloat("_degrade", val);
    }

    private void Start() {
        Invoke("CompStart", preparationTime);
    }

    private void Update() {
        currentTime += Time.deltaTime;
        toUpdateWater_FB();
    }

    private void CompStart() {
        level = eLevelFase.nivel_0;
        CompHorda();
    }

    private void CompHorda() {
        StartCoroutine(Horda());
    }

    private void CompBreak() {
        switch(level) {
            case eLevelFase.nivel_0:
                level = eLevelFase.nivel_1;
                StartCoroutine(Break());
                break;
            case eLevelFase.nivel_1:
                level = eLevelFase.nivel_2;
                StartCoroutine(Break());
                break;
            case eLevelFase.nivel_2:
                menu.ChangeScene(nextLevel);
                break;
        }
    }

    IEnumerator Horda() {
        controller.SetLevelEnemyManager(level);
        controller.SetSpawnEnemy(true);
        yield return new WaitForSeconds(attackTime);
        controller.SetSpawnEnemy(false);
        CompBreak();
    }

    IEnumerator Break() {
        yield return new WaitForSeconds(breakTime);
        CompHorda();
    }
}