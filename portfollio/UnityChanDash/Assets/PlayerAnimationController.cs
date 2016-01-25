using UnityEngine;
using System.Collections;

public class PlayerAnimationController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        bool isDownJunp = Input.GetKeyDown(KeyCode.UpArrow);
        bool isSlidKey = Input.GetKeyDown(KeyCode.DownArrow);
        Animator animator = GetComponent<Animator>();
        animator.SetBool("JUMP",isDownJunp);
        animator.SetBool("SLIDE",isSlidKey);
	}
    void OnDead() {
        GetComponent<Animator>().SetTrigger("DEAD");
        enabled = false;
    }
    void OnGoal() {
        GetComponent<Animator>().SetTrigger("GOAL");
        enabled = false;
    }
}
