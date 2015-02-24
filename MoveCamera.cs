using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {
    public GameObject player;
    private Vector3 distance = new Vector3(0, 20.0f, -15.0f);


    void Update() {
        transform.position = player.transform.position + distance;
        transform.rotation = Quaternion.Euler(60.0f, 0, 0);
    }
}