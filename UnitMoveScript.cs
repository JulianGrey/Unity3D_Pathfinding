using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitMoveScript : MonoBehaviour {

    public GameObject previousCell;
    
    public List<GameObject> closedList = new List<GameObject>();
    
    public int nextCell = 0;
    
    public float moveSpeed = 3.0f;
    
    public bool pathFound = false;
    public bool targetReached = false;


    void Start() {

        closedList = transform.GetComponent<Pathfinding>().closedList;
    }


    void OnTriggerEnter(Collider other){

        previousCell = other.gameObject;
    }


    void Update() {
        
        transform.GetComponent<Pathfinding>().Pathfinder();
        if(transform.position == closedList[closedList.Count - 1].transform.position) {
            targetReached = true;
        }
        else {
            targetReached = false;
        }

        if(!targetReached) {
            MoveUnit();
        }
    }


    void MoveUnit() {

        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, closedList[nextCell].transform.position, step);
        transform.LookAt(closedList[nextCell].transform.position);

        if(transform.position == closedList[nextCell].transform.position) {
            nextCell++;
        }
    }
}