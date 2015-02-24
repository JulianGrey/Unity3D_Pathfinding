using UnityEngine;
using System.Collections;

public class Node : MonoBehaviour {

    public GameObject parentNode = null;

    public int distanceFromTarget;
    public int distanceMoved;
    public int moveCost;
    public int nodeResist;
    public int nodeType;
    public int x;
    public int y;

    public bool walkable = true;


    public int FindDistanceToTarget(int x, int y, GameObject targetNode) {

        int targetX = targetNode.GetComponent<Node>().x;
        int targetY = targetNode.GetComponent<Node>().y;

        return ((Mathf.Abs(x - targetX) + Mathf.Abs(y - targetY)) * 10);
    }
}