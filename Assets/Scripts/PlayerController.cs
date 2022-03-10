using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//biblioteca para la interfaz de usuario
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private float speed = 5f;
    private float movementX;
    private SpriteRenderer flipPlayer;
    private Rigidbody2D playerRb;
    private float jumpForce = 6f;
    private AudioSource audioSourcePlayer;
    public AudioClip fireItemSoundCollect, jumpSound, magicShootSound;
    public GameObject FireBall;
    public GameObject cameraPlayer;
    public GameObject explosionPlayer;
    private Animator animatorPlayer;
    public GameObject panelGameOver, panelWin;
    private BoxCollider2D boxColliderPlayer;
    [SerializeField] private LayerMask playerLayerMask;
    // ManaBar
    public Image manaBar;
    public float actualMana;
    public float manaMax;
    private FireItemController fireItemController;
    // DeadBar
    public Image enemiesBar;
    public Text enemiesKilledText, panelkilledText, panelWinText;
    private float enemiesCreated, currentEnemies, enemiesKilled;
    public Texture2D cursorTexture;
 


    void Start()
    {
       
        flipPlayer = GetComponent<SpriteRenderer>();       
        playerRb = GetComponent<Rigidbody2D>();
        audioSourcePlayer = GetComponent<AudioSource>();
        animatorPlayer = GetComponent<Animator>();
        boxColliderPlayer = GetComponent<BoxCollider2D>();
        fireItemController = FindObjectOfType<FireItemController>();
        enemiesCreated = GameObject.FindGameObjectsWithTag("Enemy").Length;

        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }
  
    void Update()
    {
        // ----enemies bar----
        currentEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        enemiesKilledText.text = "CURRENT ENEMIES: " + currentEnemies.ToString();
        enemiesKilled = enemiesCreated - currentEnemies;
        enemiesBar.fillAmount = enemiesKilled / enemiesCreated;

        // ----Panels win and gameover----
        panelkilledText.text = "Killed Enemies: " + enemiesKilled.ToString();
        if (enemiesKilled == enemiesCreated) panelWin.SetActive(true);
        panelWinText.text = "Killed Enemies: " + enemiesKilled.ToString();


        // ----mana bar----
        manaBar.fillAmount = actualMana / manaMax;

        // ----movement player----
        movementX = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(movementX, 0, 0);
        transform.Translate(movement * Time.deltaTime * speed);
        
        // ----rotation player----
        if (movementX > 0)
        {
            flipPlayer.flipX = false;
            animatorPlayer.SetBool("isWalking", true);
        }
        else if (movementX < 0)
        {
            flipPlayer.flipX = true;
            animatorPlayer.SetBool("isWalking", true);
        }
        else
        {
            animatorPlayer.SetBool("isWalking", false);
        }
        // ----jump player----
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && isGrounded())
        {
            playerRb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            audioSourcePlayer.PlayOneShot(jumpSound);
        }
       
        // ----Simple Attack player----
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (actualMana > 0)
            {
                magicShoot();
            }
        }
        // ----FireBall player----
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (actualMana > 0)
            {
                magicShoot();
            }

        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {       
      
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Muerte del player, fin del juego");       
            cameraPlayer.transform.parent = null;
            Instantiate(explosionPlayer, transform.position + new Vector3(0, 0.4f, 0), Quaternion.identity);
            panelGameOver.SetActive(true);
            Destroy(gameObject);
            actualMana = 0f;
            FireItemController.shoots = 0;
        }

        // spike dead
        if (collision.gameObject.tag == "Spike")
        {
            Debug.Log("Muerte del player, fin del juego");
            cameraPlayer.transform.parent = null;
            Instantiate(explosionPlayer, transform.position + new Vector3(0, 0.4f, 0), Quaternion.identity);
            panelGameOver.SetActive(true);
            Destroy(gameObject);
            actualMana = 0f;
            FireItemController.shoots = 0;
        }

        // Slime dead
        if (collision.gameObject.tag == "Slime")
        {
            Debug.Log("Muerte del player, fin del juego");
            cameraPlayer.transform.parent = null;
            Instantiate(explosionPlayer, transform.position + new Vector3(0, 0.4f, 0), Quaternion.identity);
            panelGameOver.SetActive(true);
            Destroy(gameObject);
            actualMana = 0f;
            FireItemController.shoots = 0;
        }
    }


    private bool isGrounded()
    {
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxColliderPlayer.bounds.center, boxColliderPlayer.bounds.size, 0f, Vector2.down, 0.1f, playerLayerMask);
        return raycastHit2d.collider != null;
       
    }

    public void FireItemSound()
    {
        audioSourcePlayer.PlayOneShot(fireItemSoundCollect);
    }

    public void magicShoot()
    {
        // subtract 1 from HUD magic shot counter
        FireItemController.shoots -= 1;
        fireItemController.magicShootsText.text = "MAGIC SHOOTS: " + FireItemController.shoots.ToString();

        actualMana -= 1f;

        // instanciar el objeto FireBall en el centro del jugador
        GameObject fireBall = Instantiate(FireBall, transform.position + new Vector3(-1, 0, 0), Quaternion.identity);
        fireBall.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 10);
        audioSourcePlayer.PlayOneShot(magicShootSound);

        // fire in the direction of the mouse
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        fireBall.GetComponent<Rigidbody2D>().velocity = direction.normalized * 10;

        // ignore player
        Physics2D.IgnoreCollision(fireBall.GetComponent<Collider2D>(), GetComponent<Collider2D>());

        // destroy after collision with enemy
        Destroy(fireBall, 1.5f);

       


    }


   
}
