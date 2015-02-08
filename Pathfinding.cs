using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pathfinding : MonoBehaviour {

    public GameObject currentNode;
    public GameObject nextNode;
    public GameObject startNode;
    public GameObject targetNode;

    public List<GameObject> closedList = new List<GameObject>();
    public List<GameObject> nodeList = new List<GameObject>();
    public List<GameObject> openList = new List<GameObject>();

    public int distanceMoved = 0;
    public int minNodeValue;

    public bool pathFound = false;


	void Start() {

        openList.Clear();
        closedList.Clear();
        nodeList = GameObject.Find("Level").GetComponent<BuildMap>().nodeList;
        startNode = GameObject.Find("Level").GetComponent<GameControl>().startNode;
        targetNode = GameObject.Find("Level").GetComponent<GameControl>().targetNode;
        currentNode = nodeList.Find(node => node == startNode);
    }


    public void Pathfinder() {
        if(!pathFound) {
            if(openList.Count == 0 && closedList.Count == 0) {
                closedList.Add(currentNode);
            }

            List<GameObject> adjacentNodes = FindAdjacentNodes(currentNode, nodeList);

            nextNode = null;
            distanceMoved++;

            for (var i = 0; i < adjacentNodes.Count; i++) {
                if(!closedList.Contains(adjacentNodes[i])) {
                    if(!openList.Contains(adjacentNodes[i])) {
                        adjacentNodes[i].GetComponent<Node>().moveCost = distanceMoved;
                        openList.Add(adjacentNodes[i]);
                        if(nextNode == null) {
                            nextNode = adjacentNodes[i];
                            minNodeValue = adjacentNodes[i].GetComponent<Node>().moveCost + adjacentNodes[i].GetComponent<Node>().distanceFromTarget;
                        }
                        else {
                            if((adjacentNodes[i].GetComponent<Node>().moveCost + adjacentNodes[i].GetComponent<Node>().distanceFromTarget) < minNodeValue) {
                                nextNode = adjacentNodes[i];
                                minNodeValue = adjacentNodes[i].GetComponent<Node>().moveCost + adjacentNodes[i].GetComponent<Node>().distanceFromTarget;
                            }
                        }
                    }
                }
            }
            closedList.Add(nextNode);
            openList.Remove(nextNode);
            currentNode = nextNode;

            if(currentNode == targetNode) {
                pathFound = true;
            }
        }
    }


    List<GameObject> FindAdjacentNodes(GameObject node, List<GameObject> nodeList) {

        int currentNodeX = node.GetComponent<Node>().x;
        int currentNodeY = node.GetComponent<Node>().y;

        List<GameObject> nodes = new List<GameObject>();
        if(nodeList.Find(foundNode => foundNode.GetComponent<Node>().x == currentNodeX && foundNode.GetComponent<Node>().y == currentNodeY + 1)) {
            nodes.Add(nodeList.Find(foundNode => foundNode.GetComponent<Node>().x == currentNodeX && foundNode.GetComponent<Node>().y == currentNodeY + 1));
        }
        if(nodeList.Find(foundNode => foundNode.GetComponent<Node>().x == currentNodeX && foundNode.GetComponent<Node>().y == currentNodeY - 1)) {
            nodes.Add(nodeList.Find(foundNode => foundNode.GetComponent<Node>().x == currentNodeX && foundNode.GetComponent<Node>().y == currentNodeY - 1));
        }
        if(nodeList.Find(foundNode => foundNode.GetComponent<Node>().x == currentNodeX + 1 && foundNode.GetComponent<Node>().y == currentNodeY)) {
            nodes.Add(nodeList.Find(foundNode => foundNode.GetComponent<Node>().x == currentNodeX + 1 && foundNode.GetComponent<Node>().y == currentNodeY));
        }
        if(nodeList.Find(foundNode => foundNode.GetComponent<Node>().x == currentNodeX - 1 && foundNode.GetComponent<Node>().y == currentNodeY)) {
            nodes.Add(nodeList.Find(foundNode => foundNode.GetComponent<Node>().x == currentNodeX - 1 && foundNode.GetComponent<Node>().y == currentNodeY));
        }
        return nodes;
    }
}
