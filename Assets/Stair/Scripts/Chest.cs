using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private float chestHealth;
    private Animator _animator;


    private void OnTriggerStay2D(Collider2D other)
    {
        var collideObject = other.gameObject;
        Debug.Log("hit");
        if (collideObject.CompareTag("Player"))
        {
            var characterObject = collideObject.GetComponent<Character>();
            StartCoroutine(AttackChest(characterObject));
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

    IEnumerator AttackChest(Character character)
    {
        if (character.IsAttacking())
        {
            float damage = character.gameObject.GetComponent<Character>().GetCharacterDamage();
            DamageChest(damage);
            yield return new WaitForSeconds(1.5f);
        }
    }
}
