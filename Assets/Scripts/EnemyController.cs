using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    private Rigidbody rb;
    private bool isDead;
    private Animator anim;
    private bool rotate;
    private float lastRotation;
    private float lastSpeed;
    private GameObject player;
    private AudioSource audioSource;

    void Start ()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent <Rigidbody>();
        rb.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 90f, 0.0f));
        rb.velocity = new Vector3(2f, 0.0f, 0.0f);
        isDead = false;
        rotate = false;
        anim = GetComponent<Animator>();
        lastRotation = 270f;
        lastSpeed = -2f;
        player = GameObject.Find("Player");
    }
	
	void Update ()
    {
        if(this.transform.position.z + 5 < player.transform.position.z)
        {
            Destroy(this.gameObject);
        }

        if (rotate)
        {
            rb.transform.rotation = Quaternion.Euler(new Vector3(0.0f, lastRotation, 0.0f));
            rb.velocity = new Vector3(lastSpeed, 0.0f, 0.0f);
            rotate = false;

            if (lastRotation == 270f)
                lastRotation = 90f;

            else
                lastRotation = 270f;

            if (lastSpeed == -2f)
                lastSpeed = 2f;

            else
                lastSpeed = -2f;
        }

        if (isDead)
        {
            audioSource.Play();
            anim.Play("Death");
            Destroy(this.gameObject, 0.5f);
        }
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody.CompareTag("Bolt"))
        {
            isDead = true;
            Destroy(collision.gameObject);
        }

        if (collision.rigidbody.CompareTag("Hurdle"))
        {
            Destroy(this.gameObject);
        }

        if (collision.rigidbody.CompareTag("Fence"))
        {
            rotate = true;
        }
    }
}
