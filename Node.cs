using UnityEngine;
using System.Collections;

public class Node : MonoBehaviour {

    public GameObject parentNode = null;

    public int distanceFromTarget;
    public int distanceMoved;
    public int moveCost;
    public int x;
    public int y;

    public bool walkable = true;


    public int FindDistanceToTarget(int x, int y, GameObject targetNode) {

        int targetX = targetNode.GetComponent<Node>().x;
        int targetY = targetNode.GetComponent<Node>().y;

        return (Mathf.Abs(x - targetX) + Mathf.Abs(y - targetY));
    }


    void Update() {

        if(Input.GetMouseButtonDown(0)) {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit)) {
                if(hit.collider.name == this.gameObject.name) {
                    if(walkable == true) {
                        walkable = false;
                    }
                    else {
                        walkable = true;
                    }
                }
            }
        }
        SetNodeColour();
    }


    void SetNodeColour() {

        if(walkable == true) {
            transform.Find("Sphere").renderer.material.color = Color.red;
        }
        else {
            transform.Find("Sphere").renderer.material.color = Color.black;
        }
    }
}