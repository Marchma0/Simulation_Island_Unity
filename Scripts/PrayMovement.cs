using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrayMovement : MonoBehaviour
{

    // Fonction régissant la vie des proies

    public float speed;
    private Rigidbody2D myRigidbody;
    private Vector3 change;

    public float REPROD_PROB;
    public int MAX_PREY=30;
    public int MIN_PREY=1;

    public int life;
    public int cycle;
    public bool isInvincible;
    
    public SpriteRenderer graphics;

    public GameObject mother;
    private int nbrPrey;
    private int nbrTree;


    private Animator animator;


    void Start(){
        life=2;
        cycle=0;
        REPROD_PROB = 0.4f;
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>(); 
        graphics = GetComponent<SpriteRenderer>();
        nbrPrey = GameObject.FindGameObjectsWithTag("hitable").Length;
        nbrTree = GameObject.FindGameObjectsWithTag("tree").Length;

        change = Vector3.zero;
        StartCoroutine(WalkAndReproduce());
    }

    IEnumerator WalkAndReproduce(){                                         // Marche aléatoire + Proba de se reproduire + Méchanisme anti extinction
        while(true){
        float rdm = Random.Range(0.0f,1.0f);
        nbrPrey = GameObject.FindGameObjectsWithTag("hitable").Length;
        nbrTree = GameObject.FindGameObjectsWithTag("tree").Length;

        if(rdm<0.3 || nbrPrey==1){
            change.x = 0.0f;
            change.y = 0.0f;
            rdm = Random.Range(0.0f,1.0f);
            if(rdm<REPROD_PROB && nbrPrey <MAX_PREY && nbrTree>5){
                yield return new WaitForSecondsRealtime(2);
                Reproduce();
            }
            if(nbrPrey==1 && Random.Range(0.0f,1.0f)<0.9 && nbrTree>5){
                yield return new WaitForSecondsRealtime(2);
                Reproduce();
            }
        }
        else{
            change.x = Random.Range(-1.0f,1.0f);
            change.y = Random.Range(-1.0f,1.0f);
        }
        cycle++;
        if(cycle>=20){
            StartCoroutine(OneShot());
        }
        yield return new WaitForSecondsRealtime(2);
        }
    }



    void Update(){
        myRigidbody.velocity = Vector2.zero;                                // Eviter le bug de velocité
        UpdateAnimationAndMove();
    }

    void UpdateAnimationAndMove(){                                          // Meme système que pour le player
        if(change != Vector3.zero){
            MoveCharacter();
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving",true);
        }else{
            animator.SetBool("moving",false);
        }
    }

    void Reproduce(){                                                       // Fonction de reproduction
        GameObject go = GameObject.Instantiate(mother);
        go.transform.position = transform.position;
    }

    void SetChange(float f){
        change.x=f;
        change.y=f;
    }

    void MoveCharacter(){                                                   // Meme système que pour le player
        myRigidbody.MovePosition(       
            transform.position + change * speed * Time.deltaTime
        );
    }

    public void Hit(){                                                      //Quand on Hit une proie une fois elle prends des dégats et
        if(isInvincible){                                                   //devient invincible une courte période
            isInvincible=false;
            return;
        }
        if(life<=0){
            FindObjectOfType<Inventory>().IncrementPray();                  // Si il meurt on l'ajoute a l'inventaire du player
            Destroy(this.gameObject);
        }
        else{
            life-=1;
            isInvincible = true;
            StartCoroutine(InvincibiltyHit());
            StartCoroutine(HandleHit());            
        }
    }

    public void HitPred(){                                                  // Contrairement au player les prédateurs oneshot les proies
        life=0;
        StartCoroutine(OneShot());

    }

    public IEnumerator InvincibiltyHit(){                                   // Tout ce qui est animation...
        while(isInvincible){
            graphics.color = new Color(1f,0f,0f,1f);
            yield return new WaitForSeconds(0.2f);
            graphics.color = new Color(1f,1f,1f,1f);
            yield return new WaitForSeconds(0.2f);
        }
    }

    public IEnumerator OneShot(){
        graphics.color = new Color(1f,0f,0f,1f);
        yield return new WaitForSeconds(0.2f);
        graphics.color = new Color(1f,1f,1f,1f);
        yield return new WaitForSeconds(0.2f);
        this.gameObject.tag="dead";
        Destroy(this.gameObject);    
    }

    public IEnumerator HandleHit(){
        yield return new WaitForSeconds(0.5f);
        isInvincible=false;
    }
}
