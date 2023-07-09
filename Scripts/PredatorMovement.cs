using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PredatorState{
    walk,
    attack,
    rompish
}


public class PredatorMovement : MonoBehaviour
{
    public PredatorState currentState;
    public Transform targetTransform;
    public GameObject[] targetsObj;


    public float speed;
    public int pv;
    public float chaseRadius;
    public float attackRadius;
    public bool isChasing;
    public float REPROD_PROB;
    public float ROMPISH_PROB;

    private Rigidbody2D myRigidbody;
    public SpriteRenderer graphics;

    private Vector3 change;

    public GameObject mother;

    private Animator animator;


    // Start is called before the first frame update
    void Start(){
        targetsObj = GameObject.FindGameObjectsWithTag("hitable");
        isChasing =false;

        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>(); 
        graphics = GetComponent<SpriteRenderer>();

        change = Vector3.zero;
        StartCoroutine(life());
    }

    IEnumerator life(){                                     //SERT A MAJ LE VECTEUR CHANGE NOTAMENT POUR CHASSER OU S'ASSOIRE/REPRODUIRE OU BOUGER
        while(true){
        isChasing=false;
        float rdm = Random.Range(0.0f,1.0f);
        if(rdm>0.1 && targetsObj.Length!=0){
            Chase();
            targetsObj = GameObject.FindGameObjectsWithTag("hitable");
        }
        if(!isChasing){
            rdm = Random.Range(0.0f,1.0f);
            if(rdm<ROMPISH_PROB){
                currentState = PredatorState.rompish;
                change.x = 0.0f;                            //         AJOUTER 
                change.y = 0.0f;                            //    UNE VITESSE CONSTANTE          
                rdm = Random.Range(0.0f,1.0f);
                if(rdm<REPROD_PROB){
                    yield return new WaitForSecondsRealtime(2);
                    currentState = PredatorState.walk;
                    Reproduce();
                }
            }
            else{
                currentState = PredatorState.walk;
                change.x = Random.Range(-1.0f,1.0f);
                change.y = Random.Range(-1.0f,1.0f);
            }
        }
        pv-=1;
        yield return new WaitForSecondsRealtime(2);
        }
        
    }



    void Update(){
        myRigidbody.velocity = Vector2.zero;               //eviter le bug de velocité
        UpdateAnimationAndMove();
        Dead();
    }

    void UpdateAnimationAndMove(){                          // anim...
        if(change != Vector3.zero){
            MoveCharacter();
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving",true);
        }else{
            animator.SetBool("moving",false);
        }
    }

    void Reproduce(){
        GameObject go = GameObject.Instantiate(mother);
        go.transform.position = transform.position;
        go.GetComponent<PredatorMovement>().SetLife(8);
    }

    void SetLife(int v){
        pv=v;
    }

    public void AddLife(int v){
        if(pv+v>15){
            pv=15;
        }
        else{
            pv=pv+v;
        }
    }

    public void SetChange(float x, float y){
        change.x = x;
        change.y = y;
    }

    void Dead(){
        if(pv<=0){
            StartCoroutine(AnimDead());
        }
    }

    IEnumerator AnimDead(){
        animator.SetBool("moving",false);
        yield return new WaitForSeconds(0.2f);
        graphics.color = new Color(1f,0f,0f,1f);
        yield return new WaitForSeconds(0.2f);
        graphics.color = new Color(1f,1f,1f,1f);
        yield return new WaitForSeconds(0.2f);
        this.gameObject.SetActive(false); 
    }

    void Chase(){                                                           //Fonction de chasse selon le rayon de chasse et son bon vouloir de chasse (proba)
        targetsObj = GameObject.FindGameObjectsWithTag("hitable");
        foreach(GameObject target in targetsObj){
            targetTransform = target.transform;
            if(Vector3.Distance(targetTransform.position, transform.position) <= chaseRadius && Vector3.Distance(targetTransform.position, transform.position) >= attackRadius && target.activeSelf){
                Vector3 dist = targetTransform.position - transform.position;
                change.x = dist.normalized.x;
                change.y = dist.normalized.y;
                currentState = PredatorState.attack;
                isChasing=true;
                break;
            }
        }
    }

 
    void MoveCharacter(){
        myRigidbody.MovePosition(
            transform.position + change * speed * Time.deltaTime
        );
    }
}