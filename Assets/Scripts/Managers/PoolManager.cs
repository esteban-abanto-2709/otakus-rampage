using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour {

    [SerializeField] private GameObject objDestroyBullet = null;
    [SerializeField] private GameObject objMoneyBag = null;
    [SerializeField] private GameObject objBulletTower = null;
    [SerializeField] private GameObject objOtakuFrut = null;

    [SerializeField] private List<GameObject> inGame = new List<GameObject>();

    [SerializeField] private List<GameObject> pool_destroyBullet = new List<GameObject>();
    [SerializeField] private List<GameObject> pool_moneyBag = new List<GameObject>();
    [SerializeField] private List<GameObject> pool_bulletTower = new List<GameObject>();
    [SerializeField] private List<GameObject> pool_otakuFrut = new List<GameObject>();

    public void GiveMoneyBag(Transform _pos, float _money) {
        GameObject temp;

        if(pool_moneyBag.Count == 0) {
            temp = Instantiate(objMoneyBag);
        } else {
            temp = pool_moneyBag[0];
            temp.SetActive(true);

            pool_moneyBag.Remove(temp);
        }

        temp.transform.position = _pos.position;
        temp.transform.rotation = _pos.rotation;

        BagMoney scr = temp.GetComponent<BagMoney>();
        scr.SetManager(this);
        scr.SetAmounCash(_money);

        inGame.Add(temp);
    }
    public void Pool_ManeyBag(GameObject temp) {
        temp.SetActive(false);

        inGame.Remove(temp);
        pool_moneyBag.Add(temp);
    }

    public void GiveOtakuFrut(Vector3 _pos, Quaternion _rot) {
        GameObject temp;

        if(pool_otakuFrut.Count == 0) {
            temp = Instantiate(objOtakuFrut);
        } else {
            temp = pool_otakuFrut[0];
            temp.SetActive(true);

            pool_otakuFrut.Remove(temp);
        }

        temp.transform.position = _pos;
        temp.transform.rotation = _rot;

        Frut scr = temp.GetComponent<Frut>();
        scr.SetManager(this);

        inGame.Add(temp);
    }
    public void Pool_OtakuFrut(GameObject temp) {
        temp.SetActive(false);

        inGame.Remove(temp);
        pool_otakuFrut.Add(temp);
    }

    public void GiveBulletTower(Transform _pos, float _force, float _damage, float _impact) {
        GameObject temp;

        if(pool_bulletTower.Count == 0) {
            temp = Instantiate(objBulletTower);
        } else {
            temp = pool_bulletTower[0];
            temp.SetActive(true);

            pool_bulletTower.Remove(temp);
        }

        temp.transform.position = _pos.position;
        temp.transform.rotation = _pos.rotation;

        BulletTower scr = temp.GetComponent<BulletTower>();
        scr.SetManager(this);
        scr.ReplaceForce(_force);
        scr.ReplaceDamage(_damage);
        scr.ReplaceImpact(_impact);
        scr.StartShoot(_pos);

        inGame.Add(temp);
    }
    public void Pool_BulletTower(GameObject temp) {
        temp.SetActive(false);

        inGame.Remove(temp);
        pool_bulletTower.Add(temp);
    }

    public void GiveDestroyBullet(Vector3 _pos) {
        GameObject temp;

        if(pool_destroyBullet.Count == 0) {
            temp = Instantiate(objDestroyBullet);
        } else {
            temp = pool_destroyBullet[0];
            temp.SetActive(true);

            pool_destroyBullet.Remove(temp);
        }

        temp.transform.position = _pos;
        temp.GetComponent<ParticleSystem>().Play();
        inGame.Add(temp);
        StartCoroutine(FeedBackDestroy(temp));
    }

    public void Pool_DestroyBullet(GameObject temp) {
        temp.SetActive(false);

        inGame.Remove(temp);
        pool_destroyBullet.Add(temp);
    }

    private IEnumerator FeedBackDestroy(GameObject temp) {
        yield return new WaitForSeconds(1);
        Pool_DestroyBullet(temp);
    }
}