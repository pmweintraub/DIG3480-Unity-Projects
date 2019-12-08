using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioClip musicClipThree;
    public AudioClip musicClipFour;
    public AudioClip musicClipFive;
    public AudioClip musicClipSix;
    public float speed;
    public Text winText;
    public Text scoreText;
    public Text livesText;
    public Text timeText;
    public GameObject player;


    public float timeLeft;

    private int lives;

    private Rigidbody2D rd2d;
    private bool facingRight = true;
    public static int score;
    Animator anim;




    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        winText.text = "";
        timeLeft = 90;
        score = 0;
        lives = 3;
        SetScoreText();
        SetLivesText();
    }

    void Update()
    {
        SetTimeText();
        if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetInteger("State", 1);
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetInteger("State", 0);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            
            anim.SetInteger("State", 1);
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetInteger("State", 0);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            anim.SetInteger("State", 1);
        }

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            anim.SetInteger("State", 0);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            anim.SetInteger("State", 1);
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            anim.SetInteger("State", 0);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            anim.SetInteger("State", 2);
        }

        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            anim.SetInteger("State", 0);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            anim.SetInteger("State", 2);
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            anim.SetInteger("State", 0);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

 


    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);
            score = score + 1;
            SetScoreText();
            musicSource.clip = musicClipThree;
            musicSource.Play();
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            lives = lives - 1;
            SetLivesText();
            musicSource.clip = musicClipFour;
            musicSource.Play();
        }
        if(other.gameObject.CompareTag("Powerup"))
        {
            other.gameObject.SetActive(false);
            musicSource.clip = musicClipSix;
            musicSource.Play();
            GetComponent<SpriteRenderer>().color = new Color(0f, 1f, 0f);
            StartCoroutine(PuTime(5.0f));
            Physics2D.IgnoreLayerCollision(9, 10, true);

        }
        if (lives == 0)
        {
            Destroy(player);
        }
    }

    IEnumerator PuTime(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f);
        Physics2D.IgnoreLayerCollision(9, 10, false);
    }





    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
                musicSource.clip = musicClipFive;
                musicSource.Play();
            }
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
                musicSource.clip = musicClipFive;
                musicSource.Play();
            }
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
            {
                anim.SetInteger("State", 0);
            }
        }
    }



    void SetScoreText()
    {
        if (score == 4)
        {
            player.transform.position = new Vector2(-79f, 1f);
            lives = 3;
            SetLivesText();
        }
        scoreText.text = "Score: " + score.ToString();
        if (score >= 8)
        {
            winText.text = "You win! Game created by Paulina Weintraub!";
        }
    }

    void SetTimeText()
    {
        if (score != 8)
        {
            timeLeft -= Time.deltaTime;
            timeText.text = "Time: " + timeLeft.ToString("F0");
            if (timeLeft < 0)
            {
                Destroy(player);
                winText.text = "You lose! Game created by Paulina Weintraub!";
            }
        }
        if (score >= 8)
        {
            timeText.text = "Time: " + timeLeft.ToString("F0");
            timeLeft = 90;
        }
        
    }
 

    void SetLivesText()
    {
        livesText.text = "Lives: " + lives.ToString();
        if (lives <= 0)
        {
            winText.text = "You lose! Game created by Paulina Weintraub!";
        }
    }
}
