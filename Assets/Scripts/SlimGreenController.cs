using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimGreenController : MonoBehaviour
{
    public Transform destino;
    public float speed = 0.3f;
    private Vector3 startPos, endPos;
    private SpriteRenderer flipEnemy;
    private float movementx;

    void Start()
    {
        startPos = transform.position;
        endPos = destino.position;
        flipEnemy = GetComponent<SpriteRenderer>();
        
    }

    void Update()
    {
        transform.position = Vector3.Lerp(startPos, endPos, Mathf.PingPong(Time.time * speed, 1.0f));
        
        // ----rotation enemy----       
        if (transform.position.x < destino.position.x +0.1f)
        {
            flipEnemy.flipX = true;
        }
        else if (transform.position.x > startPos.x - 0.1f)
        {
            flipEnemy.flipX = false;
        }
       
        
        


        


    }

      
}
