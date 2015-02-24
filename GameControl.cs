using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameControl : MonoBehaviour {

    public GameObject ground;
    public GameObject wall;
    public GameObject spawnPoint;
    public GameObject unit;

    public List<GameObject> nodeList = new List<GameObject>();
    public List<GameObject> walkableNodeList = new List<GameObject>();
    public List<GameObject> spawnPointList = new List<GameObject>();

    public int numWalkableNodes;
    public int numSpawnPoints = 5;
    private int unitsSpawned = 0;
    private int maxNumberOfUnits = 3;

    private int[,] myMap = {
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
        {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 2, 2, 2, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 2, 2, 2, 1},
        {1, 0, 1, 1, 1, 1, 0, 0, 1, 0, 0, 0, 1, 2, 2, 2, 1, 0, 1, 1, 1, 1, 0, 0, 1, 0, 0, 0, 1, 2, 2, 2, 1},
        {1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 2, 2, 2, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 2, 2, 2, 1},
        {1, 0, 0, 0, 0, 1, 2, 2, 2, 0, 0, 0, 1, 2, 2, 2, 1, 0, 0, 0, 0, 1, 2, 2, 2, 0, 0, 0, 1, 2, 2, 2, 1},
        {1, 0, 0, 0, 0, 1, 2, 2, 2, 0, 0, 0, 0, 0, 2, 2, 1, 0, 0, 0, 0, 1, 2, 2, 2, 0, 0, 0, 0, 0, 2, 2, 1},
        {1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 1, 0, 0, 0, 0, 2, 2, 2, 2, 2, 2, 0, 0, 0, 2, 2, 1},
        {1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 1},
        {1, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
        {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
        {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 1},
        {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 2, 2, 2, 2, 2, 2, 0, 0, 0, 0, 0, 1},
        {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 2, 2, 2, 2, 2, 2, 0, 0, 0, 0, 0, 1},
        {1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
        {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 2, 2, 2, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 2, 2, 2, 1},
        {1, 0, 1, 1, 1, 1, 0, 0, 1, 0, 0, 0, 1, 2, 2, 2, 1, 0, 1, 1, 1, 1, 0, 0, 1, 0, 0, 0, 1, 2, 2, 2, 1},
        {1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 2, 2, 2, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 2, 2, 2, 1},
        {1, 0, 0, 0, 0, 1, 2, 2, 2, 0, 0, 0, 1, 2, 2, 2, 1, 0, 0, 0, 0, 1, 2, 2, 2, 0, 0, 0, 1, 2, 2, 2, 1},
        {1, 0, 0, 0, 0, 1, 2, 2, 2, 0, 0, 0, 0, 0, 2, 2, 1, 0, 0, 0, 0, 1, 2, 2, 2, 0, 0, 0, 0, 0, 2, 2, 1},
        {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 1},
        {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
        {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
        {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
        {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
        {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
        {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}
    };


    void Start() {

        BuildMapFromArray();
        SetSpawnPoint();
        SpawnUnits();
        // transform.GetComponent<GameControl>().enabled = true;
    }

    
    void BuildMapFromArray() {

        GameObject mapNode;
        for(var j = 0; j < myMap.GetLength(0); j++) {
            for(var i = 0; i < myMap.GetLength(1); i++) {
                if(myMap[j, i] == 1) {
                    mapNode = Instantiate(wall, new Vector3(i * 1.0f, 0, j * -1.0f), Quaternion.identity) as GameObject;
                    mapNode.GetComponent<Node>().walkable = false;
                    mapNode.transform.Find("Model").renderer.material.color = Color.black;
                }
                else {
                    mapNode = Instantiate(ground, new Vector3(i * 1.0f, 0, j * -1.0f), Quaternion.identity) as GameObject;
                    mapNode.GetComponent<Node>().walkable = true;
                    if(myMap[j, i] == 0) {
                        mapNode.transform.Find("Model").renderer.material.color = Color.green;
                        mapNode.GetComponent<Node>().nodeResist = 1;
                    }
                    else if(myMap[j, i] == 2) {
                        mapNode.transform.Find("Model").renderer.material.color = Color.blue;
                        mapNode.GetComponent<Node>().nodeResist = 6;
                    }
                }
                mapNode.name = "Node (" + i + ", " + j + ")";
                mapNode.GetComponent<Node>().x = i;
                mapNode.GetComponent<Node>().y = j;
                mapNode.GetComponent<Node>().nodeType = myMap[j, i];
                nodeList.Add(mapNode);
            }
        }
    }


    void SetSpawnPoint() {

        GameObject spawnNode;
        for(var i = 0; i < nodeList.Count; i++) {
            if(nodeList[i].GetComponent<Node>().walkable) {
                walkableNodeList.Add(nodeList[i]);
            }
        }
        for(var i = 0; i < numSpawnPoints; i++) {
            int randomNode = Mathf.RoundToInt(Random.value * walkableNodeList.Count);
            spawnNode = Instantiate(spawnPoint, walkableNodeList[randomNode].transform.position, Quaternion.identity) as GameObject;
            spawnNode.name = "Spawnpoint " + (i + 1);
            spawnPointList.Add(spawnNode);
        }
        numWalkableNodes = walkableNodeList.Count;
    }


    void SpawnUnits() {
        GameObject player;
        for(var i = 0; i < maxNumberOfUnits; i++) {
            int randomSpawn = Mathf.RoundToInt(Random.value * numSpawnPoints);
            if(randomSpawn >= numSpawnPoints) {
                randomSpawn = numSpawnPoints - 1;
            }
            GameObject startNode = spawnPointList[randomSpawn];
            player = Instantiate(unit, new Vector3(startNode.transform.position.x, startNode.transform.position.y, startNode.transform.position.z), Quaternion.identity) as GameObject;
            player.name = "Unit " + (i + 1);
            // GameObject.Find("Main Camera").GetComponent<MoveCamera>().player = player;
            // GameObject.Find("Main Camera").GetComponent<MoveCamera>().enabled = true;
        }
    }
}