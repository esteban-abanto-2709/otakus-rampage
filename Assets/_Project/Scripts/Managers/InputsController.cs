using UnityEngine;

public class InputsController : MonoBehaviour {

    [SerializeField] private GameController scrGame = null;

    private void Update() {
        if(scrGame.PlayerIsDead) {
            scrGame.PerformDeath();
            return;
        }

        //Inputs del Juego
        Escape();
        PanelShop();

        if(PanelManager.state != ePanelState.withoutPanel)
            return;

        //Inputs del Player
        PlayerMove();
        PlayerRotate();

        //Inputs de las Armas
        Click();
        Reload();
        MouseScroll();

        KeyFast();
    }

    private void KeyFast() {
        if(Input.GetKeyDown(KeyCode.M)) {
            scrGame.ModiMoney(5000);
        }

        if(Input.GetKeyDown(KeyCode.H)) {
            scrGame.SpawnEnemy();
        }

        if(Input.GetKeyDown(KeyCode.H)) {
            scrGame.MaxHealth();
        }

        if(Input.GetKeyDown(KeyCode.B)) {
            scrGame.SpawnBoss();
        }
    }

    private void PlayerMove() {
        float _horAxis = Input.GetAxisRaw("Horizontal");
        float _verAxis = Input.GetAxisRaw("Vertical");

        scrGame.PlayerMove(_horAxis, _verAxis);
    }

    private void PlayerRotate() {
        float _horAxis = Input.GetAxisRaw("Mouse X");
        float _verAxis = Input.GetAxisRaw("Mouse Y");

        scrGame.PlayerRotate(_horAxis, _verAxis);
    }

    private void Click() {
        bool value = Input.GetMouseButton(0);

        if(value)
            scrGame.WeaponClick();
    }

    private void Reload() {
        bool value = Input.GetKeyDown(KeyCode.R);

        if(value)
            scrGame.WeaponReload();
    }

    private void MouseScroll() {
        float value = Input.GetAxisRaw("Mouse ScrollWheel");
        scrGame.WeaponScroll(value);
    }

    private void Escape() {
        bool value = Input.GetKeyDown(KeyCode.Escape);

        if(value)
            scrGame.GameEscape();
    }

    private void PanelShop() {
        bool value = Input.GetKeyDown(KeyCode.E);

        if(value)
            scrGame.OpenPanelShop();
    }
}