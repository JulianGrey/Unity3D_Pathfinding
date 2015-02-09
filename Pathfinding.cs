using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pathfinding : MonoBehaviour {

    public GameObject currentNode;
    public GameObject nextNode;
    public GameObject targetNode;

    public List<GameObject> nodeList = new List<GameObject>();

    public int distanceMoved = 0;
    public int minNodeValue;


	void Start() {

        nodeList = GameObject.Find("Level").GetComponent<BuildMap>().nodeList;
    }


    public List<GameObject> Pathfinder(GameObject currentNode) {

        distanceMoved = 0;
        minNodeValue = 0;
        List<GameObject> closedList = new List<GameObject>();
        List<GameObject> openList = new List<GameObject>();

        while(currentNode != targetNode) {
            if(openList.Count == 0 && closedList.Count == 0) {
                closedList.Add(currentNode);
            }

            List<GameObject> adjacentNodes = FindAdjacentNodes(currentNode, nodeList);
            nextNode = null;
            distanceMoved++;

            for (var i = 0; i < adjacentNodes.Count; i++) {
                if(!closedList.Contains(adjacentNodes[i])) {
                    if(!openList.Contains(adjacentNodes[i])) {
                        int distanceFromTarget = adjacentNodes[i].GetComponent<Node>().FindDistanceToTarget(adjacentNodes[i].GetComponent<Node>().x, adjacentNodes[i].GetComponent<Node>().y, targetNode);
                        adjacentNodes[i].GetComponent<Node>().moveCost = distanceMoved;
                        openList.Add(adjacentNodes[i]);
                        if(nextNode == null) {
                            nextNode = adjacentNodes[i];
                            minNodeValue = adjacentNodes[i].GetComponent<Node>().moveCost + distanceFromTarget;
                        }
                        else {
                            if((adjacentNodes[i].GetComponent<Node>().moveCost + distanceFromTarget) < minNodeValue) {
                                nextNode = adjacentNodes[i];
                                minNodeValue = adjacentNodes[i].GetComponent<Node>().moveCost + distanceFromTarget;
                            }
                        }
                    }
                }
            }
            closedList.Add(nextNode);
            openList.Remove(nextNode);
            currentNode = nextNode;
        }
        return closedList;
    }


    List<GameObject> FindAdjacentNodes(GameObject node, List<GameObject> nodeList) {

        int currentNodeX = node.GetComponent<Node>().x;
        int currentNodeY = node.GetComponent<Node>().y;

        List<GameObject> nodes = new List<GameObject>();
        if(nodeList.Find(foundNode => foundNode.GetComponent<Node>().x == currentNodeX && foundNode.GetComponent<Node>().y == currentNodeY + 1 && foundNode.GetComponent<Node>().walkable == true)) {
            nodes.Add(nodeList.Find(foundNode => foundNode.GetComponent<Node>().x == currentNodeX && foundNode.GetComponent<Node>().y == currentNodeY + 1));
        }
        if(nodeList.Find(foundNode => foundNode.GetComponent<Node>().x == currentNodeX && foundNode.GetComponent<Node>().y == currentNodeY - 1 && foundNode.GetComponent<Node>().walkable == true)) {
            nodes.Add(nodeList.Find(foundNode => foundNode.GetComponent<Node>().x == currentNodeX && foundNode.GetComponent<Node>().y == currentNodeY - 1));
        }
        if(nodeList.Find(foundNode => foundNode.GetComponent<Node>().x == currentNodeX + 1 && foundNode.GetComponent<Node>().y == currentNodeY && foundNode.GetComponent<Node>().walkable == true)) {
            nodes.Add(nodeList.Find(foundNode => foundNode.GetComponent<Node>().x == currentNodeX + 1 && foundNode.GetComponent<Node>().y == currentNodeY));
        }
        if(nodeList.Find(foundNode => foundNode.GetComponent<Node>().x == currentNodeX - 1 && foundNode.GetComponent<Node>().y == currentNodeY && foundNode.GetComponent<Node>().walkable == true)) {
            nodes.Add(nodeList.Find(foundNode => foundNode.GetComponent<Node>().x == currentNodeX - 1 && foundNode.GetComponent<Node>().y == currentNodeY));
        }
        return nodes;
    }
}
