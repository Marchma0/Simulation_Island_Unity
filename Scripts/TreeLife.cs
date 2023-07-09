using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeLife : MonoBehaviour
{

    public SpriteRenderer graphics;

    public bool isBurning;

    public bool burned=false;

    void Start()
    {
        isBurning =false;

        graphics = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(anim());
    }

    private void OnTriggerEnter2D(Collider2D other){
        StartCoroutine(ForestFire(other));
    }

    private IEnumerator ForestFire(Collider2D other){                             // Propagation du feu                   
        while(true){
            if(isBurning){
                yield return new WaitForSeconds(2f);
                if(other.CompareTag("tree")){
                    other.GetComponent<TreeLife>().SetFire(true);
                }
                yield return new WaitForSeconds(2.5f);
                this.gameObject.SetActive(false);
            }
        yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator anim(){                                                     // Animation simple pour le feu 
        if(isBurning){
            for(int i=0; i<4;i++){ //effet clignotant
                graphics.color = new Color(1f,0.9f,0f,1f);
                yield return new WaitForSeconds(0.5f);
                graphics.color = new Color(1f,1f,1f,1f);
                yield return new WaitForSeconds(0.5f);
            }
            graphics.color = new Color(0f,0f,0f,1f);
            yield return new WaitForSeconds(1.0f);

            burned=true;
            isBurning=false;
            this.gameObject.SetActive(false);

        }

    }

    public void SetFire(bool b){
        isBurning =b;
    }

    public void Grow(){
        if(burned){
            this.gameObject.SetActive(true); 
            isBurning=false;      
        }
    }


}
