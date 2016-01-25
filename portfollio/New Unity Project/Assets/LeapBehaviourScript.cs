using UnityEngine;
using System.Collections;
using Leap;
public class LeapBehaviourScript : MonoBehaviour {

    Controller controller = new Controller();
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        var frame = controller.Frame();
        var finger = frame.Fingers.Frontmost;
        var iBox = frame.InteractionBox;
        if(finger.IsValid){
            var position = iBox.NormalizePoint(finger.TipPosition);
            position *= 10;
            position.x -= 5;
            position.z = (-position.z)+5;
            transform.localPosition = new Vector3(position.x,position.y,position.z);
        }
	}
}
