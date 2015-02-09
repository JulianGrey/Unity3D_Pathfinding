using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pathfinding : MonoBehaviour {

    public GameObject nextNode;
    public GameObject targetNode;

    public List<GameObject> nodeList = new List<GameObject>();

    public int distanceMoved = 0;


    void Start() {

        nodeList = GameObject.Find("Level").GetComponent<BuildMap>().nodeList;
    }


    public List<GameObject> Pathfinder(GameObject currentNode) {

        distanceMoved = 0;

        GameObject initialNode = currentNode;

        List<GameObject> closedList = new List<GameObject>();
        List<GameObject> completeList = new List<GameObject>();
        List<GameObject> openList = new List<GameObject>();

        while(currentNode != targetNode) {
            if(openList.Count == 0 && closedList.Count == 0) {
                closedList.Add(currentNode);
            }

            List<GameObject> adjacentNodes = FindAdjacentNodes(currentNode, nodeList, openList, closedList);
            int minNodeValue = 0;
            nextNode = null;
            distanceMoved++;

            for (var i = 0; i < adjacentNodes.Count; i++) {
                int distanceFromTarget = adjacentNodes[i].GetComponent<Node>().FindDistanceToTarget(adjacentNodes[i].GetComponent<Node>().x, adjacentNodes[i].GetComponent<Node>().y, targetNode);
                adjacentNodes[i].GetComponent<Node>().parentNode = currentNode;
                adjacentNodes[i].GetComponent<Node>().distanceMoved = distanceMoved;
                adjacentNodes[i].GetComponent<Node>().distanceFromTarget = distanceFromTarget;
                adjacentNodes[i].GetComponent<Node>().moveCost = distanceMoved + distanceFromTarget;
                openList.Add(adjacentNodes[i]);
                minNodeValue = GetLowestNodeValue(openList);
            }
            nextNode = GetNextNode(openList, minNodeValue);
            closedList.Add(nextNode);
            openList.Remove(nextNode);
            currentNode = nextNode;
        }
        completeList = FindCompletePath(closedList, initialNode);
        completeList.Reverse();
        return completeList;
    }


    List<GameObject> FindAdjacentNodes(GameObject node, List<GameObject> nodeList, List<GameObject> openList, List<GameObject> closedList) {

        int currentNodeX = node.GetComponent<Node>().x;
        int currentNodeY = node.GetComponent<Node>().y;

        List<GameObject> nodes = new List<GameObject>();

        GameObject upNode = nodeList.Find(foundNode => foundNode.GetComponent<Node>().x == currentNodeX && foundNode.GetComponent<Node>().y == currentNodeY + 1 && foundNode.GetComponent<Node>().walkable == true);
        GameObject rightNode = nodeList.Find(foundNode => foundNode.GetComponent<Node>().x == currentNodeX + 1 && foundNode.GetComponent<Node>().y == currentNodeY && foundNode.GetComponent<Node>().walkable == true);
        GameObject downNode = nodeList.Find(foundNode => foundNode.GetComponent<Node>().x == currentNodeX && foundNode.GetComponent<Node>().y == currentNodeY - 1 && foundNode.GetComponent<Node>().walkable == true);
        GameObject leftNode = nodeList.Find(foundNode => foundNode.GetComponent<Node>().x == currentNodeX - 1 && foundNode.GetComponent<Node>().y == currentNodeY && foundNode.GetComponent<Node>().walkable == true);

        if(upNode != null && !openList.Contains(upNode) && !closedList.Contains(upNode)) {
            nodes.Add(upNode);
        }
        if(rightNode != null && !openList.Contains(rightNode) && !closedList.Contains(rightNode)) {
            nodes.Add(rightNode);
        }
        if(downNode != null && !openList.Contains(downNode) && !closedList.Contains(downNode)) {
            nodes.Add(downNode);
        }
        if(leftNode != null && !openList.Contains(leftNode) && !closedList.Contains(leftNode)) {
            nodes.Add(leftNode);
        }
        return nodes;
    }


    int GetLowestNodeValue(List<GameObject> openList) {

        int minNodeValue = 0;
        for(var i = 0; i < openList.Count; i++) {
            if(minNodeValue == 0) {
                minNodeValue = openList[i].GetComponent<Node>().moveCost;
            }
            else {
                if(openList[i].GetComponent<Node>().moveCost < minNodeValue) {
                    minNodeValue = openList[i].GetComponent<Node>().moveCost;
                }
            }
        }
        return minNodeValue;
    }


    GameObject GetNextNode(List<GameObject> openList, int minNodeValue) {

        List<GameObject> nextNodeList = new List<GameObject>();
        for(var i = 0; i < openList.Count; i++) {
            if(openList[i].GetComponent<Node>().moveCost == minNodeValue) {
                nextNodeList.Add(openList[i]);
            }
        }
        return nextNodeList[nextNodeList.Count - 1];
    }


    List<GameObject> FindCompletePath(List<GameObject> closedList, GameObject start) {

        List<GameObject> completeList = new List<GameObject>();
        GameObject current = closedList.Find(node => node == targetNode);

        completeList.Add(current);
        while(current != start) {
            completeList.Add(current.GetComponent<Node>().parentNode);
            current = current.GetComponent<Node>().parentNode;
        }
        return completeList;
    }
}