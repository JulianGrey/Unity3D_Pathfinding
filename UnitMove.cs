using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitMove : MonoBehaviour {

    public GameObject previousNode;
    public GameObject targetNode;

    public List<GameObject> closedList = new List<GameObject>();
    public List<GameObject> nodeList = new List<GameObject>();

    public int nextCell = 0;

    public float moveSpeed = 3.0f;

    public bool canSearch = true;
    public bool pathFound = false;
    public bool targetReached = false;


    void Start() {

        nodeList = GameObject.Find("Level").GetComponent<BuildMap>().nodeList;
        transform.GetComponent<Pathfinding>().enabled = true;
    }


    void OnTriggerEnter(Collider other) {

        previousNode = other.gameObject;
    }


    void Update() {

        SetNewTarget();
        if(targetNode) {
            if(canSearch) {
                if(!previousNode) {
                    closedList = transform.GetComponent<Pathfinding>().Pathfinder(GameObject.Find("Level").GetComponent<GameControl>().startNode);
                }
                else {
                    targetReached = false;
                    closedList = transform.GetComponent<Pathfinding>().Pathfinder(previousNode);
                    MoveToTarget();
                }
                canSearch = false;
            }

            if(!targetReached) {
                MoveToTarget();

                if(transform.position == closedList[closedList.Count - 1].transform.position) {
                    targetReached = true;
                    closedList.Clear();
                    nextCell = 0;
                }
            }
        }
    }


    void MoveToTarget() {

        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, closedList[nextCell].transform.position, step);
        transform.LookAt(closedList[nextCell].transform.position);

        if(transform.position == closedList[nextCell].transform.position) {
            nextCell++;
        }
    }


    void SetNewTarget() {

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Input.GetMouseButtonDown(1)) {
            if(Physics.Raycast(ray, out hit)) {
                if(hit.collider.tag == "Node" && hit.collider.gameObject.GetComponent<Node>().walkable) {
                    nextCell = 0;
                    canSearch = true;
                    targetNode = hit.collider.gameObject;
                }
            }
        }
    }
}