using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public Animator animator;

    public UIManager UIManager;
    public bridgemanager bridgemanager;
    public float speed = 8f;
    public float Xmovment = 1f;
    public float restoredspeed = 8f;
    private Vector3 movevector;
    private float verticalvelocity = 0f;
    private float gravity = 25f;

    public AudioSource audioSource;
    public AudioClip coinclip;
    public AudioSource backgroundsource;
    public AudioClip backgroundclip;

    float force = 0.5f;
    public int unlockMinScore;

    private Vector2 fingerDown;
    private Vector2 fingerUp;
    public bool detectSwipeOnlyAfterRelease = false;
    public float SWIPE_THRESHOLD = 20f;
    /// //////////////////////////////////
    public int currentlane = 0;
    bool canswipe = true;
    public int score;

    void Start()
    {
        audioSource.clip = coinclip;

        backgroundsource.clip = backgroundclip;
        backgroundsource.loop = true;
        backgroundsource.volume = 0.7f;
        backgroundsource.Play();

    }
    void Update()
    {
        movevector = Vector3.zero;

            if (controller.isGrounded)// if i am on the floor
            {
            verticalvelocity = Input.GetAxisRaw("Vertical") * speed * force;
            }
            else
            {
            verticalvelocity -= gravity * Time.deltaTime;
            }
        movevector.y = verticalvelocity;
            //x left -right
            ///////////////////////////////////////////////////////////////////////////////////////////
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                fingerUp = touch.position;
                fingerDown = touch.position;
                canswipe = true;
                }

                //Detects Swipe while finger is still moving
                if (touch.phase == TouchPhase.Moved)
                {
                    if (!detectSwipeOnlyAfterRelease)
                    {
                    fingerDown = touch.position;
                    checkSwipe();
                    }
                }
                //Detects swipe after finger is released
                if (touch.phase == TouchPhase.Ended)
                {
                fingerDown = touch.position;
                checkSwipe();
                }
            }
        movevector.x = currentlane * 20;
        /////////////////////////////////////////////////////////////////////////////////////

        controller.Move(movevector * Time.deltaTime);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -Xmovment, Xmovment), transform.position.y, transform.position.z);
    }
    void OnTriggerEnter(Collider hitInfo)
    {

        if (hitInfo.gameObject.CompareTag("coins"))
        {
            Destroy(hitInfo.gameObject);
            audioSource.Play();
            score++;
            
        }
        if (hitInfo.gameObject.CompareTag("objects"))
        {
            bridgemanager.movenow = false;
            animator.SetBool("hit", true);
            speed = 0f;
            backgroundsource.Pause();
            if (score >= unlockMinScore)
            {
                UnlockLevels();
                Debug.Log("unlocking");
            }
            else
            {
                
                //Gameover
            }
        }
    }
    public void dead()/////////////////////called from the animation event trigger
    {
        FindObjectOfType<UIManager>().deadmenu();
    }
    protected void UnlockLevels()
    {
        string currentLevel = SceneManager.GetActiveScene().name;
        for (int j = 1; j < StartManager.nomberoflevels; j++)
        {
            if (currentLevel == "Level" + j.ToString())
            {
                int levelIndex = (j + 1);
                PlayerPrefs.SetInt("level" + levelIndex.ToString(), 1);
            }
        }
        UIManager.WinGame();
    }
    public void resume()
    {
        backgroundsource.Play();
        bridgemanager.movenow = true;
        animator.SetBool("hit", false);
        movevector.x = 0;
        movevector.y = transform.position.y + 5f;
        speed = restoredspeed;
        animator.Play("1HCombatRunF");
    }
    ///////////////////////////////////////////////////////////pausing//////////////////////////////////////////////////////////
    public void pause()
    {
        backgroundsource.Pause();        
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void checkSwipe()
    {
        //Check if Vertical swipe
        if (verticalMove() > SWIPE_THRESHOLD && verticalMove() > horizontalValMove())
        {
            //Debug.Log("Vertical");
            if (fingerDown.y - fingerUp.y > 0)//up swipe
            {
                OnSwipeUp();
            }
            else if (fingerDown.y - fingerUp.y < 0)//Down swipe
            {
                OnSwipeDown();
            }
            fingerUp = fingerDown;
        }
        //Check if Horizontal swipe
        else if (canswipe = true  && horizontalValMove() > SWIPE_THRESHOLD && horizontalValMove() > verticalMove())
        {
            //Debug.Log("Horizontal");
            if (fingerDown.x - fingerUp.x > 0)//Right swipe
            {
                OnSwipeRight();
                canswipe = false;
            }
            else if (fingerDown.x - fingerUp.x < 0)//Left swipe
            {
                OnSwipeLeft();
                canswipe = false;
            }
            fingerUp = fingerDown;
        }

        //No Movement at-all
        else
        {
            //Debug.Log("No Swipe!");
        }
    }
    float verticalMove()
    {
        return Mathf.Abs(fingerDown.y - fingerUp.y);
    }
    float horizontalValMove()
    {
        return Mathf.Abs(fingerDown.x - fingerUp.x);
    }

    //////////////////////////////////CALLBACK FUNCTIONS/////////////////////////////
    void OnSwipeUp()
    {
        Debug.Log("Swipe UP");
        movevector.y = 15f;
    }
    void OnSwipeDown()
    {
        Debug.Log("Swipe Down");
    }
    void OnSwipeLeft()
    {
        if(currentlane > -1)
        {
            currentlane--;
        }
    }
    void OnSwipeRight()
    {
        if (currentlane < 1)
        {
            currentlane++;
        }
    }
}
