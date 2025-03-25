using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Game1Script : MonoBehaviour
{


    public List<GameObject> originals, replacements;
    public UIDocument uIDocument;
    private float timerAll=0f;
    private VisualElement visualElement;
    private Label labelTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
                     //metraei olo ton xrono
        replaceTiles();
             visualElement=uIDocument.rootVisualElement;                  //sisxetisi ton stoixeion toy UI me kodika
                labelTimer=visualElement.Q<Label>("labelTimer");

                 
    }

    // Update is called once per frame
    void Update()
    {   timerAll+=Time.deltaTime;  
    if(labelTimer!=null) {
                
            labelTimer.text=$"Timer: {timerAll:F2} sec"; //provoli sto label me format F2
    }
        
    }

    public void replaceTiles(){

        List<GameObject> randomList=originals.OrderBy(x=> Random.value).ToList();       //anakateuei tin original lista

        for(int i=0; i<5 ; i++){

            UnityEngine.Vector3 orn=randomList[i].transform.position;
            UnityEngine.Vector3 rpl=replacements[i].transform.position;

             replacements[i].transform.position=new UnityEngine.Vector3( orn.x, rpl.y,orn.z); 
             

            Destroy(randomList[i]);

           

        }

    }
}