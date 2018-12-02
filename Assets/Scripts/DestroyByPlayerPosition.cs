using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByPlayerPosition : MonoBehaviour {

    private GameObject player;

    void Start()
    {
        player = GameObject.Find("Player"); 
    }

    void Update()
    {
        if(this.transform.position.z + 5 < player.transform.position.z)
        {
            Destroy(this.gameObject);
        }  
    }       
}
