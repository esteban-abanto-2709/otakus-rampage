using UnityEngine;

public class PlayerStats : EntityStats {

    [SerializeField] private GameController controller = null;

    [SerializeField] private Transform posCamera = null;
    [SerializeField] private Transform[] posHeathBar = new Transform[4];
    [SerializeField] private float lookSensitivity = 5f;
    [SerializeField] private float currentCash = 0f;

    [SerializeField] private float timeGod = 1f;
    public float TimeGod => timeGod;

    public Transform PosCamera => posCamera;

    public float CurrentCash => currentCash;

    public void SetCurrentCash(float value) {
        currentCash = value;

        controller.UpdateMoney(currentCash);
        controller.UpdateButtons();
    }

    public void ModiCurrentCash(float value) {
        currentCash += value;

        if (currentCash <= 0)
            currentCash = 0;

        controller.UpdateMoney(currentCash);
        controller.UpdateButtons();
    }

    public void SetLookSensitivity(float val) => lookSensitivity = val;
    public float LookSensitivity => lookSensitivity;

    public void FeedBackHealth() {
        float temp = healthCurrent / healthMax;
        foreach (Transform bar in posHeathBar) {
            bar.localScale = new Vector3(1f, temp, 1f);
        }
    }
}