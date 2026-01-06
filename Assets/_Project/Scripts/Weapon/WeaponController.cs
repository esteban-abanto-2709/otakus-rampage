using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WeaponStats))]
public class WeaponController : MonoBehaviour {

    private WeaponManager scrManager = null;
    private WeaponStats scrStats = null;
    private bool isReloading = false;

    private List<GameObject> inGame = new List<GameObject>();
    private List<GameObject> pool = new List<GameObject>();

    private void Awake() {
        scrStats = GetComponent<WeaponStats>();
    }

    private void OnEnable() {
        scrStats.AnimReload(false);
        isReloading = false;
    }

    public void SetManager(WeaponManager _val) {
        scrManager = _val;
    }

    private bool mode;
    public void Shoot(Vector3 _guide) {
        scrStats.LogicCoolDown(ref mode);

        if(!mode)
            return;

        if(isReloading || scrStats.AmmoCurrent() == 0)
            return;

        scrStats.CompShoot();
        GameObject objTemp = null;

        if(pool.Count == 0) {
            objTemp = Instantiate(scrStats.Bullet());
            objTemp.transform.position = scrStats.Spawn().position;
            objTemp.transform.rotation = scrStats.Spawn().rotation;

            inGame.Add(objTemp);
        } else {
            objTemp = pool[0];
            objTemp.transform.position = scrStats.Spawn().position;
            objTemp.transform.rotation = scrStats.Spawn().rotation;
            objTemp.SetActive(true);

            pool.Remove(objTemp);
            inGame.Add(objTemp);
        }

        ScriptBullet(objTemp, _guide);
        scrStats.FeedBackAmmo();

        if(scrStats.AmmoCurrent() <= 0)
            StartReload();
    }

    private void ScriptBullet(GameObject val, Vector3 guide) {
        Bullet temp = null;
        temp = val.GetComponent<Bullet>();
        temp.AddController(this);
        temp.ReplaceForce(scrStats.ForceShoot());
        temp.ReplaceDamage(scrStats.Damage());
        temp.ReplaceImpact(scrStats.ImpactForce());
        temp.StartShoot(guide);
    }

    public void Pool(GameObject _value) {
        _value.SetActive(false);

        inGame.Remove(_value);
        pool.Add(_value);
    }

    public void StartReload() {
        StartCoroutine(Reload());
    }

    private IEnumerator Reload() {
        isReloading = true;
        scrStats.CompReload();
        scrManager.UpdateButtons();
        scrStats.AnimReload(true);
        yield return new WaitForSeconds(scrStats.ReloadTime());
        scrStats.AnimReload(false);
        scrStats.FeedBackAmmo();
        scrManager.FeedBackAmmo();
        isReloading = false;
    }

    public void GiveDestroyBullet(Vector3 _pos) {
        scrManager.GiveDestroyBullet(_pos);
    }
}