using UnityEngine;

public class GameController : MonoBehaviour {

    [SerializeField] private PlayerController scrController = null;
    [SerializeField] private PlayerStats scrStats = null;
    [SerializeField] private WeaponManager scrWeapon = null;
    [SerializeField] private PoolManager scrPool = null;
    [SerializeField] private PanelManager scrPanel = null;
    [SerializeField] private ShopManager scrShop = null;
    [SerializeField] private EnemyManager scrEnemy = null;

    public float CurrentCash => scrStats.CurrentCash;
    public bool IsStopGame => scrPanel.IsStopGame;
    public Transform PosPlayer => scrController.transform;

    public void PlayerMove(float _AxisHor, float _AxisVer) {
        scrController.Move(_AxisHor, _AxisVer);
    }

    public void PlayerRotate(float _AxisHor, float _AxisVer) {
        scrController.Rotate(_AxisHor);
        scrController.RotateCamera(_AxisVer);
    }

    public void WeaponClick() {
        scrWeapon.Shoot();
    }

    public void WeaponReload() {
        scrWeapon.Reload();
    }

    public void WeaponScroll(float _value) {
        scrWeapon.ChangeWeapon(_value);
    }

    public void GameEscape() {
        scrPanel.PerformeEscape();
    }

    public void OpenPanelShop() {
        scrPanel.PerformCallShop();
    }

    //Set Options Panel

    public void SetLookSensitivity(float _value) {
        scrStats.SetLookSensitivity(_value);
    }

    public void SetVolSound(float _value) {
        //Debug.Log("Valor ingresado!! Sound");
    }

    public void SetVolMusic(float _value) {
        //Debug.Log("Valor ingresado!! Music");
    }

    //Other Conections
    public bool PlayerIsDead => scrController.PlayerIsDead;

    public void SetSpawnEnemy(bool _val) {
        scrEnemy.SetSpawnEnemy(_val);
    }

    public void SetLevelEnemyManager(eLevelFase _val) {
        scrEnemy.SetLevel(_val);
    }

    public void ModiMoney(float value) {
        scrStats.ModiCurrentCash(value);
    }

    public void UpdateMoney(float val) {
        scrPanel.UpdateMoneyText(val);
    }

    public void BuyWeapons(int _val) {
        scrWeapon.BuyWeapon(_val);
    }

    public void BuyWater() {
        scrWeapon.BuyWater();
    }

    public void UpdateButtons() {
        scrShop.UpdateButtons();
    }

    //Pools

    public void GiveMoneyBag(Transform _pos, float money) {
        scrPool.GiveMoneyBag(_pos, money);
    }

    public void GiveOtakuFrut(Vector3 _pos, Quaternion _rot) {
        scrPool.GiveOtakuFrut(_pos, _rot);
    }

    public void GiveBulletTower(Transform _pos, float _force, float _damage, float _impact) {
        scrPool.GiveBulletTower(_pos, _force, _damage, _impact);
    }

    public void GiveDestroyBullet(Vector3 _pos) {
        scrPool.GiveDestroyBullet(_pos);
    }

    //Conecciones para Testing
    public void SpawnEnemy() {
        scrEnemy.SpawnEnemy();
    }

    public void SpawnBoss() {
        scrEnemy.SpawnBoss();
    }

    public void MaxHealth() {
        scrController.MaxHealth();
    }

    public void PerformDeath() {
        scrPanel.PerformDeath();
    }
}