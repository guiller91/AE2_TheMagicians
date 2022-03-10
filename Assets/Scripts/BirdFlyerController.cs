using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdFlyerController : MonoBehaviour
{
    [SerializeField] public Transform player;
    [SerializeField] public float distance;
    public Vector3 startPos, endPos;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        animator = GetComponent<Animator>();
        startPos = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        distance = Vector2.Distance(transform.position, player.position);
        animator.SetFloat("Distance", distance);
    }

    public void flip (Vector3 target)
    {
        if (transform.position.x < target.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
}
