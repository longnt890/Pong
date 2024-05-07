using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallMovement : MonoBehaviour
{

    [SerializeField] private float initialSpeed = 10;
    [SerializeField] private float speedIncrement = 0.25f;
    [SerializeField] private Text playerScore;
    [SerializeField] private Text AIScore;

    private int hitCounter;
    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Invoke("StartBall", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        rb.velocity =  Vector2.ClampMagnitude(rb.velocity, initialSpeed + (speedIncrement * hitCounter));
    }

    private void StartBall()
    {
        rb.velocity = new Vector2(-1,0) * (initialSpeed + speedIncrement * hitCounter);
    }

    private void ResetBall()
    {
        rb.velocity = new Vector2(0, 0);
        transform.position = new Vector2(0, 0);
        hitCounter = 0;
        Invoke("StartBall", 2f);
    }

    private void PlayerBounce(Transform myObject)
    {
        hitCounter++;

        Vector2 ballPos = transform.position;
        Vector2 playerPos = myObject.position;

        float xDirections, yDirections;

        if(transform.position.x > 0)
        {
            xDirections = -1;
        }
        else
        {
            xDirections = 1;
        }

        yDirections = (ballPos.y - playerPos.y) / myObject.GetComponent<Collider2D>().bounds.size.y;

        if(yDirections == 0)
        {
            yDirections = 0.25f;
        }

        rb.velocity = new Vector2(xDirections, yDirections) * (initialSpeed + speedIncrement * hitCounter);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Player" || collision.gameObject.name == "AI")
        {
            PlayerBounce(collision.transform);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (transform.position.x > 0)
        {
            ResetBall();
            playerScore.text = (int.Parse(playerScore.text) + 1).ToString();
        }else if(transform.position.x < 0)
        {
            ResetBall();
            AIScore.text = (int.Parse(AIScore.text) + 1).ToString();
        }
    }

}
