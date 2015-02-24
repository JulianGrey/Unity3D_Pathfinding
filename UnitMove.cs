using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitMove : MonoBehaviour {

    private GameObject previousNode;
    private GameObject targetNode;

    private List<GameObject> closedList;
    private List<GameObject> nodeList;
    private List<GameObject> walkableNodeList;

    public int nextNode = 0;
    public int numWalkableNodes;

    public float moveSpeed = 10.0f;

    public bool canSearch = true;
    public bool pathFound = false;
    public bool targetReached;


    void Start() {

        nodeList = GameObject.Find("Level").GetComponent<BuildMap>().nodeList;
        walkableNodeList = GameObject.Find("Level").GetComponent<BuildMap>().walkableNodeList;
        numWalkableNodes = GameObject.Find("Level").GetComponent<BuildMap>().numWalkableNodes;
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
        if(walkableNodeList[randomNode] != targetNode) {
            nextNode = 0;
            targetReached = false;
            canSearch = true;
            targetNode = walkableNodeList[randomNode];
        }
    }
}