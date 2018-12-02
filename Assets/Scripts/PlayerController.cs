using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Lane {Left, Center, Right}
public enum MoveDirection {Left, Right, None}

public class PlayerController : MonoBehaviour {

    private Rigidbody rb;
    private float speed;
    public float jumpForce;
    private bool isGrounded;
    private Animator anim;
    private bool isDead;
    private Lane currentLane;
    public GameObject shot;
    public Transform shotSpawn;
    private float fireRate;
    private float nextFire;
    private GameController gameController;
    private bool invincibility;
    private float timer;
    public AudioSource[] audioSource;

    private Vector3 firstPosition;
    private Vector3 lastPosition;
    private float dragDistance;

    void Start () {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        anim.Play("Running");
        speed = 5f;
        isGrounded = true;
        rb.velocity = new Vector3(0.0f, 0.0f, speed);
        isDead = false;
        currentLane = Lane.Center;
        invincibility = false;
        fireRate = 0.5f;
        dragDistance = Screen.height * 15 / 100;

        GameObject gameControllerObject = GameObject.Find("GameController");
        if(gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }

        else
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }

    void Update()
    {
        if (!isDead)
        {
            if (invincibility)
            {
                if (timer <= 10f)
                {
                    timer += Time.deltaTime;
                }

                else
                {
                    anim.Play("Running");
                    invincibility = false;
                    timer = 0f;
                }
            }

            if (Input.GetKeyDown("up") && isGrounded)
            {
                rb.AddForce(0f, jumpForce, 0f, ForceMode.Impulse);
                anim.Play("Jumping");
                audioSource[2].Play();
            }

            if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                Instantiate(shot, new Vector3(rb.transform.position.x, rb.transform.position.y + 0.5f, rb.transform.position.z), shotSpawn.rotation); ;
                audioSource[0].Play();
            }

            MoveDirection requestedMoveDirection = MoveDirection.None;

            if (Input.GetKeyDown("left"))
            {
                requestedMoveDirection = MoveDirection.Left;
            }

            else if (Input.GetKeyDown("right"))
            {
                requestedMoveDirection = MoveDirection.Right;
            }

            switch (requestedMoveDirection)
            {
                case MoveDirection.Right:
                    if (currentLane == Lane.Right)
                    {
                        break;
                    }

                    else if (currentLane == Lane.Center)
                    {
                        rb.MovePosition(new Vector3(1.3f, rb.transform.position.y, rb.transform.position.z));
                        currentLane = Lane.Right;
                    }

                    else
                    {
                        rb.MovePosition(new Vector3(0.0f, rb.transform.position.y, rb.transform.position.z));
                        currentLane = Lane.Center;
                    }

                    break;

                case MoveDirection.Left:
                    if (currentLane == Lane.Left)
                    {
                        break;
                    }

                    else if (currentLane == Lane.Center)
                    {
                        rb.MovePosition(new Vector3(-1.3f, rb.transform.position.y, rb.transform.position.z));
                        currentLane = Lane.Left;
                    }

                    else
                    {
                        rb.MovePosition(new Vector3(0.0f, rb.transform.position.y, rb.transform.position.z));
                        currentLane = Lane.Center;
                    }

                    break;
            }

            /*if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);

                if(touch.phase == TouchPhase.Began)
                {
                    firstPosition = touch.position;
                    lastPosition = touch.position;
                }

                else if(touch.phase == TouchPhase.Moved)
                {
                    lastPosition = touch.position;
                }

                else if(touch.phase == TouchPhase.Ended)
                {
                    MoveDirection requestedMoveDirection = MoveDirection.None;
                    lastPosition = touch.position;

                    if(Mathf.Abs(lastPosition.x - firstPosition.x) > dragDistance || Mathf.Abs(lastPosition.y - firstPosition.y) > dragDistance)
                    {
                        if(Mathf.Abs(lastPosition.x - firstPosition.x) > Mathf.Abs(lastPosition.y - firstPosition.y))
                        {
                            if(lastPosition.x > firstPosition.x)
                            {
                                requestedMoveDirection = MoveDirection.Right;
                            }

                            else
                            {
                                requestedMoveDirection = MoveDirection.Left;
                            }

                            switch (requestedMoveDirection)
                            {
                                case MoveDirection.Right:
                                    if (currentLane == Lane.Right)
                                    {
                                        break;
                                    }

                                    else if (currentLane == Lane.Center)
                                    {
                                        rb.MovePosition(new Vector3(1.3f, rb.transform.position.y, rb.transform.position.z));
                                        currentLane = Lane.Right;
                                    }

                                    else
                                    {
                                        rb.MovePosition(new Vector3(0.0f, rb.transform.position.y, rb.transform.position.z));
                                        currentLane = Lane.Center;
                                    }

                                    break;

                                case MoveDirection.Left:
                                    if (currentLane == Lane.Left)
                                    {
                                        break;
                                    }

                                    else if (currentLane == Lane.Center)
                                    {
                                        rb.MovePosition(new Vector3(-1.3f, rb.transform.position.y, rb.transform.position.z));
                                        currentLane = Lane.Left;
                                    }

                                    else
                                    {
                                        rb.MovePosition(new Vector3(0.0f, rb.transform.position.y, rb.transform.position.z));
                                        currentLane = Lane.Center;
                                    }

                                    break;
                            }
                        }

                        else
                        {
                            if(lastPosition.y > firstPosition.y && isGrounded)
                            {
                                rb.AddForce(0f, jumpForce, 0f, ForceMode.Impulse);
                                anim.Play("Jumping");
                                audioSource[2].Play();
                            }
                        }
                    }

                    else
                    {
                        nextFire = Time.time + fireRate;
                        Instantiate(shot, new Vector3(rb.transform.position.x, rb.transform.position.y + 0.5f, rb.transform.position.z), shotSpawn.rotation); ;
                        audioSource[0].Play();
                    }
                }
            }*/
        }

        if (isDead)
        {
            audioSource[1].Play();
            anim.Play("Death");
            rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
            gameController.GameOver();
        }
    }

    void OnCollisionEnter(Collision collision)
    { 
        if (collision.rigidbody.CompareTag("Hurdle") || collision.rigidbody.CompareTag("Enemy"))
        {
            if (!invincibility)
            {
                isDead = true;
            }

            else
            {
                Destroy(collision.gameObject);
                rb.velocity = new Vector3(0.0f, 0.0f, speed);
            }
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.rigidbody.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

        void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            gameController.AddScore(10);
            audioSource[3].Play();
        }

        if (other.gameObject.CompareTag("ScoreMultiplier"))
        {
            other.gameObject.SetActive(false);
            audioSource[5].Play();
            gameController.AddScore(gameController.GetScore());
        }

        if (other.gameObject.CompareTag("Invincibility"))
        {
            other.gameObject.SetActive(false);
            anim.Play("Invincibility");
            audioSource[4].Play();
            invincibility = true;
        }
    }

    public void AddSpeed()
    {
        speed += 0.2f;
        rb.velocity = new Vector3(0.0f, 0.0f, speed);
    }
}
