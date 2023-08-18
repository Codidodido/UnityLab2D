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
        float horizontalAxis = Input.GetAxis("Horizontal");

        Vector3 direction = Vector3.zero;
        direction.x = horizontalAxis;
        
        Vector3 translate = direction * (moveSpeed * Time.deltaTime);
        transform.Translate(translate);
        PlayMoveAnimation(horizontalAxis);
        RunCharacter(horizontalAxis);
    }

    void CharacterAttack()
    {
        KickAttack1();
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

    private void KickAttack1()
    {
        if (Input.GetKeyDown(KeyCode.J) && !CheckAnimation("running") && !CheckAnimation("isWalking"))
        {
            _animation.SetTrigger("kick1");
        }
    }

    bool CheckAnimation(string animationName)
    {
        var isAnimationRun = _animation.GetCurrentAnimatorStateInfo(0).IsName(animationName);
        return isAnimationRun;
    }
}
