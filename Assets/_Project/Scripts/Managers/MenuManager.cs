using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuManager : MonoBehaviour {

    [SerializeField] private GameController controller = null;

    private GameManager manager => GameManager.gameManager;

    private void Awake() {
        manager.CompStart();

        if(controller == null)
            return;

        controller.SetLookSensitivity(manager.LookSensitivity);
        controller.SetVolSound(manager.VolSound);
        controller.SetVolMusic(manager.VolMusic);
    }

    private void Start() {
        Time.timeScale = 1;
    }

    //Set de Valores
    public void SetLookSensitivity(float _value) {

        if(controller == null)
            return;

        controller.SetLookSensitivity(_value);
    }

    public void SetVolSound(float _value) {

        if(controller == null)
            return;

        controller.SetVolSound(_value);
    }

    public void SetVolMusic(float _value) {

        if(controller == null)
            return;

        controller.SetVolMusic(_value);
    }

    //Logica de Cambio de Escena

    public void MainMenu() {
        SceneManager.LoadScene("Menu");
    }

    public void Level_Tutorial() {
        SceneManager.LoadScene("Tutorial");
    }

    public void Level_1() {
        SceneManager.LoadScene("Nivel_1");
    }

    public void Level_2() {
        SceneManager.LoadScene("Nivel_2");
    }

    public void Level_Boss() {
        SceneManager.LoadScene("Nivel_Boss");
    }

    public void ChangeScene(string escene) {
        SceneManager.LoadScene(escene);
    }

    public void Exit() {
        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}