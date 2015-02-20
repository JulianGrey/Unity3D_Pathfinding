using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitMove : MonoBehaviour {

    public GameObject previousNode;
    public GameObject targetNode;

    public List<GameObject> closedList = new List<GameObject>();
    public List<GameObject> nodeList = new List<GameObject>();
    public List<GameObject> walkableNodeList = new List<GameObject>();

    public int nextNode = 0;
    public int numWalkableNodes = 0;

    public float moveSpeed = 10.0f;

    public bool canSearch = true;
    public bool pathFound = false;
    public bool targetReached;


    void Start() {

        nodeList = GameObject.Find("Level").GetComponent<BuildMap>().nodeList;
        for(var i = 0; i < nodeList.Count; i++) {
            if(nodeList[i].GetComponent<Node>().walkable) {
                walkableNodeList.Add(nodeList[i]);
            }
        }
        numWalkableNodes = walkableNodeList.Count;
        transform.GetComponent<Pathfinding>().enabled = true;
        targetReached = true;
    }


    void OnTriggerEnter(Collider other) {

        previousNode = other.gameObject;
    }


    void Update() {

        // SetNewTarget();
        if(targetReached) {
            SetAutomatedTarget();
        }
        if(targetNode) {
            if(canSearch) {
                transform.GetComponent<Pathfinding>().targetNode = targetNode;
                closedList = transform.GetComponent<Pathfinding>().Pathfinder(previousNode);
                MoveToTarget();
                canSearch = false;
            }

            if(!targetReached) {
                MoveToTarget();

                if(transform.position == closedList[closedList.Count - 1].transform.position) {
                    targetReached = true;
                    closedList.Clear();
                }
            }
        }
    }


    void MoveToTarget() {

        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, closedList[nextNode].transform.position, step);
        transform.LookAt(closedList[nextNode].transform.position);

        if(transform.position == closedList[nextNode].transform.position) {
            nextNode++;
        }
    }


    void SetNewTarget() {

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Input.GetMouseButtonDown(1)) {
            if(Physics.Raycast(ray, out hit)) {
                if(hit.collider.gameObject != previousNode) {
                    if(hit.collider.tag == "Node" && hit.collider.gameObject.GetComponent<Node>().walkable) {
                        nextNode = 0;
                        targetReached = false;
                        canSearch = true;
                        targetNode = hit.collider.gameObject;
                    }
                }
            }
        }
    }

    void SetAutomatedTarget() {

        int randomNode = Mathf.RoundToInt(Random.value * numWalkableNodes);
        nextNode = 0;
        targetReached = false;
        canSearch = true;
        targetNode = walkableNodeList[randomNode];
    }
}
