using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameControl : MonoBehaviour {

    private GameObject startNode;
    public GameObject unit;

    private List<GameObject> nodeList;
    private List<GameObject> walkableNodeList;

    private int unitsSpawned = 0;
    private int maxNumberOfUnits = 1;


    void Start() {
        nodeList = transform.GetComponent<BuildMap>().nodeList;
        walkableNodeList = transform.GetComponent<BuildMap>().walkableNodeList;
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