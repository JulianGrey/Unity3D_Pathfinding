using UnityEngine;
using System.Collections;

public class Node : MonoBehaviour {
    
    public GameObject targetNode;

    public int distanceFromTarget;
    public int distanceMoved;
    public int moveCost;
    public int x;
    public int y;

    // public bool walkable = true;


    void Update() {

        targetNode = GameObject.Find("Level").GetComponent<GameControl>().targetNode;
        distanceFromTarget = FindDistanceToTarget(x, y, targetNode);
    }

    int FindDistanceToTarget(int x, int y, GameObject targetNode) {

        int targetX = targetNode.GetComponent<Node>().x;
        int targetY = targetNode.GetComponent<Node>().y;

        return (Mathf.Abs(x - targetX) + Mathf.Abs(y - targetY));
    }
}