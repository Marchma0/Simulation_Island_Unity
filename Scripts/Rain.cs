using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Rain : MonoBehaviour
{

    // Systeme de pluie/automne 

    public bool isAutumn=true;
    public bool isDay=true;

    public Tilemap ground = GameObject.FindWithTag("ground").GetComponent<Tilemap>();
    public Tilemap decors = GameObject.FindWithTag("decors").GetComponent<Tilemap>();

    void Start()
    { 
        StartCoroutine(IsRaining());

    }

    IEnumerator IsRaining(){
        while(true){
            if(!isAutumn || !isDay){yield return new WaitForSeconds(1.0f);}
            else{
                for(int i=0;i<70;i++){
                    ground.color = new Color(255,255-i,255-2*i); //     255 184 109    0 71 146
                    decors.color = new Color(255,255-i,255-2*i);
                    yield return new WaitForSeconds(0.1f);
                }

            }
        }
    }



}
