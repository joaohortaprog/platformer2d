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

    [Header("Jump Collision Check")]
    public Collider2D collider2D;
    public float distToGround;
    public float spaceToGround = .1f;
    public LayerMask groundLayer;
    public ParticleSystem jumpVFX;


    // Guarda a escala original do personagem
    private Vector3 _defaultScale;

    private void Awake()
    {
        if (healthBase != null)
        {
            healthBase.OnKill += OnPlayerKill;
        }

        _currentPlayer = Instantiate(sOPlayerSetup.player, transform);

        if (collider2D != null)
        {
            distToGround = collider2D.bounds.extents.y;
        }
    }

    private bool IsGrounded()
    {
        Vector2 origin = collider2D.bounds.center;
        float rayDistance = collider2D.bounds.extents.y + spaceToGround;

        Debug.DrawRay(
            origin,
            Vector2.down * rayDistance,
            Color.magenta
        );

        RaycastHit2D hit = Physics2D.Raycast(
            origin,
            Vector2.down,
            rayDistance,
            groundLayer
        );

        return hit.collider != null;
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
        IsGrounded();
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
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
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
            PlayJumpVFX();
        }
    }

    private void PlayJumpVFX()
    {
        if(jumpVFX != null) jumpVFX.Play();
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