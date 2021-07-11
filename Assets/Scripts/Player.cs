using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    public SpriteRenderer spriteRenderer;
    public int health;
    public float speed;
    public float jumpForce;
    bool isJumping;
    public bool isVulnerable;

    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        //retorna uma direção no eixo x com valor entre -1 (esquerda) e 1 (direita)
        float direction = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(direction * speed, rb.velocity.y);

        if(direction > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
            
        }

        if(direction < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);//flip em X
            
        }

        if(direction != 0 && isJumping == false)
        {
            anim.SetInteger("transition", 1);//seta o parametro transition em animator para 1 para rodar a animação de run
        }
       
        else if(direction == 0 && isJumping == false) 
        {
            anim.SetInteger("transition", 0);
        }

        Jump();
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isJumping == false)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            anim.SetInteger("transition", 2);
            isJumping = true;
        }
    }

    public void AddDamage()
    {
        if(isVulnerable == false)
        {
            health--;
            isVulnerable = true;
            StartCoroutine(Respawn());
        }
        
    }

    IEnumerator Respawn()
    {
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.enabled = true;
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.enabled = true;
        yield return new WaitForSeconds(0.5f);
        isVulnerable = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            isJumping = false;
        }
    }
}
