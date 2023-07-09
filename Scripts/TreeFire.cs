using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeFire : MonoBehaviour
{

    // Systeme des feux de forets pondérés par le nombre de prois présents sur le terrains

    public GameObject[] prayTab;
    public GameObject[] treeTab; 

    public int nb_pray;
    public float PROB_FIRE;

    public bool IsRaining=false;

    void Start()
    {
        prayTab = GameObject.FindGameObjectsWithTag("hitable");
        treeTab = GameObject.FindGameObjectsWithTag("tree");

        PROB_FIRE = 0.1f;
        StartCoroutine(ForestFire());
    }

    // Update is called once per frame
    void Update()
    {
        prayTab = GameObject.FindGameObjectsWithTag("hitable");
    }

    private IEnumerator ForestFire(){
        while(true){
            if(IsRaining){                                                                          // Si il pleut, les arbres réparaissent
                    float rdm2 = Random.Range(0.0f,1.0f);
                    if(rdm2<0.7f){
                        int nbTree = Random.Range(0,treeTab.Length);
                        Debug.Log(nbTree);
                        treeTab[nbTree].GetComponent<TreeLife>().gameObject.SetActive(true);
                        treeTab[nbTree].GetComponent<TreeLife>().SetFire(false);
                    }
                    yield return new WaitForSeconds(5.0f);
            } 
            else{                                                                                   // Sinon un arbre brule selon une proba pondéré par le nombre de proie
                float rdm = Random.Range(0.0f,1.0f);
                PROB_FIRE = prayTab.Length/100.0f;
                if(rdm<PROB_FIRE){
                    int nbTree = Random.Range(0,treeTab.Length);
                    Debug.Log(nbTree);
                    treeTab[nbTree].GetComponent<TreeLife>().SetFire(true);
                }
                    yield return new WaitForSeconds(4.0f);
            }
               
       }
    }


    public void NightForest(){                  //anim pour la nuit
        StartCoroutine(Night());
    }

    public void DayForest(){                    //anim poir le jour
        StartCoroutine(Day());
   
    }

    public void SetRaining(bool b){
        IsRaining=b;
    }

    private IEnumerator Night(){
        for(int i=0; i<50; i++){
            for(int j=0; j<treeTab.Length; j++){
                treeTab[j].GetComponent<SpriteRenderer>().color = new Color((100-i)/100f,(100-i)/100f,(100-i)/100f);
            }
            yield return new WaitForSeconds(0.1f);
        }
    } 

    private IEnumerator Day(){
        for(int i=0; i<50; i++){
            for(int j=0; j<treeTab.Length; j++){
                treeTab[j].GetComponent<SpriteRenderer>().color = new Color(0.5f+i/100.0f,0.5f+i/100.0f,0.5f+i/100.0f);
            }
            yield return new WaitForSeconds(0.1f);
        }
    } 

}
