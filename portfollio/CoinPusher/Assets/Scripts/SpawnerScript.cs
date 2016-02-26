using UnityEngine;
using System.Collections;

public class SpawnerScript : MonoBehaviour {
    public GameObject coinPrefab;
    //public GameObject uScore;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetMouseButtonDown(0)){
            Vector3 offset = new Vector3(Mathf.Sin(Time.time*255),0,0);
            Instantiate(coinPrefab,transform.position+offset,transform.rotation);
      //      uScore.SendMessage("CountDown");
        }
	}
}
