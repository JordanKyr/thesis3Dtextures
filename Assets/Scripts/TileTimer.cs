using System.Reflection.Emit;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using Label = UnityEngine.UIElements.Label;

public class TileTimer : MonoBehaviour 
        //metrisi kai provoli xronou otan o paiktis den einai pano sta sosta tiles
{

    private float timerTile=0f, timerAll=0f; 
    public UIDocument uiDocument;
    private bool isOnTile=false;
    private VisualElement vElement,vElementContainer;
    private Label timerTileLabel, timerAllLabel, percentTileLabel; // Label Grafiko
    float timePerc;

    private float distance=30f;  //apostasi apo dapedo
    [SerializeField]private LayerMask isGround;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         vElement=uiDocument.rootVisualElement;                  //sisxetisi ton stoixeion toy UI me kodika
         vElementContainer=vElement.Q<VisualElement>("timerContainer");

        if(uiDocument!=null) {
            timerTileLabel=vElement.Q<Label>("timerTile");
            timerAllLabel=vElement.Q<Label>("timerAll");
            percentTileLabel=vElement.Q<Label>("percentTile");

        }

      //  initialUITimer();
    }

    // Update is called once per frame
    void Update()
    {
        
        timerAll+=Time.deltaTime;               //metraei olo ton xrono
         globalSettings.Instance.globalGame3Time=timerAll;
        mainMenuScript.Instance.setGame3Time();
        
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
            globalSettings.Instance.globalGame3Percent=Mathf.Round(timePerc);
            mainMenuScript.Instance.setGame3Percent();
            
        }
    }
/*
        if(Input.GetKeyDown(KeyCode.Escape) && Time.timeScale==1) pausedUITimer();
        else if(Time.timeScale==1) returnUITimer();
 
    }

private void returnUITimer(){
            vElementContainer.style.alignSelf=Align.FlexEnd;
            vElement.style.translate= new Translate(Length.Percent(0), Length.Percent(0));
            vElementContainer.style.backgroundColor=new Color(0,0,0, 0f);
           

}
     private void pausedUITimer(){
           
            vElementContainer.style.alignSelf=Align.Center;
            vElementContainer.style.backgroundColor=new Color(0.4386792f,0.6273584f,1.0f, 0.6f);
            vElement.style.translate=new Translate(Length.Percent(0), Length.Percent(75));



    }
 */  

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
