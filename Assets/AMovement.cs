using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AMovement : MonoBehaviour
{
    Rigidbody2D rb2d;
    float maxVelocity = 5;
    RaycastHit2D hit;
    public bool grounded;
    public float raycastOffset;
    public float raycastLength;
    public float jumpHeight;
    public float gravityCutOff;
    public float jumpGravity;
    public GameObject calculator;
    Animator playerAnimator;
    SpriteRenderer playerSpriteRenderer;
    public int health = 1;
    Text healthText;
    public int score = 0;
    Text scoreText;
    CameraFollow cfScript;
    public bool facingRight = true;
    bool invulnerability = false;
    float attackLength = 0.5f;
    public int padHeight;

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        healthText = GameObject.Find("healthText").GetComponent<Text>();
        scoreText = GameObject.Find("scoreText").GetComponent<Text>();
        cfScript = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) && grounded == true)
        {
            rb2d.AddForce(new Vector2(0, jumpHeight), ForceMode2D.Force);
        }

        if (Input.GetKey(KeyCode.A))
        {
            playerAnimator.SetBool("Running", true);
            transform.localScale = new Vector2(-1, 1);
            rb2d.velocity = new Vector2(Mathf.Clamp(rb2d.velocity.x, -maxVelocity, 0), rb2d.velocity.y);
            rb2d.AddForce(new Vector2(-0.5f, 0), ForceMode2D.Impulse);
            facingRight = false;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            playerAnimator.SetBool("Running", true);
            transform.localScale = new Vector2(1, 1);
            rb2d.velocity = new Vector2(Mathf.Clamp(rb2d.velocity.x, 0, maxVelocity), rb2d.velocity.y);
            rb2d.AddForce(new Vector2(0.5f, 0), ForceMode2D.Impulse);
            facingRight = true;
        }
        else
        {
            playerAnimator.SetBool("Running", false);
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        }

        Debug.DrawRay(transform.position - new Vector3(0, raycastOffset), new Vector2(0, raycastLength), Color.blue);
        hit = Physics2D.Raycast(transform.position - new Vector3(0, raycastOffset), Vector2.down, raycastLength);

        if (Physics2D.Raycast(transform.position - new Vector3(0, raycastOffset), Vector2.down, raycastLength))
        {
            if (hit.transform.tag == "Floor")
            {
                grounded = true;
                playerAnimator.SetBool("Grounded", true);
            }
        }
        else
        {
            grounded = false;
            playerAnimator.SetBool("Grounded", false);
        }


        if (grounded == false && rb2d.velocity.y <= gravityCutOff)
        {
            Physics2D.gravity = new Vector2(0, jumpGravity);
        }
        else
        {
            Physics2D.gravity = new Vector2(0, -9.81f);
        }



        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(attack());
        }



        healthText.text = health.ToString();
        scoreText.text = score.ToString();

        if (health < 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Waffle")
        {
            Destroy(other.gameObject);
            health++;
        }

        if (other.tag == "ScoreItem")
        {
            Destroy(other.gameObject);
            score++;
        }

        if (other.name == "Pad")
        {
             rb2d.AddForce(new Vector2(0, padHeight), ForceMode2D.Force);
        }

        if (other.name == "Gojo")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if (other.name == "Dress")
        {
            Destroy(other.gameObject);
            health+=2;
            playerAnimator.SetBool("Dress", true);
            jumpHeight += 50;
            attackLength = 1;
        }

        /*
        if (other.tag == "Cockroach" || other.tag == "Oldman")
        {
            health--;
        }*/
    }

    private IEnumerator attack()
    {
        calculator.SetActive(true);
        yield return new WaitForSeconds(attackLength);
        calculator.SetActive(false);

    }

    public void damage()
    {
        if (invulnerability == false)
        {
            StartCoroutine(hurt());
        }
    }

    private IEnumerator hurt()
    {
        health--;
        invulnerability = true;
        yield return new WaitForSeconds(2);
        invulnerability = false;

    }
}
