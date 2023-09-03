using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerMovement : MonoBehaviour
{
    /*private Player player;*/
    private Rigidbody2D body;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    private Animator animator;
    private bool isGrounded;
    private bool gravitySwitch = false;
    private BoxCollider2D collider2D;
    private bool top;
    private bool isDead;
    private Vector3 respawnPoint;

    [SerializeField] private Text totalScore;
    [SerializeField] private GameObject fallDetector;
    [SerializeField] private GameObject topFallDetector;
    [SerializeField] private HealthBar healthBar;

    public GameManager gameManager;
    
/*    public Transform groundCheck;
*/  [SerializeField] public LayerMask groundLayer;
/*    [SerializeField] public LayerMask wallLayer;*/

    // Start is called before the first frame update
    private void Start()
    {
        /*player = GetComponent<Player>();*/
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        respawnPoint = transform.position;
        totalScore.text = "Score: " + Score.score;
    }

    // Update is called once per frame
    private void Update()
    {
/*        GroundedCheck();*/

        if (Health.totHealth <= 0 && !isDead)
        {
            isDead = true;
            gameManager.GameOver();
        }
        float horizontalVelocity = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(horizontalVelocity * speed, body.velocity.y);
        body.velocity = movement;

        if (horizontalVelocity > 0.01) 
        {
            transform.localScale = new Vector3((float)0.35, (float)0.35, (float)0.35);
        }
        else if (horizontalVelocity < -0.01)
        {
            transform.localScale = new Vector3((float)-0.35, (float)0.35, (float)0.35);
        }

        if(Input.GetKey(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
        if (Input.GetKey(KeyCode.X) && isGrounded)
        {
            Physics2D.gravity = new Vector2(0, -9.8f);
            
        }
         if (Input.GetKey(KeyCode.Z) && isGrounded)
        {
            Physics2D.gravity = new Vector2(0, 9.8f);
            isGrounded = false;
            //body.gravityScale *= -1;
            //Rotate();
        }
    

        //Animator
        animator.SetBool("walk", horizontalVelocity != 0);
        animator.SetBool("grounded", isGrounded);

        //FallDetection
        fallDetector.transform.position = new Vector2(transform.position.x, fallDetector.transform.position.y);

        //TopFallDetection
        topFallDetector.transform.position = new Vector2(transform.position.x, topFallDetector.transform.position.y);
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jumpForce);
        isGrounded = false;
        animator.SetTrigger("jump");
    }
   
     private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }
    private void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.tag == "FallDetector")
        {
            Physics2D.gravity = new Vector2(0, -9.8f);
            body.gravityScale = 1;
            body.velocity = Vector3.zero;
            transform.position = respawnPoint;
            healthBar.Damage(0.25f);
        }
        else if (collision.tag == "CheckPoint")
        {
            respawnPoint = transform.position;
        }
        else if (collision.tag == "NextLvl")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            respawnPoint = transform.position;
        }
        else if (collision.tag == "PrevLvl")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            respawnPoint = transform.position;
        }
        else if (collision.tag == "Crystal")
        {
            Score.score += 1;
            totalScore.text = "Score: " + Score.score;
            collision.gameObject.SetActive(false);
        }
        else if (collision.tag == "Spike")
        {
            healthBar.Damage(0.25f);
        }
        else if (collision.tag == "YouWin")
        {
            gameManager.YouWin();
        } 
    }

    private void Rotate()
    {
        if (top == false)
        {
            transform.eulerAngles = new Vector3(0, 0, 180f);
        }
        else
        {
            transform.eulerAngles = Vector3.zero; 
        }
        top = !top;
    }
/*    private void GroundedCheck()
    {
        RaycastHit2D raycast = Physics2D.BoxCast(collider2D.bounds.center, collider2D.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycast != null;
    }*/
}
