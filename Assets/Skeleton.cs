using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Internal;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    // Start is called before the first frame update
    Animator ani;
    bool attack;
    bool sAttack;
    int dir;
    public int player;
    public float speed;
    public float maxVel;
    public float jumpForce;
    public GameObject slime;
    float timer;
    bool isGrounded;
    bool death;
    SpriteRenderer sr;
    Rigidbody2D rb;
    float vert, horiz;
    bool left;
    bool canSAttack;
    private Vector3 jump;
    void Start() 
    {
        ani = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        dir = 0;
        attack = false;
        sAttack = false;
        left = false;
        death = false;
        player = 2;
        vert = horiz = 0;
        timer = 0;
        isGrounded = true;
        canSAttack = false;
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (!death)
        {
            if (player == 1)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    dir = 1;
                    sr.flipX = true;
                    ani.SetInteger("dir", dir);
                    left = true;
                }
                if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.W))
                {
                    dir = 0;     
                    ani.SetInteger("dir", dir);
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    sr.flipX = false;
                    dir = 1;
                    ani.SetInteger("dir", dir);
                    left = false;
                }
                if (Input.GetKeyDown(KeyCode.W) && isGrounded)
                {
                    dir = 2;
                    ani.SetInteger("dir", dir);
                 
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    attack = true;
                }
                if (Input.GetKeyUp(KeyCode.E))
                {
                    attack = false;
                }
                if (Input.GetKeyDown(KeyCode.R) && canSAttack)
                {
                    sAttack = true;
                    ani.SetBool("sAttack", sAttack);
                    ani.SetBool("attack", false);
                    canSAttack = false;

                }
                if (Input.GetKeyUp(KeyCode.R))
                {
                    sAttack = false;
                    ani.SetBool("sAttack", sAttack);
                }

            }
            else if (player == 2)
            {
                if (Input.GetKeyDown(KeyCode.J))
                {
                    dir = 1;
                    sr.flipX = true;
                    ani.SetInteger("dir", dir);
                    left = true;
                }
                if (Input.GetKeyUp(KeyCode.J) || Input.GetKeyUp(KeyCode.L) || Input.GetKeyUp(KeyCode.I))
                {
                    dir = 0;
                    ani.SetInteger("dir", dir);
                }
                if (Input.GetKeyDown(KeyCode.L))
                {
                    sr.flipX = false;
                    dir = 1;
                    ani.SetInteger("dir", dir);
                    left = false;
                }
                if (Input.GetKeyDown(KeyCode.I))
                {
                    dir = 2;
                    ani.SetInteger("dir", dir);
                }
                if (Input.GetKeyDown(KeyCode.U))
                {
                    attack = true;
                    GameObject o = Instantiate(slime, this.transform.position, Quaternion.identity);
                    o.transform.position = new Vector2(o.transform.position.x, o.transform.position.y - 1f);
                    int temp = 1;
                    if (left) temp = -1;
                    o.GetComponent<Rigidbody2D>().velocity = new Vector2(4*temp, 0);
                    Destroy(o, 2);
                }
                if (Input.GetKeyUp(KeyCode.U))
                {
                    attack = false;
                }
                if (Input.GetKeyDown(KeyCode.Y) && canSAttack)
                {
                    sAttack = true;
                    ani.SetBool("sAttack", sAttack);
                    ani.SetBool("attack", false);
                    canSAttack = false;
                }
                if (Input.GetKeyUp(KeyCode.Y))
                {
                    sAttack = false;
                    ani.SetBool("sAttack", sAttack);
                }
            }

        }
    }

    private void FixedUpdate()
    {
        if (dir == 0)
        {
            vert = 0;
            horiz = 0;
        }
        else if (dir == 2) // jump
        {
            vert = jumpForce;
            horiz = rb.velocity.x;
            isGrounded = false;
        }
        else if (left) // left
        {
            vert = 0;
            horiz = Mathf.Clamp(horiz - speed, -maxVel, 0);
        }
        else if (!left) // right
        {
            vert = 0;
            horiz = Mathf.Clamp(horiz + speed, 0, maxVel);
        }
        
        rb.velocity = new Vector2(horiz, vert);

    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag.Equals("ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.gameObject.tag.Equals("ground"))
        {
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Ball"))
        {
            canSAttack = true;
        }
    }
}
