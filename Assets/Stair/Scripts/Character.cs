using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Character : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float moveSpeed;
    [SerializeField] private float extraSpeed;
    [SerializeField] private bool isRunning;
    private Animator _animation;
    void Start()
    {
        _animation = GetComponent<Animator>();
        isRunning = false;
    }

    // Update is called once per frame
    void Update()
    {
        CharacterMovement();
        CharacterAttack();
    }

    void CharacterMovement()
    {
        float horizontalAxis = Input.GetAxisRaw("Horizontal");

        
        WalkCharacter(horizontalAxis);
        FlipCharacter(horizontalAxis);
        RunCharacter(horizontalAxis);
        
        
        PlayMoveAnimation(horizontalAxis);
        
    }
    
    
    private void WalkCharacter(float directionForce)
    {
        Vector3 direction = Vector3.zero;
        direction.x = directionForce;
        
        Vector3 translate = direction * (moveSpeed * Time.deltaTime);
        transform.Translate(translate);
    }
    
    private void FlipCharacter(float directionForce)
    {
        if (directionForce > 0 || directionForce < 0)
        {
            transform.localScale = new Vector2(directionForce/2f,0.5f);    
        }
    }

    private void RunCharacter(float directionForce)
    {
        PlayRunAnimation(directionForce);
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            moveSpeed += extraSpeed;
            isRunning = true;
            

        }else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed -= extraSpeed;
            isRunning = false;
        }
    }
    private void PlayMoveAnimation(float directionForce)
    {
        if (directionForce != 0 && !CheckAnimation("kick1"))
        {
            _animation.SetBool("isWalking",true);
        }
        else
        {
            _animation.SetBool("isWalking", false);
        }
    }

    private void PlayRunAnimation(float directionForce)
    {
        if(isRunning && directionForce!=0)
        {
            _animation.SetBool("running",true);
        }
        else
        {
            _animation.SetBool("running",false);
        }
    }
    void CharacterAttack()
    {
        KickAttack1();
        KickAttack2();
    }
    

    private void KickAttack1()
    {
        if (Input.GetKeyDown(KeyCode.J) && !isRunning)
        {
            _animation.SetTrigger("kick1");
        }
    }

    private void KickAttack2()
    {
        if (Input.GetKeyDown(KeyCode.K) && !isRunning)
        {
            _animation.SetTrigger("kick2");
        }
    }
    bool CheckAnimation(string animationName)
    {
        var isAnimationRun = _animation.GetCurrentAnimatorStateInfo(0).IsName(animationName);
        return isAnimationRun;
    }

    
}
