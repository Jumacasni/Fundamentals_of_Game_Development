using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

    private Rigidbody rb;
    public float speed;
    private GameObject player;
    private float positionPlayer;
    private float positionShot;

	void Start () {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
        player = GameObject.Find("Player");
        positionPlayer = player.transform.position.z;
        positionShot = rb.transform.position.z;	}

    void Update()
    {
        if(rb.transform.position.z > positionPlayer + 30f)
        {
            Destroy(this.gameObject);
        }
    }
}
