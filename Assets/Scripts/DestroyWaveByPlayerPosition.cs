using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWaveByPlayerPosition : MonoBehaviour {

    private GameObject player;

    void Start()
    {
        player = GameObject.Find("Player"); 
    }

    void Update()
    {
        if(this.transform.position.z + this.GetComponent<MeshRenderer>().bounds.size.z < player.transform.position.z)
        {
            Destroy(this.gameObject);
        }  
    }       
}
