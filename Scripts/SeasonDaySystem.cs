using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SeasonDaySystem : MonoBehaviour
{
    public Tilemap ground;
    public Tilemap decors;
    public GameObject forest;
    public GameObject rain;

    public bool isAutumn=false;
    public bool isDay=false;
    public bool handle=false;

    public int jour=1;

    void Start()
    {
        ground = GameObject.FindWithTag("ground").GetComponent<Tilemap>();
        decors = GameObject.FindWithTag("decors").GetComponent<Tilemap>();

        rain = GameObject.FindWithTag("rain");
        forest = GameObject.FindWithTag("forest");
        StartCoroutine(Day());
    }


    private IEnumerator Day(){                                          // En simple, tout les 3 jours y a la saison des pluies (les arbres ne brulent plus + repousse)
        while(true){                                                    // + les tomates ne peuvent pas etre cultiver
            if(!isAutumn){
                forest.GetComponent<TreeFire>().SetRaining(false);
                GameObject[] tomatoes = GameObject.FindGameObjectsWithTag("vege");
                foreach(GameObject tomato in tomatoes){
                    tomato.GetComponent<TomatoFarm>().SetRaining(false);
                }
                rain.SetActive(false);
                handle=true;
                yield return new WaitForSeconds(15.0f);

                forest.GetComponent<TreeFire>().NightForest();
                
                for(int i=0;i<50;i++){
                    ground.color = new Color((100-i)/100f,(100-i)/100f,(100-i)/100f);
                    decors.color = new Color((100-i)/100f,(100-i)/100f,(100-i)/100f);
                    yield return new WaitForSeconds(0.1f);
                }

                yield return new WaitForSeconds(15.0f);

                forest.GetComponent<TreeFire>().DayForest();

                for(int i=0;i<50;i++){
                    ground.color = new Color(0.5f+i/100.0f,0.5f+i/100.0f,0.5f+i/100.0f);
                    decors.color = new Color(0.5f+i/100.0f,0.5f+i/100.0f,0.5f+i/100.0f);
                    yield return new WaitForSeconds(0.1f);
                }
                jour++;
                if(jour%4==3){
                    isAutumn=true;
                }
            }
            else{
                yield return new WaitForSeconds(15.0f);
                if(handle){
                    forest.GetComponent<TreeFire>().SetRaining(true);
                    forest.GetComponent<TreeFire>().NightForest();

                    GameObject[] tomatoes = GameObject.FindGameObjectsWithTag("vege");
                    foreach(GameObject tomato in tomatoes){
                        tomato.GetComponent<TomatoFarm>().SetRaining(true);
                    }
                    
                    for(int i=0;i<50;i++){
                        ground.color = new Color((100-i)/100f,(100-i)/100f,(100-i)/100f);
                        decors.color = new Color((100-i)/100f,(100-i)/100f,(100-i)/100f);
                        yield return new WaitForSeconds(0.1f);
                    }
                    rain.SetActive(true);

                }
                else{
                    forest.GetComponent<TreeFire>().NightForest();
                    
                    for(int i=0;i<50;i++){
                        ground.color = new Color((100-i)/100f,(75-i/2)/100f,0.55f);
                        decors.color = new Color((100-i)/100f,(75-i/2)/100f,0.55f);
                        yield return new WaitForSeconds(0.1f);
                    }
                }
                yield return new WaitForSeconds(15.0f);

                forest.GetComponent<TreeFire>().DayForest();

                for(int i=0;i<50;i++){
                    ground.color = new Color(0.5f+(i/100.0f),0.5f+(i/2/100f),0.55f);                       //     255 184 109    0 71 146
                    decors.color =  new Color(0.5f+(i/100.0f),0.5f+(i/2/100f),0.5f);  ;
                    yield return new WaitForSeconds(0.1f);
                }
                handle=false;
                jour++;
                if(jour%4==1){
                    isAutumn=false;
                }
            }
            
        }
    }
}
