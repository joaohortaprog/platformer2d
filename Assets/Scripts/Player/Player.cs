using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Runtime.ExceptionServices;

public class Player : MonoBehaviour
{
    public Rigidbody2D myRigidBody;

    [Header("Movement setup")]
    public Vector2 friction = new Vector2(.1f, 0);
    public float speed;
    public float speedRun;
    public float forceJump = 2f;

    [Header("Animation setup")]
    public float jumpScaleY = 1.5f;
    public float jumpScaleX = .75f;
    public float animDuration = .3f;
    public Ease ease = Ease.OutBack;

    [Header("Animation Player")]
    public string boolRun = "Run";
    public Animator animator;
    public float playerSwipeDuration = .1f;


    private float _currentSpeed;
    private bool _isRunning = false;


    private void Update()
    {
        HandleJump();
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _currentSpeed = speedRun;
            animator.speed = 2;
        }

        else
        {
            _currentSpeed = speed;
            animator.speed = 1;
        }
            

        //_isRunning = Input.GetKey(KeyCode.LeftControl);


        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //myRigidBody.MovePosition(myRigidBody.position - velocity * Time.deltaTime);
            myRigidBody.velocity = new Vector2(-_currentSpeed, myRigidBody.velocity.y);
            if(myRigidBody.transform.localScale.x != -0.5f)
            {
                myRigidBody.transform.DOScaleX(-0.5f, playerSwipeDuration);
            }

            animator.SetBool(boolRun, true);

            //Forma alternativa de fazer verificação condicional em uma única linha
            //myRigidBody.velocity = new Vector2(Input.GetKey(KeyCode.LeftControl) ? -speed : -speedRun, myRigidBody.velocity.y);


        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //myRigidBody.MovePosition(myRigidBody.position + velocity * Time.deltaTime);
            myRigidBody.velocity = new Vector2(_currentSpeed, myRigidBody.velocity.y);
            if (myRigidBody.transform.localScale.x != 0.5f)
            {
                myRigidBody.transform.DOScaleX(0.5f, playerSwipeDuration);
            }


            animator.SetBool(boolRun, true);


            //Forma alternativa de fazer verificação condicional em uma única linha
            //myRigidBody.velocity = new Vector2(Input.GetKey(KeyCode.LeftControl) ? speed : speedRun, myRigidBody.velocity.y);


        }
        else
        {
            animator.SetBool(boolRun, false);

            myRigidBody.velocity = new Vector2(0, myRigidBody.velocity.y);
        }


        if (myRigidBody.velocity.x > 0)
        {
            myRigidBody.velocity += friction;
        }
        else if (myRigidBody.velocity.x < 0)
        {
            myRigidBody.velocity -= friction;

        }

    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myRigidBody.velocity = Vector2.up * forceJump;
            myRigidBody.transform.localScale = Vector2.one;

            DOTween.Kill(myRigidBody.transform);

            HandleJumpScale();
        }
        
    }

    private void HandleJumpScale()
    {
        myRigidBody.transform.DOScaleY(jumpScaleY, animDuration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
        myRigidBody.transform.DOScaleX(jumpScaleX, animDuration).SetLoops(2, LoopType.Yoyo).SetEase(ease);
    }
}
