using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CountGraph : MonoBehaviour
{
    public int treeCount;
    public int prayCount;
    public int predatorCount;
    public int money;
    public int tomato;


    void Start()
    {
        prayCount = GameObject.FindGameObjectsWithTag("hitable").Length;
        treeCount = GameObject.FindGameObjectsWithTag("tree").Length;
        predatorCount = GameObject.FindGameObjectsWithTag("predator").Length;
        money = 0;
        StartCoroutine(Graph());

    }


    void Update()
    {
    }

    IEnumerator Graph(){
        int i=1;
        string filePath = Application.dataPath + "/graph.txt";

        // Créer un StreamWriter pour écrire dans le fichier texte
        StreamWriter writer = new StreamWriter(filePath);

        // Écrire dans le fichier texte
        writer.WriteLine("GRAPH : \n\n");

        while(true){
            prayCount = GameObject.FindGameObjectsWithTag("hitable").Length;
            treeCount = GameObject.FindGameObjectsWithTag("tree").Length;
            predatorCount = GameObject.FindGameObjectsWithTag("predator").Length;
            money = GameObject.FindWithTag("inventory").GetComponent<Inventory>().GetMoney();
            //tomato = GameObject.FindWithTag("player").GetComponent<PlayerMovement>().GetTomato();
            writer.WriteLine("Jour : "+ i);
            writer.WriteLine("Predators = " + predatorCount + "\nPray = " + prayCount);
            writer.WriteLine("Tree = " + treeCount + "\nmoney : "+ money + "\n\n");
            i++;
            yield return new WaitForSeconds(1.0f);
        }
        writer.Close();

    }
}
