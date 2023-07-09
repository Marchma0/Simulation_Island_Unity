using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{


    //      SYSTEME QUI PERMET LA MAJ DE LA VIE, DE L'ENERGIE ET L'INVINCIBILITÉ INDUITE


    public int maxHealth=100;
    public int maxEnergy=100;
    public int currentHealth;
    public int currentEnergy;
    public bool isInvincible=false;
    public bool dead =false;

    public HealthBar healthBar;
    public EnergyBar energyBar;
    public SpriteRenderer graphics;

    void Start()
    {
        currentHealth = maxHealth;
        currentEnergy = maxEnergy;
        healthBar.SetMaxHealth(100);
        energyBar.SetMaxEnergy(100);
        StartCoroutine(EnergyLoop());   // coroutine qui fais déscendre l'energie de 1 toutes les 0.6 secondes

    }

    void Update()
    {
        // vérifie si son nombre de hp lui permet de vivre sinon on le tue
        if(currentHealth<=0){
            dead=true;
            this.gameObject.SetActive(false); 
        }
    }

    public void TakeDamage(int damage){
        currentHealth-=damage;
        healthBar.SetHealth(currentHealth);
        isInvincible=true;
        StartCoroutine(InvincibiltyHit());  // Invinciilité induite (courte période + clignote en rouge)
    }

    public void RemoveEnergy(int energy){
        currentEnergy-=energy;
        energyBar.SetEnergy(currentEnergy);
    }

    public void Rest(){
        currentEnergy=maxEnergy;
        energyBar.SetEnergy(currentEnergy);
    }

    public void AddLife(int life){
        currentHealth +=10;
        if(currentHealth>maxHealth){
            currentHealth=maxHealth;
        }
        healthBar.SetHealth(currentHealth);
    }
    
    public bool GetDead(){
        return dead;
    }

    public IEnumerator InvincibiltyHit(){
        for(int i=0; i<6;i++){
            graphics.color = new Color(1f,0f,0f,1f);
            yield return new WaitForSeconds(0.2f);
            graphics.color = new Color(1f,1f,1f,1f);
            yield return new WaitForSeconds(0.2f);
        }
        isInvincible=false;
    }

    public IEnumerator EnergyLoop(){
        while(true){
            if(currentEnergy==0){
                TakeDamage(10);
                currentEnergy=maxEnergy;
                energyBar.SetMaxEnergy(100);
            }
            currentEnergy-=1;
            energyBar.SetEnergy(currentEnergy);
            yield return new WaitForSeconds(0.6f);
            
        }
    }



}
