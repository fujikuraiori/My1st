using UnityEngine;
using System.Collections;

public class GameRoot : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    for(int i=0;i<4;i++){
            for (int j = 0; j <= i;j++ )
            {
                GameObject go = Instantiate(Resources.Load("Pin"))as GameObject;
                go.transform.position = new Vector3(j-i/2.0f,0.0f,4.0f+i);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
