using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildMap : MonoBehaviour {

    public GameObject node;

    public List<GameObject> nodeList = new List<GameObject>();

    public int mapHeight = 8;
    public int mapWidth = 10;


    void Start() {

        CreateMap(mapWidth, mapHeight);
        transform.GetComponent<GameControl>().enabled = true;
    }


    void CreateMap(int mapWidth, int mapHeight) {

        for(var j = 0; j < mapHeight; j++) {
            for(var i = 0; i < mapWidth; i++) {
                GameObject mapNode = Instantiate(node, new Vector3(i * 1.0f, 0, j * 1.0f), Quaternion.identity) as GameObject;
                mapNode.name = "Node (" + i + ", " + j + ")";
                mapNode.GetComponent<Node>().x = i;
                mapNode.GetComponent<Node>().y = j;
                nodeList.Add(mapNode);
            }
        }
    }
}