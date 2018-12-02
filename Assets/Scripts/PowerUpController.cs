using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour {

    void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody.CompareTag("Hurdle"))
        {
            Destroy(this.gameObject);
        }
    }
}
