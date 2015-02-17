using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildMap : MonoBehaviour {

    public GameObject ground;
    public GameObject wall;

    public List<GameObject> nodeList = new List<GameObject>();

    public int[,] myMap = {
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
        {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
        {1, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1},
        {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
        {1, 0, 0, 0, 0, 0, 2, 2, 0, 0, 0, 1},
        {1, 0, 0, 0, 0, 0, 2, 2, 0, 0, 0, 1},
        {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
        {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1},
        {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1}
    };


    void Start() {

        CreateStringMap();
        transform.GetComponent<GameControl>().enabled = true;
    }

    void CreateStringMap() {

        Debug.Log(myMap.GetLength(0) + " " + myMap.GetLength(1));
        GameObject mapNode;
        for(var j = 0; j < myMap.GetLength(0); j++) {
            for(var i = 0; i < myMap.GetLength(1); i++) {
                if(myMap[j, i] == 1) {
                    mapNode = Instantiate(wall, new Vector3(i * 1.0f, 0, j * -1.0f), Quaternion.identity) as GameObject;
                }
                else {
                    mapNode = Instantiate(ground, new Vector3(i * 1.0f, 0, j * -1.0f), Quaternion.identity) as GameObject;
                }
                mapNode.name = "Node (" + i + ", " + j + ")";
                mapNode.GetComponent<Node>().x = i;
                mapNode.GetComponent<Node>().y = j;
                mapNode.GetComponent<Node>().nodeType = myMap[j, i];
                nodeList.Add(mapNode);
                Debug.Log(i + " " + j);
            }
        }
    }
}