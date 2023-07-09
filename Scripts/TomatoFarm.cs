using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomatoFarm : MonoBehaviour
{

    //  Systeme de la ferme

    public SpriteRenderer sprite;
    public Transform trans;

    public int life;

    public bool collectable;
    public bool isHere;
    public bool IsRaining;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        trans = GetComponent<Transform>();

        trans.localScale = new Vector3(0.1f,0.1f,1f);
        sprite.color = new Color(0.5f,0.5f,0f);
        isHere=true;
        collectable=false;
        StartCoroutine(Life());
    }

    void Update()
    {
        
    }

    private IEnumerator Life(){
        while(true){                                                                            // pousse si elles sont la 
            if(isHere && !collectable){
                for(int i=0;i<50;i++){
                    sprite.color = new Color(0.5f+i/100.0f,0.5f+i/100.0f,2.0f*i/100.0f);
                    trans.localScale = new Vector3(0.1f+0.02f*i,0.1f+0.02f*i,1f);
                    yield return new WaitForSeconds(0.1f);
                }
                collectable=true;
            }
            yield return new WaitForSeconds(0.1f);

        }
    }

    public void Collect(){
        if(collectable){                                                                        //sont collecté puis désactiver
            isHere=false;
            collectable=false;
            sprite.color = new Color(1f,1f,1f,0f);
            FindObjectOfType<Inventory>().IncrementTomato(2);
        }
        else{                                                                                   //sont plantés donc réactivé
            if(!IsRaining && FindObjectOfType<Inventory>().GetTomato()>0){
                trans.localScale = new Vector3(0.1f,0.1f,1f);
                sprite.color = new Color(0.5f,0.5f,0f,1f);
                FindObjectOfType<Inventory>().IncrementTomato(-1);
                isHere=true;
                collectable=false;
            }
        }
    }

    public void SetRaining(bool b){
        IsRaining=b;
    }
}
