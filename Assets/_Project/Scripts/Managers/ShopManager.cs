using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour {

    [SerializeField] private GameController controller = null;

    [SerializeField] private Image[] weaponImg = new Image[3];
    [SerializeField] private GameObject[] weaponBlock = new GameObject[3];
    [SerializeField] private float[] weaponCost = new float[3];

    [SerializeField] private Image waterImg = null;
    [SerializeField] private GameObject waterBlock = null;
    [SerializeField] private Text waterCostText = null;
    [SerializeField] private float waterCost = 0f;

    private void Start() {
        UpdateButtons();
    }

    //Metodo llamado por Botones de interfast
    public void TypeWeapon(int val) {
        if(weaponBlock[val].activeSelf)
            return;

        if(weaponCost[val] <= controller.CurrentCash) {
            controller.ModiMoney(weaponCost[val] * -1);
            controller.BuyWeapons(val);
            weaponBlock[val].SetActive(true);
            controller.GameEscape();
        }
    }

    public void BuyWater() {
        if(waterCost <= controller.CurrentCash) {
            controller.BuyWater();
            controller.ModiMoney(waterCost * -1);
            waterBlock.SetActive(true);
        }
    }

    public void UpdateButtons() {
        for(int i = 0;i < weaponImg.Length;i++) {
            if(weaponCost[i] <= controller.CurrentCash) {
                weaponImg[i].color = Color.white;
            } else {
                weaponImg[i].color = Color.gray;
            }
        }

        float val = (300f - WeaponStats.ammoGeneral);
        waterCost = val;
        waterCostText.text = val.ToString();

        waterBlock.SetActive(val == 0);

        if(waterCost <= controller.CurrentCash) {
            waterImg.color = Color.white;
        } else {
            waterImg.color = Color.gray;
        }
    }
}