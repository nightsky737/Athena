using UnityEngine;
using System.Collections.Generic;

public class PlayerScript: MonoBehaviour 
{
    private Rigidbody2D body;

    private int xleft = -215;
    private int xright = -85;
    private int ytop = 1000;
    private int ybottom = -25;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpheight;
    [SerializeField] private float dashSpeed;
    private float timer = 0;
    private float lastDashed = 0;
    private float lastMoved = 0;
    private float dashCoolDown = 2;
    private int orientation = -1; //-1 is left

    public Sprite LeftSkin;
    public Sprite RightSkin;
    public Sprite RightSleep;
    public Sprite LeftSleep;

    public SpriteRenderer spriteRenderer;

    public LogicScript logic;

    bool canJump = true;
    //awake is part of monobehaviour (base class of script that u can attach to random shit)
    //awake calls when its being loaded
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {   
        if (collision.gameObject.CompareTag("Floor")) // Assuming your floor has the "Floor" tag
        {
            canJump = true;
        }
        if (collision.gameObject.CompareTag("Emotion")) // Assuming your floor has the "Floor" tag
        {   
            logic.collect(collision.gameObject.name.ToLower());
            Destroy(collision.gameObject);

        }
        
    }



    void OnCollisionExit2D(Collision2D collision)
{   
    if (collision.gameObject.CompareTag("Floor"))
    {
      canJump = false;
    }
}
    private void Update()
    {   
        if(transform.position.x < xleft || transform.position.x > xright || 
        transform.position.y < ybottom || transform.position.y > ytop)
        {
            transform.position = new Vector3(-150, 0, 0);
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position = transform.position + (Vector3.left * moveSpeed);
            orientation = -1;
            spriteRenderer.sprite = LeftSkin;

        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.position = transform.position + (Vector3.right * moveSpeed);
            orientation = 1;
            spriteRenderer.sprite  = RightSkin;
            lastMoved = timer;


        }
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))  && canJump)
        {
        body.linearVelocity = new Vector2(body.linearVelocity.x, body.linearVelocity.y + jumpheight);
        }

        if (Input.GetKeyDown(KeyCode.Space) && timer - lastDashed >dashCoolDown)
        {
        body.linearVelocity = new Vector2(body.linearVelocity.x  + dashSpeed * orientation, body.linearVelocity.y);
        // transform.position = transform.position + (Vector3.right * dashSpeed * orientation);
        lastMoved = timer;
        }
        timer = timer + ((float) Time.deltaTime);
        if(timer - lastMoved > 2)
        {
            if(orientation == 1)
            {
                spriteRenderer.sprite = RightSleep;
            }
            else
            {
                spriteRenderer.sprite = LeftSleep;
            }
        }


    }
}
