using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPointPosition : MonoBehaviour{

    void Start(){WaysControllerSctipt.Instance.NewPositionTower(transform.position, (side == Side.Left) );}

    public enum Side{ Left, Right }
    [SerializeField] private Side side;
}
