using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float speed;
    public Text scoreText;
    public Text countText;
    public Text winText;

    private Rigidbody rb;
    private int score;
    private int count;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count += 1;
            score += 1;
            SetCountText();
        }
        else if (other.gameObject.CompareTag("Pick Up (Bad)"))
        {
            other.gameObject.SetActive(false);
            count += 1;
            score -= 1;
            SetCountText();
        }
        else if (other.gameObject.CompareTag("Obstacle (Bad)"))
        {
            score -= 1;
            SetCountText();
        }
    }

    private void SetCountText()
    {
        scoreText.text = "Score: " + score.ToString();
        countText.text = "Pick Ups: " + count.ToString();
        if (GameObject.FindGameObjectsWithTag("Pick Up").Length == 0)
        {
            winText.text = "Nice job! You finished with a score of: " + score.ToString();
        }
    }
}
