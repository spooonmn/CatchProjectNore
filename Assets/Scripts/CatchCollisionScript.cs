using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchCollisionScript : MonoBehaviour
{
    
    public bool isCaught = false;


    void Start()
    {
        
    }

   
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("hit");
        }
    }
}
