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
        globalSettings.Instance.globalGame1Time=timerAll;
        mainMenuScript.Instance.setGame1Time();
    if(labelTimer!=null) {
                
            labelTimer.text=$"Timer: {timerAll:F2} sec"; //provoli sto label me format F2

    }
      
    }

    public void replaceTiles(){

        string newOrder="";

        List<int> indices = Enumerable.Range(0,originals.Count).ToList();       //ftiaxno ena set me indices kai kano ayto shuffle
        indices=indices.OrderBy(x=> Random.value).ToList();
        
        List<GameObject> randomListRepl=replacements.OrderBy(x=> Random.value).ToList();
        
        List<string> replacementOrder = new List<string>();

        for(int i=0; i<5 ; i++){

             int shuffledIndex = indices[i];

            UnityEngine.Vector3 orn=originals[i].transform.position;
            UnityEngine.Vector3 rpl=randomListRepl[shuffledIndex].transform.position;
            

             randomListRepl[shuffledIndex].transform.position=new UnityEngine.Vector3( orn.x, rpl.y,orn.z); 
             
            // replacementOrder.Add(replacements[shuffledIndex].name);
             
             newOrder+=randomListRepl[shuffledIndex].name+",";
             

            Destroy(originals[shuffledIndex]);

           

        }

        //globalSettings.Instance.globalCorrectOrder= string.Join(", ", replacementOrder);
        globalSettings.Instance.globalCorrectOrder=newOrder;
        mainMenuScript.Instance.setCorrectOrder();
    }
}