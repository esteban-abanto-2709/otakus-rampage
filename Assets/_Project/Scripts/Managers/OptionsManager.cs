using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsManager : MonoBehaviour {

    private GameManager manager => GameManager.gameManager;

    [SerializeField] private AudioMixer music = null;
    [SerializeField] private AudioMixer sound = null;

    [SerializeField] private Slider lookSensitivity = null;
    [SerializeField] private Slider volSound = null;
    [SerializeField] private Slider volMusic = null;

    private const string SOUND = "volSound";
    private const string MUSIC = "volMusic";

    //Metodos llamados al iniciar la escena
    //Acomoda las barras
    public void SetLookSensitivity(float val) {
        lookSensitivity.value = val;
    }

    public void SetVolSound(float val) {
        volSound.value = val;
    }

    public void SetVolMusic(float val) {
        volMusic.value = val;
    }

    //Metodos llamados desde interfast
    public void LookSensitivity(float value) {
        manager.SetLookSensitivity(value);
    }

    public void VolumenSound(float value) {
        sound.SetFloat(SOUND, value);
        manager.SetVolSound(value);
    }

    public void VolumenMusic(float value) {
        music.SetFloat(MUSIC, value);
        manager.SetvolMusic(value);
    }
}