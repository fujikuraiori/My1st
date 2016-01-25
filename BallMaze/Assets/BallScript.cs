using UnityEngine;
using System.Collections;

public class BallScript : MonoBehaviour
{

    private Rigidbody rigidbody;

    // Use this for initialization
    void Start()
    {
        rigidbody = this.gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody>().WakeUp();
    }
}

