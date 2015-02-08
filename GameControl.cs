using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameControl : MonoBehaviour {

    public GameObject startNode;
    public GameObject unit;

    public List<GameObject> nodeList = new List<GameObject>();

    public bool unitSpawned = false;


    void Start() {
        nodeList = transform.GetComponent<BuildMap>().nodeList;
        startNode = nodeList.Find(node => node.GetComponent<Node>().x == 8 && node.GetComponent<Node>().y == 1);
    }


    void Update() {
        if(!unitSpawned) {
            Instantiate(unit, new Vector3(startNode.transform.position.x, startNode.transform.position.y, startNode.transform.position.z), Quaternion.identity);
            unitSpawned = true;
        }
    }
}