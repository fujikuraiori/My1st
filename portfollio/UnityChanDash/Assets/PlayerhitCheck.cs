using UnityEngine;
using System.Collections;

public class PlayerhitCheck : MonoBehaviour {
    public bool colliderActive = true;
    void OnTriggerStay(Collider col) { 
        if(colliderActive == false){
            return;        
        }
        if(col.name == "Goal"){
            gameObject.SendMessage("OnGoal");
            return;
        }
        bool isHigh = col.name == "High";
        bool isLow = col.name == "Low";
        var stateInfo = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
        bool isRun = stateInfo.IsTag("RUN");
        bool isJump = stateInfo.IsTag("JUMP");
        bool isSlide = stateInfo.IsTag("SLIDE");
        if(isRun == true || (isJump == true && isHigh == true) ||(isSlide && isLow)){
            gameObject.SendMessage("OnDead");
        }
    }
    void OnDead() {
        colliderActive = false;
    }
    void OnGoal() {
        colliderActive = false;
    }
}
