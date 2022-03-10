using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireBallController : MonoBehaviour
{
    public GameObject explosionPlayer;
    public AudioClip explosion;
    private AudioSource audioSourceFireBall;

    void Start()
    {
        audioSourceFireBall = GetComponent<AudioSource>();
    }

    void Update()
    {
        
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {

            Destroy(other.gameObject);
            Instantiate(explosionPlayer, other.transform.position, Quaternion.identity);
            audioSourceFireBall.PlayOneShot(explosion);
            Destroy(gameObject);
        }

    }
    
}
