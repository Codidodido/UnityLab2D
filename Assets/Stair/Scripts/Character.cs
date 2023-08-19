using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR;

public class Character : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float characterDamage;
    [SerializeField] private float characterMoveSpeed;
    [SerializeField] private float characterExtraSpeed;
    [SerializeField] private float characterJumpForce;
    private bool _characterIsRunning;
    [SerializeField] private bool _characterIsAttacking;
    private Animator _animator;
    
    //Returns 
    public float GetCharacterDamage()
    {
        return characterDamage;
    }
    public bool CheckAnimationBool(string animationName)
    {
        return _animator.GetBool(animationName);
    }

    public string GetCurrentAnimationName()
    {
        var animationName =_animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        return animationName;
    }

    public bool IsAttacking()
    {
        return _characterIsAttacking;
    }
    
    
    //Main
    void Start()
    {
        _animator = GetComponent<Animator>();
        _characterIsRunning = false;
    }

    
    void Update()
    {
        CharacterMovement();
        CharacterAttack();
    }

    
    
    // Actions
    
        //Movement
        
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
                _characterIsRunning = true;
                

            }else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                characterMoveSpeed -= characterExtraSpeed;
                _characterIsRunning = false;
            }
        }
    
        //Attacks
        void CharacterAttack()
        {
            
            KickAttack1();
            KickAttack2();
        }
        private void KickAttack1()
        {
            if(Input.GetKeyDown(KeyCode.J))
            {
                DoAttack(10f,"kick1");    
            }
            
        }
        private void KickAttack2()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                DoAttack(25f,"kick2");    
            }else if (Input.GetKeyUp(KeyCode.K))
            {
                
            }
            
            
        }
        
        void DoAttack(float damage,string name)
        {
            if (!_characterIsRunning && !CheckAnimationBool("isWalking"))
            {
                StartCoroutine(AttackEnum(name));
            }
            
        }

        IEnumerator AttackEnum(string name)
        {
            _characterIsAttacking = true;
                
            
            _animator.SetTrigger(name);
            
            

            yield return new WaitForSeconds(0.03f);
            _characterIsAttacking = false;
        }
    
    //Play Animations
    private void PlayMoveAnimation(float directionForce)
    {
        if (directionForce != 0 && !_characterIsAttacking )
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
        if(_characterIsRunning && directionForce!=0)
        {
            _animator.SetBool("running",true);
        }
        else
        {
            _animator.SetBool("running",false);
        }
    }

}
