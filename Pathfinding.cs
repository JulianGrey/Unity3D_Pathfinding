using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pathfinding : MonoBehaviour {

    public GameObject nextNode;
    public GameObject targetNode;

    public List<GameObject> nodeList = new List<GameObject>();


    void Start() {

        nodeList = GameObject.Find("Level").GetComponent<BuildMap>().nodeList;
    }


    public List<GameObject> Pathfinder(GameObject currentNode) {

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

            for (var i = 0; i < adjacentNodes.Count; i++) {
                int distanceFromTarget = adjacentNodes[i].GetComponent<Node>().FindDistanceToTarget(adjacentNodes[i].GetComponent<Node>().x, adjacentNodes[i].GetComponent<Node>().y, targetNode);
                adjacentNodes[i].GetComponent<Node>().parentNode = currentNode;
                if(currentNode.GetComponent<Node>().parentNode != null) {
                    if(Mathf.Abs((adjacentNodes[i].GetComponent<Node>().x - currentNode.GetComponent<Node>().x) + (adjacentNodes[i].GetComponent<Node>().y - currentNode.GetComponent<Node>().y)) == 1) {
                        adjacentNodes[i].GetComponent<Node>().distanceMoved = currentNode.GetComponent<Node>().distanceMoved + (adjacentNodes[i].GetComponent<Node>().nodeResist * 10);
                    }
                    else {
                        adjacentNodes[i].GetComponent<Node>().distanceMoved = currentNode.GetComponent<Node>().distanceMoved + (adjacentNodes[i].GetComponent<Node>().nodeResist * 14);
                    }
                }
                else {
                    adjacentNodes[i].GetComponent<Node>().distanceMoved = 10;
                }
                adjacentNodes[i].GetComponent<Node>().distanceFromTarget = distanceFromTarget;
                adjacentNodes[i].GetComponent<Node>().moveCost = adjacentNodes[i].GetComponent<Node>().distanceMoved + distanceFromTarget;
                openList.Add(adjacentNodes[i]);
            }
            minNodeValue = GetLowestNodeValue(openList);
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
        GameObject upRightNode = nodeList.Find(foundNode => foundNode.GetComponent<Node>().x == currentNodeX + 1 && foundNode.GetComponent<Node>().y == currentNodeY + 1 && foundNode.GetComponent<Node>().walkable == true);

        GameObject rightNode = nodeList.Find(foundNode => foundNode.GetComponent<Node>().x == currentNodeX + 1 && foundNode.GetComponent<Node>().y == currentNodeY && foundNode.GetComponent<Node>().walkable == true);
        GameObject downRightNode = nodeList.Find(foundNode => foundNode.GetComponent<Node>().x == currentNodeX + 1 && foundNode.GetComponent<Node>().y == currentNodeY - 1 && foundNode.GetComponent<Node>().walkable == true);

        GameObject downNode = nodeList.Find(foundNode => foundNode.GetComponent<Node>().x == currentNodeX && foundNode.GetComponent<Node>().y == currentNodeY - 1 && foundNode.GetComponent<Node>().walkable == true);
        GameObject downLeftNode = nodeList.Find(foundNode => foundNode.GetComponent<Node>().x == currentNodeX - 1 && foundNode.GetComponent<Node>().y == currentNodeY - 1 && foundNode.GetComponent<Node>().walkable == true);

        GameObject leftNode = nodeList.Find(foundNode => foundNode.GetComponent<Node>().x == currentNodeX - 1 && foundNode.GetComponent<Node>().y == currentNodeY && foundNode.GetComponent<Node>().walkable == true);
        GameObject upLeftNode = nodeList.Find(foundNode => foundNode.GetComponent<Node>().x == currentNodeX - 1 && foundNode.GetComponent<Node>().y == currentNodeY + 1 && foundNode.GetComponent<Node>().walkable == true);

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
        if(nodes.Contains(upNode) && nodes.Contains(rightNode)) {
            if(upRightNode != null && !openList.Contains(upRightNode) && !closedList.Contains(upRightNode)) {
                nodes.Add(upRightNode);
            }
        }
        if(nodes.Contains(downNode) && nodes.Contains(rightNode)) {
            if(downRightNode != null && !openList.Contains(downRightNode) && !closedList.Contains(downRightNode)) {
                nodes.Add(downRightNode);
            }
        }
        if(nodes.Contains(downNode) && nodes.Contains(leftNode)) {
            if(downLeftNode != null && !openList.Contains(downLeftNode) && !closedList.Contains(downLeftNode)) {
                nodes.Add(downLeftNode);
            }
        }
        if(nodes.Contains(upNode) && nodes.Contains(leftNode)) {
            if(upLeftNode != null && !openList.Contains(upLeftNode) && !closedList.Contains(upLeftNode)) {
                nodes.Add(upLeftNode);
            }
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
        if(nextNodeList.Count > 1) {
            int minDistanceFromTarget = 0;
            GameObject consideredNode = null;

            for(var i = 0; i < nextNodeList.Count; i++) {
                if(i == 0) {
                    minDistanceFromTarget = nextNodeList[i].GetComponent<Node>().distanceFromTarget;
                    consideredNode = nextNodeList[i];
                }
                else {
                    if(nextNodeList[i].GetComponent<Node>().distanceFromTarget < minDistanceFromTarget) {
                        minDistanceFromTarget = nextNodeList[i].GetComponent<Node>().distanceFromTarget;
                        consideredNode = nextNodeList[i];
                    }
                    else if(nextNodeList[i].GetComponent<Node>().distanceFromTarget == minDistanceFromTarget) {
                        consideredNode = nextNodeList[nextNodeList.Count - 1];
                    }
                }
            }
            return consideredNode;
        }
        else {
            return nextNodeList[nextNodeList.Count - 1];
        }
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