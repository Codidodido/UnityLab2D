using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private float chestHealth;
    private Animator _animator;


    private void OnCollisionEnter2D(Collision2D other)
    {
        
        
            if (other.gameObject.CompareTag("Player"))
            {
                Debug.Log("Character here");
                float damage = other.gameObject.GetComponent<Character>().GetCharacterDamage();
                DamageChest(damage);

            }
        
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.enabled = false;
    }

    void DamageChest(float damage)
    {
        if (chestHealth <= 0)
        {
            PlayAnimation();
        }
        else
        {
            chestHealth -= damage;    
        }
    }

    void PlayAnimation()
    {
        _animator.enabled = true;
        Destroy(gameObject,5f);
    }
}
