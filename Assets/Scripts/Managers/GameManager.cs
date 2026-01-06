using UnityEngine;

public class GameManager : MonoBehaviour {
    private MenuManager menu;

    private MenuManager menuManager {
        get {
            if(menu != null) return menu;
            GameObject temp = GameObject.Find("MenuManager");
            menu = temp.GetComponent<MenuManager>();

            return menu;
        }
    }

    private static GameManager manager;

    public static GameManager gameManager {
        get {
            if(manager != null) return manager;
            GameObject temp = GameObject.Find("GameManager");
            manager = temp.GetComponent<GameManager>();

            return manager;
        }
    }

    private float lookSensitivity = 10f;
    private float volSound = 1f;
    private float volMusic = 1f;

    private float currentCash = 0;

    public float LookSensitivity => lookSensitivity;
    public float VolSound => volSound;
    public float VolMusic => volMusic;

    public float CurrentCash => currentCash;

    public void CompStart() {
        DontDestroyOnLoad(this.gameObject);
        GameObject temp = GameObject.Find("OptionsManager");
        OptionsManager option = temp.GetComponent<OptionsManager>();
        option.SetLookSensitivity(LookSensitivity);
        option.SetVolSound(VolSound);
        option.SetVolMusic(VolMusic);
    }

    //Llamados desde la interfast por OptionsManager
    public void SetLookSensitivity(float _value) {
        lookSensitivity = _value;
        menuManager.SetLookSensitivity(LookSensitivity);
    }

    public void SetVolSound(float _value) {
        volSound = _value;
        menuManager.SetVolSound(VolSound);
    }

    public void SetvolMusic(float _value) {
        volMusic = _value;
        menuManager.SetVolMusic(VolMusic);
    }
}