using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float speed;
    public Text scoreText;
    public Text countText;
    public Text winText;
    public GameObject level1;
    public GameObject level2;

    private Rigidbody rb;
    private Renderer rd;
    private Transform tf;
    private int score;
    private int count;
    private int scoreFinal;
    private bool finished;

    private int currentLevel;
    private int totalGoodPickups;
    private int level2_Requirement;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        rd = GetComponent<Renderer>();
        tf = GetComponent<Transform>();
        score = 0;
        count = 0;
        SetCountText();
        winText.text = "";
        finished = false;

        currentLevel = 1;
        totalGoodPickups = GameObject.FindGameObjectsWithTag("Pick Up").Length;
        level2_Requirement = 12;
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

        Color newColor = new Color(tf.position.x / 10, tf.position.y / 10, tf.position.z / 10, 1);
        rd.material.color = newColor;
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
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacle (Bad)"))
        {
            score -= 1;
            SetCountText();
        }
    }

    private void SetCountText()
    {
        scoreText.text = "Score: " + score.ToString();
        countText.text = "Pick Ups: " + count.ToString();
        if (GameObject.FindGameObjectsWithTag("Pick Up").Length == (totalGoodPickups - level2_Requirement))
        {
            nextLevel();
        }
        else if (GameObject.FindGameObjectsWithTag("Pick Up").Length == 0)
        {
            if (finished == false)
            {
                scoreFinal = score;
            }
            finished = true;
            winText.text = "Nice job! You finished with a score of: " + scoreFinal.ToString();
        }
    }

    private void nextLevel()
    {
        currentLevel += 1;
        switch (currentLevel)
        {
            case 1:
                rb.position = level1.transform.position + Vector3.up;
                break;
            case 2:
                rb.position = level2.transform.position + Vector3.up;
                break;
            default:
                //*shrug*
                break;
        }
    }
}
