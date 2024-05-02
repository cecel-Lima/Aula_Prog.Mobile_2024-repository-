using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KenController : MonoBehaviour
{
    public float walkSpeed = 1.0f;
    private bool _isGrounded = true;

    Animator animator;
    Rigidbody2D rigid;

    //flags para indicar o estado do personagem
    bool _isPlaying_crouch = false;
    bool _isPlaying_walk = false;
    bool _isPlaying_hadooken = false;

    //animation states
    const int STATE_IDLE = 0;
    const int STATE_WALK = 1;
    const int STATE_CROUCH = 2;
    const int STATE_JUMP = 3;
    const int STATE_HADOOKEN = 4;

    string _currentDirection = "left";
    int _currentAnimationState = STATE_IDLE;

    void Start()
    {
        //pega o componente Animator
        animator = this.GetComponent<Animator>();

        //pega o rigidbody2d
        rigid = GetComponent<Rigidbody2D>();
    }

    //FixedUpdate usado no lugar do Update para tratar aspectos de física do pulo
    void FixedUpdate()
    {
        //verifica se usa o teclado - barra de espaço
        if (Input.GetKeyDown(KeyCode.Space))
        {
            changeState(STATE_HADOOKEN);
        }
        else if (Input.GetKey("up") && !_isPlaying_hadooken && !_isPlaying_crouch)
        {
            if (_isGrounded)
            {
                _isGrounded = false;
                //pulo simples
                rigid.AddForce(new Vector2(0f, 250f));
                changeState(STATE_JUMP);
            }
        }
        else if (Input.GetKey("down"))
        {
            changeState(STATE_CROUCH);
        }
        else if (Input.GetKey("right") && !_isPlaying_hadooken)
        {
            changeDirection("right");
            transform.Translate(Vector3.right * walkSpeed * Time.deltaTime);

            if (_isGrounded)
                changeState(STATE_WALK);
        }
        else if (Input.GetKey("left") && !_isPlaying_hadooken)
        {
            changeDirection("left");
            transform.Translate(Vector3.left * walkSpeed * Time.deltaTime);

            if (_isGrounded)
                changeState(STATE_WALK);
        }
        else
        {
            if (_isGrounded)
                changeState(STATE_IDLE);
        }

        //verifica se a animacao de agachar está tocando
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("ken_crouch"))
            _isPlaying_crouch = true;
        else
            _isPlaying_crouch = false;

        //verifica se a animacao de hadouken está tocando
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("ken_hadooken"))
            _isPlaying_hadooken = true;
        else
            _isPlaying_hadooken = false;

        //verifica se a animacao de andar está tocando
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("ken_walk"))
            _isPlaying_walk = true;
        else
            _isPlaying_walk = false;
    }

    /*-------------------------------
        Altera o estado de animação
    --------------------------------*/
    void changeState(int state)
    {
        if (_currentAnimationState == state)
            return;

        switch (state)
        {
            case STATE_WALK:
                animator.SetInteger("state", STATE_WALK);
                break;

            case STATE_CROUCH:
                animator.SetInteger("state", STATE_CROUCH);
                break;

            case STATE_JUMP:
                animator.SetInteger("state", STATE_JUMP);
                break;

            case STATE_IDLE:
                animator.SetInteger("state", STATE_IDLE);
                break;

            case STATE_HADOOKEN:
                animator.SetInteger("state", STATE_HADOOKEN);
                break;
        }

        _currentAnimationState = state;
    }

    /*-------------------------------------------
     * verifica se o ken está colidindo com o chão
     * -----------------------------------------*/
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.name == "Floor")
        {
            _isGrounded = true;
            changeState(STATE_IDLE);
        }
    }

    /*--------------------------------------
     * altera o lado que o personagem está se movendo - flip
     * -----------------------------------*/
    void changeDirection(string direction)
    {
        if (_currentDirection != direction)
        {
            if (direction == "right")
            {
                transform.Rotate(0f, 180f, 0f);
                _currentDirection = "right";
            }
            else if (direction == "left")
            {
                transform.Rotate(0f, -180f, 0f);
                _currentDirection = "left";
            }
        }
    }
}
