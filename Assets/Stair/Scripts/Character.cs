using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Character : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float characterDamage;
    [SerializeField] private float characterMoveSpeed;
    [SerializeField] private float characterExtraSpeed;
    [SerializeField] private float characterJumpForce;
    [SerializeField] private bool characterIsRunning;
    
    private Animator _animator;
    
    void Start()
    {
        _animator = GetComponent<Animator>();
        characterIsRunning = false;
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
        JumpCharacter(characterJumpForce);
        
        PlayMoveAnimation(horizontalAxis);
        
    }
    
    
    private void WalkCharacter(float directionForce)
    {
        Vector3 direction = Vector3.zero;
        direction.x = directionForce;
        
        Vector3 translate = direction * (characterMoveSpeed * Time.deltaTime);
        transform.Translate(translate);
    }

    private void JumpCharacter(float jumpForce)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector2 characterVelocity = GetComponent<Rigidbody2D>().velocity;
            characterVelocity.y = jumpForce;
            GetComponent<Rigidbody2D>().velocity = characterVelocity;    
        }
        
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
            characterMoveSpeed += characterExtraSpeed;
            characterIsRunning = true;
            

        }else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            characterMoveSpeed -= characterExtraSpeed;
            characterIsRunning = false;
        }
    }
    private void PlayMoveAnimation(float directionForce)
    {
        if (directionForce != 0 && !CheckAnimation("running"))
        {
            _animator.SetBool("isWalking",true);
        }
        else
        {
            _animator.SetBool("isWalking", false);
        }
    }

    private void PlayRunAnimation(float directionForce)
    {
        if(characterIsRunning && directionForce!=0)
        {
            _animator.SetBool("running",true);
        }
        else
        {
            _animator.SetBool("running",false);
        }
    }
    void CharacterAttack()
    {
        KickAttack1();
        KickAttack2();
    }
    

    private void KickAttack1()
    {
        DoAttack(KeyCode.J,10f,"kick1");
    }

    private void KickAttack2()
    {
        
        DoAttack(KeyCode.K,25f,"kick2");
        
    }

    void DoAttack(KeyCode keyCode, float damage,string name)
    {
        if (Input.GetKeyDown(keyCode) && !characterIsRunning)
        {
            try
            {
                _animator.SetTrigger(name);
            }
            catch (Exception e)
            {
                Debug.Log("Animation Doesn't exist");
            }
            characterDamage = damage;
        }
        else if(Input.GetKeyUp(keyCode))
        {
            characterDamage = 0;
        }
        

        
    }
    
    bool CheckAnimation(string animationName)
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName(animationName) &&
            _animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            return true;
        else
            return false;
    }

    public float GetCharacterDamage()
    {
        return characterDamage;
    }
}
