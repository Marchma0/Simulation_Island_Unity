using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{

    // Systeme d'interaction du joueur avec les objets de la map


    public PlayerHealth p;

    private void OnTriggerEnter2D(Collider2D other){

        if(other.CompareTag("hitable")){
            other.GetComponent<PrayMovement>().Hit();                    // Si c'est une proie, il lui fait des dégats via l'appel de cette fonction
        }

        if(other.CompareTag("vege")){
            other.GetComponent<TomatoFarm>().Collect();                 // Si c'est une tomate, il l'a collecte tout en l'ajoutant a l'inventaire
            p.RemoveEnergy(5); 
            p.AddLife(10);
        }
        
        if(other.CompareTag("house")){
            int tomato= FindObjectOfType<Inventory>().GetTomato();      // Si c'est la maison, il se repose et vends tout son inventaire
            int dear= FindObjectOfType<Inventory>().GetDear();

            FindObjectOfType<Inventory>().SetMoney(tomato*2+dear *5);
            FindObjectOfType<Inventory>().SetTomato(0);
            FindObjectOfType<Inventory>().SetDear(0);

            p.Rest();
        }
    }
}
