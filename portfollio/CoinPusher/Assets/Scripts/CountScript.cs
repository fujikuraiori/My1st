using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CountScript : MonoBehaviour {
    int score = 30;
    private Text uScore;
	// Use this for initialization
	void Start () {
        uScore = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
        uScore.text = "Score:" + score.ToString();
	}
	
	// Update is called once per frame
	void CountUp () {
        score += 3;
        uScore.text = "Score:" + score.ToString();
	}
}
