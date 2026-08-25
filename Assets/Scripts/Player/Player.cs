using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Runtime.ExceptionServices;

public class Player : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    public HealthBase healthBase;

    //public Animator animator;

    [Header("Setup")]
    public SOPlayerSetup sOPlayerSetup;

    private float _currentSpeed;
    private bool _isRunning = false;

    private Animator _currentPlayer;


    // Guarda a escala original do personagem
    private Vector3 _defaultScale;

    private void Awake()
    {
        if (healthBase != null)
        {
            healthBase.OnKill += OnPlayerKill;
        }

        _currentPlayer = Instantiate(sOPlayerSetup.player, transform);
    }

    private void OnPlayerKill()
    {
        healthBase.OnKill -= OnPlayerKill;
        _currentPlayer.SetTrigger(sOPlayerSetup.triggerDeath);
    }

    private void Start()
    {
        _defaultScale = myRigidBody.transform.localScale;
    }

    private void Update()
    {
        HandleJump();
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _currentSpeed = sOPlayerSetup.speedRun;
            _currentPlayer.speed = 2;
        }
        else
        {
            _currentSpeed = sOPlayerSetup.speed;
            _currentPlayer.speed = 1;
        }

        //_isRunning = Input.GetKey(KeyCode.LeftControl);

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //myRigidBody.MovePosition(myRigidBody.position - velocity * Time.deltaTime);
            myRigidBody.velocity = new Vector2(-_currentSpeed, myRigidBody.velocity.y);

            if (myRigidBody.transform.localScale.x > 0)
            {
                DOTween.Kill(myRigidBody.transform);

                myRigidBody.transform
                    .DOScaleX(-_defaultScale.x, sOPlayerSetup.playerSwipeDuration)
                    .SetEase(Ease.OutSine);
            }

            _currentPlayer.SetBool(sOPlayerSetup.boolRun, true);

            //Forma alternativa de fazer verificação condicional em uma única linha
            //myRigidBody.velocity = new Vector2(Input.GetKey(KeyCode.LeftControl) ? -speed : -speedRun, myRigidBody.velocity.y);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //myRigidBody.MovePosition(myRigidBody.position + velocity * Time.deltaTime);
            myRigidBody.velocity = new Vector2(_currentSpeed, myRigidBody.velocity.y);

            if (myRigidBody.transform.localScale.x < 0)
            {
                DOTween.Kill(myRigidBody.transform);

                myRigidBody.transform
                    .DOScaleX(_defaultScale.x, sOPlayerSetup.playerSwipeDuration)
                    .SetEase(Ease.OutSine);
            }

            _currentPlayer.SetBool(sOPlayerSetup.boolRun, true);

            //Forma alternativa de fazer verificação condicional em uma única linha
            //myRigidBody.velocity = new Vector2(Input.GetKey(KeyCode.LeftControl) ? speed : speedRun, myRigidBody.velocity.y);
        }
        else
        {
            _currentPlayer.SetBool(sOPlayerSetup.boolRun, false);

            myRigidBody.velocity = new Vector2(0, myRigidBody.velocity.y);
        }

        if (myRigidBody.velocity.x > 0)
        {
            myRigidBody.velocity += sOPlayerSetup.friction;
        }
        else if (myRigidBody.velocity.x < 0)
        {
            myRigidBody.velocity -= sOPlayerSetup.friction;
        }
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myRigidBody.velocity = Vector2.up * sOPlayerSetup.forceJump;

            // Mantém a direção para a qual o personagem está olhando
            float direction = Mathf.Sign(myRigidBody.transform.localScale.x);

            // Restaura a escala original antes de iniciar o squash/stretch
            myRigidBody.transform.localScale = new Vector3(
                _defaultScale.x * direction,
                _defaultScale.y,
                _defaultScale.z);

            DOTween.Kill(myRigidBody.transform);

            HandleJumpScale(direction);
        }
    }

    private void HandleJumpScale(float direction)
    {
        Vector3 jumpScale = new Vector3(
            _defaultScale.x * sOPlayerSetup.jumpScaleX * direction,
            _defaultScale.y * sOPlayerSetup.jumpScaleY,
            _defaultScale.z);

        myRigidBody.transform
            .DOScale(jumpScale, sOPlayerSetup.animDuration)
            .SetLoops(2, LoopType.Yoyo)
            .SetEase(sOPlayerSetup.ease);
    }

    public void DestroyMe()
    {
        Destroy(gameObject);
    }
}