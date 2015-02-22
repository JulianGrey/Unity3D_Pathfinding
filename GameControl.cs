using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameControl : MonoBehaviour {

    public GameObject startNode;
    public GameObject unit;

    public List<GameObject> nodeList = new List<GameObject>();
    public List<GameObject> walkableNodeList = new List<GameObject>();

    public int unitsSpawned = 0;
    public int maxNumberOfUnits = 1;

    public bool unitSpawned = false;


    void Start() {
        nodeList = transform.GetComponent<BuildMap>().nodeList;
        for(var i = 0; i < nodeList.Count; i++) {
            if(nodeList[i].GetComponent<Node>().walkable) {
                walkableNodeList.Add(nodeList[i]);
            }
        }
        int randomNode = Mathf.RoundToInt(Random.value * walkableNodeList.Count);
        startNode = walkableNodeList[randomNode];
    }


    void Update() {
        if(unitsSpawned < maxNumberOfUnits) {
            GameObject player;
            player = Instantiate(unit, new Vector3(startNode.transform.position.x, startNode.transform.position.y, startNode.transform.position.z), Quaternion.identity) as GameObject;
            player.name = "Unit " + (unitsSpawned + 1);
            unitsSpawned++;
            GameObject.Find("Main Camera").GetComponent<MoveCamera>().player = player;
            GameObject.Find("Main Camera").GetComponent<MoveCamera>().enabled = true;
        }
    }
}