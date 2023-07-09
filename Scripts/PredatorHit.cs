using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredatorHit : MonoBehaviour
{

    // Systeme des hit des prédateurs

    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();

    }

    private void OnTriggerEnter2D(Collider2D other){
        /*if(other.CompareTag("wall")){
            StartCoroutine(DecalWall(other));
        }*/

        if(other.CompareTag("hitable")){                  
            StartCoroutine(AttackPray(other));                   // SI c'est une proie il la tue et gagne un peu de vie
        }
        if(other.CompareTag("Player")){
            StartCoroutine(AttackPlayer(other));                 // SI c'est le player il lui enleve 10 hp et gagne un peu de vie aussi
        }
    }

    private IEnumerator DecalWall(Collider2D other){
        Vector3 dist = other.GetComponent<Transform>().position - this.GetComponent<Transform>().position;
        /*Debug.Log(dist);
        Debug.Log(dist.normalized.x*-1);
        Debug.Log(dist.normalized.y*-1);*/
        Debug.Log("True");
        this.GetComponent<PredatorMovement>().SetChange(dist.normalized.x*-1,dist.normalized.y*-1);
        yield return new WaitForSeconds(1.2f);
    }

    private IEnumerator AttackPray(Collider2D other){
        animator.SetBool("attacking",true);
        other.GetComponent<PrayMovement>().HitPred();
        this.GetComponent<PredatorMovement>().AddLife(5);
        yield return null;
        animator.SetBool("attacking",false);
        yield return new WaitForSeconds(.3f);
    }

    private IEnumerator AttackPlayer(Collider2D other){
        if(other.GetComponent<PlayerHealth>().isInvincible){}
        else{
            animator.SetBool("attacking",true);
            other.GetComponent<PlayerHealth>().TakeDamage(20);
            this.GetComponent<PredatorMovement>().AddLife(5);
            yield return null;
            animator.SetBool("attacking",false);
            yield return new WaitForSeconds(.3f);

        }
    }
}
