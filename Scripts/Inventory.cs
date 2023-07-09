using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    public TextMeshProUGUI itemCounterText;
    private int tomato;
    private int dear;
    private int money;



    // SYSTEME D'INVENTAIRE AVEC LE VISUEL EN HAUT A DROITE

    void Start()
    {
        tomato = 0;
        dear = 0;
        money = 0;
        UpdateCounterText();
    }

    public void IncrementTomato()
    {
        tomato++;
        UpdateCounterText();
    }

    public void IncrementTomato(int i)
    {
        tomato+=i;
        UpdateCounterText();
    }



    public void IncrementPray()
    {
        dear++;
        UpdateCounterText();
    }

    public void SetTomato(int i){
        tomato=i;
        UpdateCounterText();

    }

    public void SetDear(int i){
        dear=i;
        UpdateCounterText();

    }

    public void SetMoney(int i){
        money+=i;
        UpdateCounterText();
    }

    public int GetMoney(){
        return money;
    }

    public int GetDear(){
        return dear;
    }

    public int GetTomato(){
        return tomato;
    }

    private void UpdateCounterText()
    {
        itemCounterText.text = dear+"\n"+tomato+"\n"+money ;
    }
}