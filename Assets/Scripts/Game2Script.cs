using System.Reflection.Emit;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using Label = UnityEngine.UIElements.Label;

public class Game2Script : MonoBehaviour

{
    private float timerTile=0f, timerAll=0f; 
    public UIDocument uiDocument;
    private bool isOnTile=false;
    private VisualElement vElement,vElementContainer;
    private Label timerTileLabel, timerAllLabel, percentTileLabel, labelMistakes; // Label Grafiko
    float timePerc;
    public FirstPersonController firstPersonController;

    private float distance=30f;  //apostasi apo dapedo
    [SerializeField]private LayerMask isGround;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    { if(uiDocument!=null) {
         vElement=uiDocument.rootVisualElement;                  //sisxetisi ton stoixeion toy UI me kodika
         vElementContainer=vElement.Q<VisualElement>("timerContainer");

       
            timerTileLabel=vElement.Q<Label>("timerTile");
            timerAllLabel=vElement.Q<Label>("timerAll");
            percentTileLabel=vElement.Q<Label>("percentTile");
            labelMistakes=vElement.Q<Label>("labelMistakes");
        }

      //  initialUITimer();
    }

    // Update is called once per frame
    void Update()
    {
        
        timerAll+=Time.deltaTime;               //metraei olo ton xrono
        checkTile();
        if(!isOnTile) timerTile+=Time.deltaTime;  //otan den einai sta tiles tote auksanete
        
        if(timerAllLabel!=null) {
                
            timerAllLabel.text=$"Timer: {timerAll:F2} sec"; //provoli sto label me format F2
    }
        
        
        if(timerTileLabel!=null) {
           // Debug.Log($"Time: {timer:F2} sec");      
            timerTileLabel.text=$"Not on Path: {timerTile:F2} sec"; //provoli sto label me format F2

 
    }
    
        if(timerAll==0) timePerc=0;                              //ypologismos percent xronou sto plakaki
        else timePerc=100-(timerTile/timerAll)*100f;

        if(percentTileLabel!=null){
            percentTileLabel.text=$"Percent on Path: {Mathf.Round(timePerc)}%";  
        }

          if(labelMistakes!=null){
           labelMistakes.text=$"Mistakes: {firstPersonController.getMistakeCount()}";  
        }


        /*if(Input.GetKeyDown(KeyCode.Escape) && Time.timeScale==1) pausedUITimer();
        else if(Time.timeScale==1) returnUITimer();
        */
        
    
    }

private void returnUITimer(){
            vElementContainer.style.alignSelf=Align.FlexEnd;
            vElement.style.translate= new Translate(Length.Percent(0), Length.Percent(0));
            vElementContainer.style.backgroundColor=new Color(0,0,0, 0f);
           

}
     /*private void pausedUITimer(){
           
            vElementContainer.style.alignSelf=Align.Center;
            vElementContainer.style.backgroundColor=new Color(0.4386792f,0.6273584f,1.0f, 0.6f);
            vElement.style.translate=new Translate(Length.Percent(0), Length.Percent(75));



    }*/


    private void checkTile(){               //elegxos me raycast an yparxei apo kato to sosto tile i stin asfalto
        RaycastHit raycastHit;                                                                              
        if(Physics.Raycast(transform.position,Vector3.down, out  raycastHit, distance, isGround))
        {
              Debug.DrawRay(transform.position, Vector3.down , Color.red, 10f);
            if(raycastHit.collider.gameObject.name.StartsWith("type") || raycastHit.collider.gameObject.name.StartsWith("ASFALTOS") )
            {
                isOnTile=true;
                return;
            }
            isOnTile=false;
        }

    }


}
