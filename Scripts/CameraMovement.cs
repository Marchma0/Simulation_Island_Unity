using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public Camera c;  
    public float smoothing;    
    public bool switchVar;
    public int switch_access;

    // Start is called before the first frame update
    void Start()
    {
        c = GetComponent<Camera>();
        switchVar = false;
        switch_access=0;
    }

    // Update is called once per frame
    void LateUpdate()
    {   
        //Systeme pour switch de camera (vue de toute l'ile / vue du perso)
        if(Input.GetButtonDown("Enable Debug Button 1")){
            if(switch_access==0){
                switchVar=true;
                switch_access+=1;
            }
            else{
                switchVar=false;
                switch_access-=1;
            }
        }
        if(transform.position != target.position && !switchVar){
            c.orthographicSize = 4.0f;
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position,targetPosition,smoothing);
        }
        else{
            c.orthographicSize = 17.39f;
            transform.position = new Vector3(6.4f,-5.49f,-10f);
        }
        
    }
}
