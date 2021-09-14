using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float speed, jumpSpeed;
    [SerializeField] private LayerMask ground;

    private PlayerActionControls playerActionControls;

    private Rigidbody2D rb;
    private Collider2D col;

    private Animator animator;

    private SpriteRenderer spriteRenderer;

    private AudioClip deathSound;

    private AudioSource audiosource;

    private bool isAlive;
    




    private void Awake()
    {
        playerActionControls = new PlayerActionControls();
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audiosource = GetComponent<AudioSource>();
       
    }

  

    private void OnEnable()
    {
        playerActionControls.Enable();
        
    }



    private void OnDisable()
    {
        playerActionControls.Disable();
    }



    void Start()
    {
        playerActionControls.Land.Jump.performed += _ => Jump();
        deathSound = Resources.Load<AudioClip>("DeathFail8Bit");
        isAlive = true;
    }


    void Update()
    {

        Move();


    }



    private void Move()
    {
        //read the movement value
        float movementInput = playerActionControls.Land.Move.ReadValue<float>();

        //move the player
        Vector3 currentPosition = transform.position;
        currentPosition.x += movementInput * speed * Time.deltaTime;

        transform.position = currentPosition;


        //animation
        if (movementInput != 0)
        {
            animator.SetBool("Run", true);

        }
        else
        {
            animator.SetBool("Run", false);

        }

        //sprite flip
        if (movementInput == -1)
        {
            spriteRenderer.flipX = true;

        }
        else if (movementInput == 1)
        {
            spriteRenderer.flipX = false;
        
        }

    }


    private void Jump()
    {
        if (isGrounded())
        {
            rb.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);

            //animation
            animator.SetTrigger("Jump");
        
        
        }
    
    
    }

    private bool isGrounded()
    {
        Vector2 topLeftPoint = transform.position;
        topLeftPoint.x -= col.bounds.extents.x;
        topLeftPoint.y += col.bounds.extents.y;

        Vector2 bottomRight = transform.position;
        bottomRight.x += col.bounds.extents.x;
        bottomRight.y -= col.bounds.extents.y;

        return Physics2D.OverlapArea(topLeftPoint, bottomRight, ground);
    
    
    }



    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && isAlive)
        {
            isAlive = false;
            
            StartCoroutine(PlaySound());
            
        }
        
    }




    IEnumerator PlaySound()
    {
        Debug.Log("corountine call");
        audiosource.clip = deathSound;
        audiosource.Play();


        playerActionControls.Land.Disable();

        yield return new WaitUntil(() => audiosource.isPlaying == false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        
    
    }







}//end
