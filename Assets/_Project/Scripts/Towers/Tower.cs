using UnityEngine;

public class Tower : MonoBehaviour {

    [SerializeField] protected TowerManager manager = null;

    [SerializeField] protected eLevelTower level = eLevelTower.nivel_0;
    [SerializeField] private Transform model = null;

    [SerializeField] private GameObject rangeBlock = null;
    [SerializeField] private GameObject rangeAction = null;

    [SerializeField] private string[] textTower = new string[3];
    public string TextTower => textTower[(int)level];

    [SerializeField] private int[] coolDown = new int[3];
    protected int currentCoolDown => coolDown[(int)level];

    [SerializeField] private int saleMoney = 0;
    public int SaleMoney => saleMoney;

    [SerializeField] private int costUpLevel = 0;
    public int CostUpLevel => costUpLevel;

    protected bool isActive = false;

    protected Transform PosPlayer => manager.PosPlayer;

    private void Start() {
        //Borrar metodo
        UpdateLevel();
    }

    public void SetManager(TowerManager _value) => manager = _value;

    protected virtual void UpdateLevel() {
        int i = 0;
        foreach (Transform view in model) {
            if (i == (int)level) {
                view.gameObject.SetActive(true);
            } else
                view.gameObject.SetActive(false);
            i++;
        }
    }

    public void SetActiveBlock(bool value) {
        rangeBlock.SetActive(value);
    }

    public void SetActiveAction(bool value) {
        rangeAction.SetActive(value);
    }

    public eLevelTower CurrentLevel => level;

    public void ChangeLevel(eLevelTower value) {
        level = value;
        UpdateLevel();
    }

    public void DestroyGameObj() {
        Destroy(this.gameObject);
    }
}