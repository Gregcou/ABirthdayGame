using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    Rigidbody2D rb2d;
    public float moveSpeed;
    public bool oldMan;
    SpriteRenderer sr;
    public Sprite mellowOldMan;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        sr = GetComponent<SpriteRenderer>();
        if (oldMan == false)
        {
            StartCoroutine(jump());
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(-moveSpeed, 0));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Calculator")
        {
            if (oldMan == true)
            {
                sr.sprite = mellowOldMan;
                GetComponent<BoxCollider2D>().isTrigger = false;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<AMovement>().damage();
        }
    }

   

    private IEnumerator jump()
    {
        while(true)
        {
            yield return new WaitForSeconds(2);
            rb2d.AddForce(new Vector2(-50, 300), ForceMode2D.Force);
        }
        
    }


}
