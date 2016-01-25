using UnityEngine;
using System.Collections;

public class PusherScript : MonoBehaviour {
    private Vector3 origin;
	// Use this for initialization
    void Awake() {
        origin = GetComponent<Transform>().position;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
	    Vector3 offset = new Vector3(0,0,Mathf.Sin(Time.time)*0.5f);
        GetComponent<Rigidbody>().MovePosition(origin + offset);
	}
}
