using UnityEngine;

public class WeaponManager : MonoBehaviour{

    [SerializeField] private GameController gameController = null;

    [SerializeField] private WeaponController scrController = null;
    [SerializeField] private Transform posShow = null;
    [SerializeField] private Transform[] barGeneralFB = new Transform[4];

    [SerializeField] private Transform[] weapon = new Transform[3];

    private int selectedWeapon = 0;

    private void Awake() {
        SelectWeapon();
    }

    public void UpdateButtons() {
        gameController.UpdateButtons();
    }

    public void FeedBackAmmo() {
        float temp = WeaponStats.ammoGeneral / 300f;
        foreach(Transform bar in barGeneralFB) {
            bar.localScale = new Vector3(1f, temp, 1f);
        }
    }

    public void BuyWater() {
        float temp = 300f - WeaponStats.ammoGeneral;
        WeaponStats.ammoGeneral += temp;
        FeedBackAmmo();
    }

    public void BuyWeapon(int numer) {
        weapon[numer].parent = posShow;
        weapon[numer].gameObject.SetActive(true);
        selectedWeapon = posShow.childCount - 1;
        SelectWeapon();
    }

    public void ChangeWeapon(float _value) {
        
        int previousSelectedWeapon = selectedWeapon;

        if(_value > 0f) {
            if(selectedWeapon >= posShow.childCount - 1)
                selectedWeapon = 0;
            else
                selectedWeapon++;
        }

        if(_value < 0f) {
            if(selectedWeapon <= 0)
                selectedWeapon = posShow.childCount - 1;
            else
                selectedWeapon--;
        }

        if(previousSelectedWeapon != selectedWeapon)
            SelectWeapon();
    }

    private void SelectWeapon() {
        int i = 0;
        foreach(Transform weapon in posShow) {
            if(i == selectedWeapon) {
                weapon.gameObject.SetActive(true);
                scrController = weapon.gameObject.GetComponent<WeaponController>();
                scrController.SetManager(this);
            } else
                weapon.gameObject.SetActive(false);
            i++;
        }
    }

    public void Shoot() {
        RaycastHit hit;
        Physics.Raycast(transform.position, transform.forward, out hit);
        scrController.Shoot(hit.point);
    }

    public void Reload() {
        scrController.StartReload();
    }

    public void GiveDestroyBullet(Vector3 _pos) {
        gameController.GiveDestroyBullet(_pos);
    }
}