using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour {

    public static ePanelState state = ePanelState.withoutPanel;

    [SerializeField] private GameObject cameraTopDown = null;
    [SerializeField] private GameObject crooshair = null;

    [SerializeField] private GameObject panelPause = null;
    [SerializeField] private GameObject panelOptions = null;
    [SerializeField] private GameObject panelControl = null;
    [SerializeField] private GameObject panelWeapon = null;
    [SerializeField] private GameObject panelTower = null;
    [SerializeField] private GameObject panelDeath = null;

    [SerializeField] private GameObject MoneyImg = null;
    [SerializeField] private Text currentMoney = null;

    public ePanelState StateGame => state;

    private void Start() {
        panelDeath.SetActive(false);
        ShowCursor(false);
        Time.timeScale = 1;
        state = ePanelState.withoutPanel;
    }

    public void PerformDeath() {
        panelDeath.SetActive(true);
        ShowCursor(true);
        Time.timeScale = 0;
    }

    //When Player press Escape
    public void PerformeEscape() {
        switch (state) {
            case ePanelState.withoutPanel:
                ShowCursor(true);
                panelPause.SetActive(true);
                Time.timeScale = 0;
                state = ePanelState.pausePanel;
                break;
            case ePanelState.towerShopPanel:
                ShowCursor(false);
                crooshair.SetActive(true);
                cameraTopDown.SetActive(false);
                MoneyImg.SetActive(false);
                panelTower.SetActive(false);
                state = ePanelState.withoutPanel;
                break;
            case ePanelState.weaponShopPanel:
                ShowCursor(false);
                crooshair.SetActive(true);
                cameraTopDown.SetActive(false);
                MoneyImg.SetActive(false);
                panelWeapon.SetActive(false);
                state = ePanelState.withoutPanel;
                break;
            case ePanelState.pausePanel:
                ShowCursor(false);
                panelPause.SetActive(false);
                Time.timeScale = 1;
                state = ePanelState.withoutPanel;
                break;
            case ePanelState.optionPanel:
                ShowCursor(true);
                panelPause.SetActive(true);
                panelOptions.SetActive(false);
                state = ePanelState.pausePanel;
                break;
            case ePanelState.controlPanel:
                ShowCursor(true);
                panelControl.SetActive(false);
                state = ePanelState.optionPanel;
                break;
        }
    }

    //Verfica si el tiempo de juego esta corriendo
    public bool IsStopGame {
        get {
            bool fin = false;

            bool esc = state == ePanelState.pausePanel;
            bool opt = state == ePanelState.optionPanel;
            bool ctl = state == ePanelState.controlPanel;

            if(esc || opt || ctl)
                fin = true;

            return fin;
        }
    }

    private void IsActiveShop(GameObject _panel, bool _val) {
        ShowCursor(_val);
        MoneyImg.SetActive(_val);
        _panel.SetActive(_val);
    }

    public void PerformeWeaponShop() {
        IsActiveShop(panelTower, false);
        IsActiveShop(panelWeapon, true);
        state = ePanelState.weaponShopPanel;
    }

    public void PerformeTowerShop() {
        IsActiveShop(panelWeapon, false);
        IsActiveShop(panelTower, true);
        state = ePanelState.towerShopPanel;
    }

    //When Player prees key E
    public void PerformCallShop() {
        if(IsStopGame)
            return;

        bool tower = state == ePanelState.towerShopPanel;
        bool weapon = state == ePanelState.weaponShopPanel;

        if(tower || weapon) {
            crooshair.SetActive(true);
            cameraTopDown.SetActive(false);
            IsActiveShop(panelTower, false);
            IsActiveShop(panelWeapon, false);
            state = ePanelState.withoutPanel;
        } else {
            crooshair.SetActive(false);
            cameraTopDown.SetActive(true);
            PerformeTowerShop();
        }
    }

    private void ShowCursor(bool value) {
        if (value) {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        } else {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void PerformeOptions() {
        panelPause.SetActive(false);
        panelOptions.SetActive(true);
        state = ePanelState.optionPanel;
    }

    public void PerformeControl() {
        panelControl.SetActive(true);
        state = ePanelState.controlPanel;
    }

    public void UpdateMoneyText(float amount) {
        //string val = "$ " + amount;
        currentMoney.text = amount.ToString();
    }
}