using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Penguin : MonoBehaviour
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
    bool isGrounded;
    bool death;
    SpriteRenderer sr;
    Rigidbody2D rb;
    float vert, horiz;
    bool left;
    bool canSAttack;
    private Vector3 jump;

    /*
     * to-do
     * make it so player can die
     * dec. lives once hit (total of 5 lives)
     * fix jumping
     */
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
        player = 1;
        vert = horiz = 0;
        isGrounded = true;
        canSAttack = false;
    }

    IEnumerator specialAttack()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f );
        transform.GetChild(0).gameObject.SetActive(false);

    }
    // Update is called once per frame
    void Update()
    {
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
                    ani.SetBool("attack", attack);
                }
                if (Input.GetKeyUp(KeyCode.E))
                {
                    attack = false;
                    ani.SetBool("attack", attack);
                }
                if (Input.GetKeyDown(KeyCode.R) && canSAttack)
                {
                    sAttack = true;
                    StartCoroutine("specialAttack");
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
                if (Input.GetKeyDown(KeyCode.I) && isGrounded)
                {
                    dir = 2;
                    ani.SetInteger("dir", dir);

                }
                if (Input.GetKeyDown(KeyCode.U))
                {
                    attack = true;
                    ani.SetBool("attack", attack);
                }
                if (Input.GetKeyUp(KeyCode.U))
                {
                    attack = false;
                    ani.SetBool("attack", attack);
                }
                if (Input.GetKeyDown(KeyCode.Y) && canSAttack)
                {
                    sAttack = true;
                    StartCoroutine("specialAttack");
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
            horiz = Mathf.Clamp(horiz - speed, -maxVel, 0);
            Debug.Log("left");
        }
        else if (!left) // right
        {
            horiz = Mathf.Clamp(horiz + speed, 0, maxVel);
        }

        rb.velocity = new Vector2(horiz, vert);

    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("ground"))
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
