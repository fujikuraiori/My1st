using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {
	// Use this for initialization
	void Start () {
	    GetComponent<Animator>().applyRootMotion = false;
        GetComponent<Rigidbody>().freezeRotation = true;
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKey("right"))
        {
            transform.Rotate(0,0.5f,0);
        }
        if (Input.GetKey("left"))
        {
            transform.Rotate(0, -0.5f, 0);
        }
        Vector3 velocity = transform.forward * Time.deltaTime * 3;
        GetComponent<Rigidbody>().MovePosition(transform.position+velocity);
	}
    void OnDead() {
        enabled = false;
    }
    void OnGoal() {
        enabled = false;
    }
}
