using UnityEngine;
using System.Collections;

public class Node : MonoBehaviour {

    public GameObject parentNode = null;

    public int distanceFromTarget;
    public int distanceMoved;
    public int moveCost;
    public int nodeResist;
    public int nodeType = 1;
    public int x;
    public int y;

    public bool walkable = true;


    public int FindDistanceToTarget(int x, int y, GameObject targetNode) {

        int targetX = targetNode.GetComponent<Node>().x;
        int targetY = targetNode.GetComponent<Node>().y;

        return ((Mathf.Abs(x - targetX) + Mathf.Abs(y - targetY)) * 10);
    }


    void Update() {

        SetNodeType();
        if(Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit)) {
                if(hit.collider.name == this.gameObject.name) {
                    this.nodeType++;
                    if(nodeType > 2) {
                        nodeType = 0;
                    }
                }
            }
        }
    }


    void SetNodeType() {

        switch(nodeType) {
            case 0:
                transform.Find("Sphere").renderer.material.color = Color.black;
                nodeResist = 0;
                walkable = false;
                break;
            case 1:
                transform.Find("Sphere").renderer.material.color = Color.green;
                nodeResist = 1;
                walkable = true;
                break;
            case 2:
                transform.Find("Sphere").renderer.material.color = Color.blue;
                nodeResist = 6;
                walkable = true;
                break;
        }
    }
}