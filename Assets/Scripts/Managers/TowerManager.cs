using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerManager : MonoBehaviour {

    private const int RAY_LENGTH = 55;

    [SerializeField] private eStateTowerShop mouseState = eStateTowerShop.noTower;
    private int numerState => (int)mouseState;

    [SerializeField] private GameController controller = null;
    public Transform PosPlayer => controller.PosPlayer;

    [SerializeField] private Camera cam = null;

    /*
    [Header("Camera 2D Controller")]
    private float currentZoom = 50f;
    private Vector3 posMouseClick = Vector3.zero;
    private Vector3 posCameraClick = Vector3.zero;
    [SerializeField] private float zoomMax = 50f;
    [SerializeField] private float zoomMin = 10f;
    [SerializeField] private float speedZoom = 10f;
    */

    [Header("General Towers")]
    [SerializeField] private TowerFeedBack[] feedbackTower = new TowerFeedBack[4];
    [SerializeField] private GameObject[] typeTower = new GameObject[4];
    [SerializeField] private float[] towerCost = new float[4];
    [SerializeField] private List<Tower> tower = new List<Tower>();

    [Header("Interaction Specific Towers")]
    [SerializeField] private RectTransform infoTower = null;
    [SerializeField] private RectTransform dontMoney = null;
    [SerializeField] private RectTransform changeTower = null;
    [SerializeField] private RectTransform specificTower = null;

    [Header("Buttons")]
    [SerializeField] private RectTransform saleTower = null;
    [SerializeField] private RectTransform levelUpTower = null;

    [Header("LayerMask")]
    [SerializeField] private LayerMask stateTower = 0;
    [SerializeField] private LayerMask stateNotTower = 0;
    [SerializeField] private LayerMask onlyMap = 0;

    [SerializeField] private float radioSphereCast = 1.5f;

    private Vector3 posMouse = Vector3.zero;

    //Variables dinamicas
    private Tower towerUnderMouse = null;
    private bool isChangeTower = false;

    private void Update() {
        if (PanelManager.state != ePanelState.towerShopPanel) {
            if (towerUnderMouse != null)
                towerUnderMouse.SetActiveAction(false);
            if (numerState != 4)
                feedbackTower[numerState].transform.position = Vector3.right * 1000;

            SetActiveBlock(false);

            mouseState = eStateTowerShop.noTower;
            return;
        }

        RaycastHit hit;
        Vector3 mouseWorldPoint = cam.ScreenToWorldPoint(Input.mousePosition);

        bool notTower = mouseState != eStateTowerShop.noTower;

        SetActiveBlock(notTower);

        if (notTower) {
            //Estado de construccion de torres
            RaycastHit rayHit;
            bool sphereHit = Physics.SphereCast(mouseWorldPoint, radioSphereCast, Vector3.down, out hit, RAY_LENGTH, stateTower);
            bool rayCastHit = Physics.Raycast(mouseWorldPoint, Vector3.down, out rayHit, RAY_LENGTH, onlyMap);

            if (rayCastHit) {
                feedbackTower[numerState].transform.position = rayHit.point;
                feedbackTower[numerState].IsPosRight(!sphereHit);

                if (towerCost[numerState] > controller.CurrentCash) {
                    dontMoney.gameObject.SetActive(true);
                    Vector3 offset = new Vector3(0, dontMoney.sizeDelta.y / 2);
                    dontMoney.position = Input.mousePosition + offset;
                }

                if (Input.GetMouseButtonDown(0)) {
                    if (!sphereHit && towerCost[numerState] <= controller.CurrentCash) {
                        CreateTower(rayHit.point, numerState);
                        controller.ModiMoney(towerCost[numerState] * -1);
                    }

                    feedbackTower[numerState].transform.position = Vector3.right * 1000;
                    mouseState = eStateTowerShop.noTower;
                }

            } else {
                feedbackTower[numerState].transform.position = Vector3.right * 1000;
                dontMoney.gameObject.SetActive(false);
                if (Input.GetMouseButtonDown(0)) {
                    mouseState = eStateTowerShop.noTower;
                }
            }
        } else {
            //Estado normal 
            dontMoney.gameObject.SetActive(false);
            Physics.Raycast(mouseWorldPoint, Vector3.down, out hit, RAY_LENGTH, stateNotTower);

            if (!isChangeTower) {
                changeTower.gameObject.SetActive(false);
            } else {
                if (MouseOnButton(saleTower)) {
                    string text = "+ " + towerUnderMouse.SaleMoney;
                    ShowText(true, text);
                } else if (MouseOnButton(levelUpTower)) {
                    string text = "- " + towerUnderMouse.CostUpLevel;

                    if (towerUnderMouse.CurrentLevel == eLevelTower.nivel_2)
                        text = "MAX";

                    ShowText(true, text);
                } else {
                    infoTower.gameObject.SetActive(false);
                }
            }

            if (hit.transform != null && !isChangeTower) {
                Transform pos = hit.transform.parent;
                towerUnderMouse = pos.GetComponent<Tower>();
                towerUnderMouse.SetActiveAction(true);
                string texto = towerUnderMouse.TextTower;
                ShowText(true, texto);
                posMouse = Input.mousePosition;

                if (Input.GetMouseButtonDown(0)) {
                    isChangeTower = true;
                    changeTower.gameObject.SetActive(true);
                    float height = specificTower.sizeDelta.y - 16;
                    float width = specificTower.sizeDelta.x - 140;
                    Vector3 vecY = Vector3.up * (height / 2);
                    Vector3 vecX = Vector3.right * (width / 2);
                    Vector3 offset = vecX + vecY;
                    specificTower.position = posMouse + offset;
                }

            } else {
                if (!isChangeTower) {
                    if (towerUnderMouse != null) {
                        towerUnderMouse.SetActiveAction(false);
                    }
                    infoTower.gameObject.SetActive(false);
                }
            }
        }
    }

    //Llamado desde boton 
    public void ButtonExitChangeTower() {
        isChangeTower = false;
    }

    //Llamado desde interfast
    public void ButtonSellTower() {
        tower.Remove(towerUnderMouse);
        towerUnderMouse.DestroyGameObj();
        int saleMoney = towerUnderMouse.SaleMoney;
        controller.ModiMoney(saleMoney);
        towerUnderMouse = null;
        isChangeTower = false;
    }

    //Llamado desde interfast
    public void ButtonLevelUpTower() {
        if (towerUnderMouse.CurrentLevel == eLevelTower.nivel_2)
            return;

        if (controller.CurrentCash < towerUnderMouse.CostUpLevel)
            return;

        int costUpLevel = towerUnderMouse.CostUpLevel * -1;

        switch (towerUnderMouse.CurrentLevel) {
            case eLevelTower.nivel_0:
                towerUnderMouse.ChangeLevel(eLevelTower.nivel_1);
                break;

            case eLevelTower.nivel_1:
                towerUnderMouse.ChangeLevel(eLevelTower.nivel_2);
                break;
        }

        controller.ModiMoney(costUpLevel);
        towerUnderMouse.SetActiveAction(false);
        towerUnderMouse = null;
        isChangeTower = false;
    }

    private bool MouseOnButton(RectTransform button) {
        Vector3 _posMouse = Input.mousePosition;

        float posButtonX = button.position.x;
        float posButtonY = button.position.y;

        float button_B = button.sizeDelta.x / 2f;
        float button_H = button.sizeDelta.y / 2f;

        float sr_X = posButtonX + button_B;
        float sr_Y = posButtonY + button_H;
        Vector3 subRigth = new Vector3(sr_X, sr_Y, 0);

        float bl_X = posButtonX - button_B;
        float bl_Y = posButtonY - button_H;
        Vector3 botLeft = new Vector3(bl_X, bl_Y, 0);

        bool M_X1 = _posMouse.x >= botLeft.x;
        bool M_Y1 = _posMouse.y >= botLeft.y;
        bool M_X2 = _posMouse.x < subRigth.x;
        bool M_Y2 = _posMouse.y < subRigth.y;

        bool onButton = M_X1 && M_Y1 && M_X2 && M_Y2;

        return onButton;
    }

    private void ShowText(bool isActive, string text) {
        infoTower.gameObject.SetActive(isActive);
        Vector3 offset = new Vector3(0, (infoTower.sizeDelta.y / 2) + 3);
        infoTower.position = Input.mousePosition + offset;
        Text texto = infoTower.GetChild(0).gameObject.GetComponent<Text>();
        texto.text = text;
    }

    //NO Usable
    /*
    private void LogicZoomCamera(Vector3 posMouse) {

        RaycastHit hitPoint;
        Physics.Raycast(posMouse, Vector3.down, out hitPoint, RAY_LENGTH, onlyMap);

        //Logic Scroll
        float mouseScroll = Input.GetAxisRaw("Mouse ScrollWheel") * -1f;
        currentZoom += mouseScroll * speedZoom;

        if(currentZoom >= zoomMax)
            currentZoom = zoomMax;

        if(currentZoom <= zoomMin)
            currentZoom = zoomMin;

        cam.orthographicSize = currentZoom;

        //Logic MoveCamera

        if(Input.GetMouseButtonDown(2)) {
            posMouseClick = hitPoint.point;
            Debug.Log(posMouseClick);
            posCameraClick = cam.transform.position;
        }

        if(Input.GetMouseButton(2)) {
            //Toma la posicion del mouse respecto a la camara (se caga cuando la camara varia)
            Vector3 mouseWorldPoint = hitPoint.point;
            Debug.Log(mouseWorldPoint);
            Vector3 refMouse = mouseWorldPoint - posMouseClick;
            Vector3 refCamera = posCameraClick - refMouse;
            cam.transform.position = refCamera;
        }
    }
    */

    //Llamado desde GameController
    public void ButtonGiveTower(int num) {
        mouseState = (eStateTowerShop)num;
    }

    private void SetActiveBlock(bool _value) {
        foreach (Tower scr in tower) {
            scr.SetActiveBlock(_value);
        }
    }

    public void CreateTower(Vector3 pos, int i) {
        GameObject objTemp = Instantiate(typeTower[i]);
        objTemp.transform.position = pos;
        Tower scrTemp = objTemp.GetComponent<Tower>();
        scrTemp.SetManager(this);
        tower.Add(scrTemp);
    }

    //Metodos de Pooling
    public void GiveBulletTower(Transform _pos, float _force, float _damage, float _impact) {
        controller.GiveBulletTower(_pos, _force, _damage, _impact);
    }

    public void GiveDestroyBullet(Vector3 _pos) {
        controller.GiveDestroyBullet(_pos);
    }
}