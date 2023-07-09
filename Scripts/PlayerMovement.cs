using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState{
    walk,
    attack,
    interact
}

public class PlayerMovement : MonoBehaviour
{
    public PlayerState currentState;
    public float speed;
    private Rigidbody2D myRigidbody;
    private Vector3 change;

    private Animator animator;

    public float health;

    void Start(){
        currentState = PlayerState.walk;
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();   

    }

    void Update(){
        myRigidbody.velocity = Vector2.zero;                        // Eviter le bug de velocité
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        if(Input.GetButtonDown("attack")){                          // Attaque
            StartCoroutine(AttackCo());
        }
        else {if(currentState == PlayerState.walk ){                // Bouge
            UpdateAnimationAndMove();   
        }}
    }

    private IEnumerator AttackCo(){                                 // Lance l'animation d'attaque, avec le booléen attacking
        animator.SetBool("attacking",true);
        currentState = PlayerState.attack;
        yield return null;
        animator.SetBool("attacking",false);
        yield return new WaitForSeconds(.3f);
        currentState = PlayerState.walk;
    }

    void UpdateAnimationAndMove(){                                  // Lance les animations "walk", notament en fournissant a l'animator le vecteur change
        if(change != Vector3.zero){
            MoveCharacter();
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving",true);
        }else{
            animator.SetBool("moving",false);
        }
    }



    void MoveCharacter(){                                           // Fait bouger le perso
        myRigidbody.MovePosition(
            transform.position + change * speed * Time.deltaTime
        );
    }
}
