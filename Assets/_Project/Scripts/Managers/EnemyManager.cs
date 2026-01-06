using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    [SerializeField] private eLevelFase level = eLevelFase.nivel_0;
    private int numLevel => (int)level;

    [Header("Puntos de Spawn")]
    [SerializeField] private Transform[] pos_level_1 = new Transform[0];
    [SerializeField] private Transform[] pos_level_2 = new Transform[0];
    [SerializeField] private Transform[] pos_level_3 = new Transform[0];

    private bool isFaseAttack = false;
    [SerializeField] private float cooldownEnemy = 3f;
    public void SetCooldownEnemy(float _val) => cooldownEnemy = _val;

    [SerializeField] private GameController game = null;
    public bool IsStopGame => game.IsStopGame;

    [Header("Type Enemies")]
    [SerializeField] private GameObject objEnemy_I = null;
    [SerializeField] private GameObject objEnemy_Boss = null;

    [Header("Main Pos")]
    [SerializeField] private Transform posRocket = null;
    [SerializeField] private Transform posPlayer = null;

    private List<GameObject> inGame = new List<GameObject>();

    private List<GameObject> pool = new List<GameObject>();
    private List<GameObject> poolBoss = new List<GameObject>();

    //Usar en Misil
    public void SetLevel(eLevelFase _value) {
        level = _value;
    }

    //Usar en Misil
    public void SetSpawnEnemy(bool _value) {
        isFaseAttack = _value;

        if(_value)
            StartSpawnEnemy();
    }

    //Funcion que loopea el Spawn
    private void StartSpawnEnemy() {
        if(isFaseAttack)
            StartCoroutine(EmitionEnemy());
    }

    private IEnumerator EmitionEnemy() {
        SpawnEnemy();
        yield return new WaitForSeconds(cooldownEnemy);
        StartSpawnEnemy();
    }

    public void SpawnEnemy() {
        foreach(Transform pos in pos_level_1) {
            CreateEnemy_I(pos);
        }

        if(numLevel >= 1) {
            foreach(Transform pos in pos_level_2) {
                CreateEnemy_I(pos);
            }
        }

        if(numLevel >= 2) {
            foreach(Transform pos in pos_level_3) {
                CreateEnemy_I(pos);
            }
        }
    }

    private void CreateEnemy_I(Transform _pos) {
        GameObject objTemp = null;

        if(pool.Count == 0) {
            objTemp = Instantiate(objEnemy_I);            
        } else {
            objTemp = pool[0];

            pool.Remove(objTemp);
        }

        objTemp.transform.position = _pos.position;
        objTemp.transform.rotation = _pos.rotation;

        inGame.Add(objTemp);
        objTemp.SetActive(true);
        ScriptEnemy(objTemp);
    }

    //Eliminar Luego
    public void SpawnBoss()
    {
        CreateEnemy_Boss(pos_level_3[0]);
    }

    private void CreateEnemy_Boss(Transform _pos)
    {
        GameObject objTemp = null;

        if(poolBoss.Count == 0)
        {
            objTemp = Instantiate(objEnemy_Boss);
        }
        else
        {
            objTemp = pool[0];

            pool.Remove(objTemp);
        }

        objTemp.transform.position = _pos.position;
        objTemp.transform.rotation = _pos.rotation;

        inGame.Add(objTemp);
        objTemp.SetActive(true);
        ScriptEnemy(objTemp);
    }

    private void ScriptEnemy(GameObject _temp) {
        EnemyStats stats = _temp.GetComponent<EnemyStats>();
        stats.GetManager(this);
        stats.GetPosRocket(posRocket);
        stats.GetPosPlayer(posPlayer);
        stats.Inicializar();
    }

    public void Pool(GameObject _value) {
        _value.SetActive(false);

        inGame.Remove(_value);
        pool.Add(_value);
    }

    public void PoolBoss(GameObject _value)
    {
        _value.SetActive(false);

        inGame.Remove(_value);
        poolBoss.Add(_value);
    }

    public void GiveMoneyBag(Transform _pos, float money) {
        game.GiveMoneyBag(_pos, money);
    }
}