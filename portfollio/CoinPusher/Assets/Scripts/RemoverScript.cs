using UnityEngine;
using System.Collections;

public class RemoverScript : MonoBehaviour {
    public GameObject uScore;
    void OnTriggerEnter(Collider col)
    {
        Destroy(col.gameObject);
        uScore.SendMessage("CountUp");
    }
}
