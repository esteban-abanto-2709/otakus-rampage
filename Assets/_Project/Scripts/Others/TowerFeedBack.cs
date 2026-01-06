using UnityEngine;

public class TowerFeedBack : MonoBehaviour {

    [SerializeField] private Material wrong = null;
    [SerializeField] private Material right = null;

    [SerializeField] private MeshRenderer[] render = new MeshRenderer[0];

    public void IsPosRight(bool _value) {
        foreach(MeshRenderer rend in render) {
            if(_value)
                rend.material = right;
            else
                rend.material = wrong;
        }
    }
}