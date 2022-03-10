using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireItemController : MonoBehaviour
{
    public static int shoots;
    public Text magicShootsText;
    private PlayerController playerScript;

    void Start()
    {
        playerScript = FindObjectOfType<PlayerController>();
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {

            while (shoots < 10){
                shoots += 10;
                playerScript.FireItemSound();
                playerScript.actualMana += 10f;
                if (shoots > 10)
                {
                    shoots = 10;
                    playerScript.actualMana = 10f;

                }
                //Debug.Log("Fire Balls " + points);
                magicShootsText.text = "MAGIC SHOOTS: " + shoots.ToString();
                Destroy(gameObject);
            }
            
        }
    }


}
