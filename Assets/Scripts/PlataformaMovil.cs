using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaMovil : MonoBehaviour
{
    public Transform destino;
    public float speed = 0.30f;
    public Vector3 startPos,endPos;
   
    void Start()
    {
        startPos = transform.position;
        endPos = destino.position;

    }

    void Update()
    {
        transform.position = Vector3.Lerp(startPos,endPos,Mathf.PingPong(Time.time*speed,1.0f));
        /*
        transform.position = Vector3.MoveTowards(transform.position,destino.position,speed*Time.deltaTime);
        if(transform.position==destino.position){
            destino.position = (destino.position == startPos) ? endPos : startPos;
        } 
        */
    }

    private void OnCollisionEnter2D(Collision2D collision){
        collision.transform.parent = transform;
    }
    private void OnCollisionExit2D(Collision2D collision){
        collision.transform.parent = null;
    }
}
