using UnityEngine;
using System.Collections;

public class DestroyScript : MonoBehaviour {

	// Use this for initialization
	void OnTriggerEnter(Collider col){
        Destroy(col.gameObject);
	}
}
