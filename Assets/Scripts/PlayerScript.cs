using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    //movement controls
    private Rigidbody2D rd2d;
    public float speed;
    public float jumpForce;

    //game over text 
    public Text gameOver;


    //face checking 
    private bool facingRight = true;

    //public GameObject damageParticlesPrefab;

    //audio
    public AudioSource musicSource;
    public AudioClip backgroundMusic;
    public AudioClip loseMusic;
    public AudioClip winMusic;
   
    public AudioClip collectClip;
    AudioSource audioSource;

    public bool gameOverB = false;

    //score
    public Text score; 
    private int scoreValue;

    //timer
    public float timeLeft = 12.0f;
    public Text timer; // used for showing countdown from 3, 2, 1 


    //ground checker
    private bool isOnGround;
    public Transform groundcheck;
    public float checkRadius;
    public LayerMask allGround;  



    // Start is called before the first frame update
    void Start()
    {
        scoreValue = 0;
        gameOver.text = "";
        SetScoreValue();
        rd2d = GetComponent<Rigidbody2D>();
       
        musicSource.clip = backgroundMusic;
        musicSource.PlayDelayed(2.0f);

        audioSource = GetComponent<AudioSource>();
    }


    //timer
    void Update()
    {
        timeLeft -= Time.deltaTime;
        timer.text = (timeLeft).ToString("Time: 0");
        if (timeLeft > 0 && scoreValue >=5)
        {
            timeLeft += Time.deltaTime;
            timer.text = (timeLeft).ToString("Time: 0");
            speed = 0.0f;
            gameOverB = true;
            jumpForce = 0.0f;
            Destroy(gameOver, 2);
            gameOver.text = "You Win! Press R to Restart \n Game created by Shea Barber";
            if (Input.GetKey(KeyCode.R))
            {
                if (gameOverB == true)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }
        }
        
        if (timeLeft <= 0 && scoreValue < 5) 
        {
            timer.text = (timeLeft).ToString("Time: 0");
            timeLeft = 0;
            speed = 0.0f;
            jumpForce = 0.0f;
            //Destroy(gameObject);
            gameOverB = true;
            Destroy(gameOver, 2);
            gameOver.text = "You Lose! Press R to Restart \n Game created by Shea Barber";
            musicSource.clip = loseMusic;
            musicSource.Play();
            
            if (Input.GetKey(KeyCode.R))
            {
                if (gameOverB == true)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }
        }
    }



    //movement and escape 
    void FixedUpdate()
    {

        

       
       
        
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);

       


        //Flipping player sprite     
        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }

        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }
    }

    //flip sprite
    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }

    //pickups and scoring 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "coin")
        {
            scoreValue +=1;
            SetScoreValue();
            Destroy(collision.collider.gameObject);
            PlaySound(collectClip);   

            //GameObject damageParticles = Instantiate(damageParticlesPrefab, transform.position, Quaternion.identity);
        }   
        
        if (scoreValue >=5)
        {
            musicSource.clip = winMusic;
            musicSource.Play();
        }
    }


    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
        if(timeLeft == 0)
        {
            musicSource.clip = loseMusic;
            musicSource.Play();
        }   
    }


    void SetScoreValue()
    {
        score.text = "Score: " + scoreValue.ToString();
    }
    
  

    //ground checker
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground" && isOnGround)
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
        }
    }
}
