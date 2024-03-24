using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float speed = 300.0f;
    private bool facingRight = true;
    private Rigidbody2D _body;
    private Animator animator;

    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        Vector2 movement1 = new Vector2(deltaX, _body.velocity.y);
        _body.velocity = movement1; 
        float deltaY = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        Vector2 movement2 = new Vector2(_body.velocity.x, deltaY);
        _body.velocity = movement2;
        PlayAnimation(deltaX, deltaY);
    }

    void StopAnimation()
    {
        animator.SetBool("IsDown", false);
        animator.SetBool("IsUp", false);
        animator.SetBool("IsLeft", false);
        animator.SetBool("IsRight", false);
    }

    void PlayAnimation(float moveX, float moveY)
    {
        if (moveX == 0 && moveY == 0)
        {
            StopAnimation();
        }
        else
        {
            if (moveY > 0 && moveX == 0)
            {
                animator.SetBool("IsUp", true);
                animator.SetBool("IsDown", false);
                animator.SetBool("IsLeft", false);
                animator.SetBool("IsRight", false);
            }
            else
            {
                if (moveY < 0 && moveX == 0)
                {
                    animator.SetBool("IsUp", false);
                    animator.SetBool("IsDown", true);
                    animator.SetBool("IsLeft", false);
                    animator.SetBool("IsRight", false);
                }
                else
                {
                    if (moveX > 0)
                    {
                        animator.SetBool("IsUp", false);
                        animator.SetBool("IsDown", false);
                        animator.SetBool("IsLeft", false);
                        animator.SetBool("IsRight", true);
                    }
                    else
                    {
                        animator.SetBool("IsUp", false);
                        animator.SetBool("IsDown", false);
                        animator.SetBool("IsLeft", true);
                        animator.SetBool("IsRight", false);
                    }
                }
            }
        }

    }

    private void horizFlip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
