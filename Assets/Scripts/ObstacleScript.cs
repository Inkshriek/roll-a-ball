using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour {

    private Rigidbody rb;
    public bool movingRight;
    public int speed;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (movingRight == true)
        {
            rb.MovePosition(transform.position + transform.right * (Time.deltaTime * speed));
        }
        else
        {
            rb.MovePosition(transform.position - transform.right * (Time.deltaTime * speed));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Flip Obstacle"))
        {
            movingRight = !movingRight;
        }
    }
}
