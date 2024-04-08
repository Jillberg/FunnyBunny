using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;


public class BirdScript : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public float flapStrength;
    public LogicScript logic;
    public PauseMenuScript pauseMenuScript;
    public bool birdIsAlive = true;
    public AudioSource flappingSFX;
    public AudioSource deathSFX;
    public int deadZone = -50;
    public ParticleSystem clouds;
    public AudioSource BGM;
    public Animator animator;
    public Material material;
    private float fade=0;
    private bool isDissolving = false;
    private bool isRespawning;



    // Start is called before the first frame update
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        material=GetComponent<SpriteRenderer> ().material;
        isRespawning=true;
        

    }

    // Update is called once per frame
    void Update()
    {
        Respawning();

        Flapping();

        EscControl();

        Dissolving();

        ExceedBorder();

       // DestroyPlayer();

        PlayAgain();

        BackToMenu();

    }

    private void OnCollisionEnter2D(Collision2D collision)// check if player has hit any pillar; if so, they are dead;
    {
        if (birdIsAlive)
        {
            Debug.Log("collision detected!");
            logic.gameOver();
            Dissolving();
            BGM.Pause();
            deathSFX.Play();
            birdIsAlive = false;
            isDissolving = true;
            animator.SetTrigger("Stunned");
            clouds.Pause();
        }
    }

    private void ExceedBorder()// check if player has exceeded the available area; if so, they are considered dead;
    {
        if ((transform.position.y < -20 || transform.position.y > 20) && birdIsAlive == true)
        {
            Debug.Log("exceed border!");
            logic.gameOver();
            animator.SetTrigger("Stunned");
            clouds.Pause();

            BGM.Pause();
            deathSFX.Play();

            birdIsAlive = false;
        }
    }

    private void Dissolving()//dying effect;
    {
        if (isDissolving)
        {
            fade -= (Time.deltaTime*3/4);
            if (fade < 0f)
            {
                
                isDissolving = false;
               
            }
            material.SetFloat("_Fade", fade);
        }
    }

    private void Respawning()//Respawning effect at the beginning of the game;
    {
        if (isRespawning)
        {
            fade+=(Time.deltaTime );
            if (fade > 1f)
            {

                isRespawning = false;

            }
            material.SetFloat("_Fade", fade);
        }
    }

    public void FlappyingSFX() //Play sound effect if game is not paused;
    {
        if (!pauseMenuScript.gameIsPaused)
        {
            flappingSFX.Play();
        }
    }

    public void PlayAgain() //When dead, can use Space to play again;
    {
        if (Input.GetKeyDown(KeyCode.Space) == true && birdIsAlive == false)
        {
            logic.reStartGame();
        }
    }

    public void BackToMenu()// When dead, can use ESC to return to the title screen;
    {
        if (Input.GetKeyDown(KeyCode.Escape) == true && birdIsAlive == false)
        {
            pauseMenuScript.Home();
        }
    }

    public void DestroyPlayer() //Destroy player game object when falling beyond deadzone; 
    {
       if (transform.position.y <deadZone)
        {
            Thread.Sleep(Convert.ToInt32(1000*(Time.deltaTime+1)));
            Debug.Log("Bird destoyed!");
            Destroy(gameObject);
        }
    }

    private void EscControl()
    {
        if (Input.GetKeyDown(KeyCode.Escape) == true && birdIsAlive == true)
        {
            if (pauseMenuScript.gameIsPaused)
            {
                pauseMenuScript.Resume();
            }
            else
            {
                pauseMenuScript.Pause();
            }
        }
    }

    private void Flapping()
    {
        if (Input.GetKeyDown(KeyCode.Space) == true && birdIsAlive == true)
        {
            animator.SetTrigger("Flying");
            myRigidbody.velocity = Vector2.up * flapStrength;
            FlappyingSFX();
        }
        else
        {
            animator.SetTrigger("ReturnToIdle");
        }
    }
}
