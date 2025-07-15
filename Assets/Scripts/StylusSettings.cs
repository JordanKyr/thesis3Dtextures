using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;
public class StylusSettings : MonoBehaviour
{

    public UIDocument uiDocument;
    public GameObject hapticCollider;
    public GameObject hapticActor;
    public GameObject specialTile;

    private VisualElement vElement;
    private Slider slStiffness, slStFriction, slDynFriction, slTileStiff, slDamp, slStatTile, slDynTile;

    private Button applyHaptic, applyTiles;

    private RadioButtonGroup radioTilePresets, radioColliderPresets;




    void OnEnable(){
        

        vElement=uiDocument.rootVisualElement;                  //sisxetisi ton stoixeion toy UI me kodika


        radioTilePresets=vElement.Q<RadioButtonGroup>("radioTilePresets");
        radioColliderPresets=vElement.Q<RadioButtonGroup>("radioColliderPresets");

        slStiffness=vElement.Q<Slider>("Stiffness");    
        slDynFriction=vElement.Q<Slider>("DynamicFriction");
        slStFriction=vElement.Q<Slider>("StaticFriction");

       
        
        slTileStiff=vElement.Q<Slider>("TileStiffness");
        slDamp=vElement.Q<Slider>("TileDamping");
        slDynTile=vElement.Q<Slider>("TileDynamic");
        slStatTile=vElement.Q<Slider>("TileStatic");



        applyTiles=vElement.Q<Button>("ApplyTiles");

        applyHaptic=vElement.Q<Button>("ApplyHaptic");
     

       

     

        if (hapticCollider != null)                                                                             //pairno arxikes times oti exei idi to plugin apo to Inspector
        {
           // slStiffness.value = hapticCollider.GetComponent<HapticCollider>().hStiffness;
           slStiffness.value = 0.5f;
            slDynFriction.value = hapticCollider.GetComponent<HapticCollider>().hFrictionD;
            slStFriction.value=hapticCollider.GetComponent<HapticCollider>().hFrictionS;
        }

      

        if(specialTile != null)
        {
            slTileStiff.value=specialTile.GetComponent<HapticMaterial>().hStiffness;
            slDamp.value=specialTile.GetComponent<HapticMaterial>().hDamping;
            slDynTile.value=specialTile.GetComponent<HapticMaterial>().hFrictionD;
            slStatTile.value=specialTile.GetComponent<HapticMaterial>().hFrictionS;
        }
                                                                                                    //listener gia click ton koumpion
        applyHaptic.clicked+=ApplyHaptic;
        applyTiles.clicked+=ApplyTiles;


        if(radioTilePresets!=null){ 
            radioTilePresets.RegisterValueChangedCallback(evt => 
                {
                   applyTilePreset(evt.newValue);
                }
            
            );
        
        }

        
        if(radioColliderPresets!=null){ 
            radioColliderPresets.RegisterValueChangedCallback(evt => 
                {
                   applyColliderPreset(evt.newValue);
                }
            
            );
        
        }

    }

    void applyColliderPreset(int presetNumber){

         presetSettings.Instance.globalColliderPreset=presetNumber;
         mainMenuScript.Instance.setColliderPreset();                //UPDATE MAIN MENU LABEL

            switch(presetNumber){
                case 0:                                                                  //mikro collider
                        hapticCollider.transform.localScale=new Vector3(0.01f,0.01f,0.01f);
                break;

                case 1:                                                                   //mesaio collider
                       
                       hapticCollider.transform.localScale=new Vector3(0.025f,0.025f,0.025f);
                break;
                
                case 2:                                                                    //megalo collider
                        hapticCollider.transform.localScale=new Vector3(0.04f,0.04f,0.04f);
                break;

            }


    }


                                                //programmatismos radiobutton
    void applyTilePreset(int presetNumber){

                 GameObject[] allTiles=GameObject.FindObjectsOfType<GameObject>();

                        presetSettings.Instance.globalTilePreset=presetNumber;   //enimerono to global preset setting
                         mainMenuScript.Instance.setMaterialPreset();       //UPDATE MAIN MENU LABEL
                    
            switch(presetNumber)
            {
                case 0:
                    Debug.Log("Preset 1");

                    foreach (GameObject gameObject in allTiles){
                    if(gameObject.name.StartsWith("type")){
                            HapticMaterial hapticMaterial=gameObject.GetComponent<HapticMaterial>();
                        if(hapticMaterial!=null ){
                               slTileStiff.value=  hapticMaterial.hStiffness=1.0f;
                              slDamp.value=  hapticMaterial.hDamping=1.0f;
                                slDynTile.value= hapticMaterial.hFrictionD=0.25f;
                               slStatTile.value=  hapticMaterial.hFrictionS=0.25f;
                        } 

                    }}
                break;

                case 1:
                    Debug.Log("Preset 2");
                    foreach (GameObject gameObject in allTiles){
                    if(gameObject.name.StartsWith("type")){
                            HapticMaterial hapticMaterial=gameObject.GetComponent<HapticMaterial>();
                        if(hapticMaterial!=null ){
                              slTileStiff.value=   hapticMaterial.hStiffness=1.0f;
                               slDamp.value= hapticMaterial.hDamping=1.0f;
                               slDynTile.value=  hapticMaterial.hFrictionD=0.5f;
                                slStatTile.value= hapticMaterial.hFrictionS=0.5f;
                        } 

                    }}

                break;

                case 2:
                    Debug.Log("Preset 3");
                    foreach (GameObject gameObject in allTiles){
                    if(gameObject.name.StartsWith("type")){
                            HapticMaterial hapticMaterial=gameObject.GetComponent<HapticMaterial>();
                        if(hapticMaterial!=null ){
                               slTileStiff.value= hapticMaterial.hStiffness=1.0f;
                               slDamp.value= hapticMaterial.hDamping=1.0f;
                                slDynTile.value= hapticMaterial.hFrictionD=0.75f;
                                slStatTile.value=hapticMaterial.hFrictionS=0.75f;
                        } 

                    }}


                break;


            }


    }

    void ApplyHaptic(){                                                                                //apply tis allages sta sliders
        hapticCollider.GetComponent<HapticCollider>().hFrictionS= slStFriction.value;
        hapticCollider.GetComponent<HapticCollider>().hStiffness= slStiffness.value;
        hapticCollider.GetComponent<HapticCollider>().hFrictionD= slDynFriction.value;

    }

   

    void ApplyTiles(){
        specialTile.GetComponent<HapticMaterial>().hStiffness=slTileStiff.value;    //allazo ola ta special tiles

        GameObject[] allTiles=GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject gameObject in allTiles){
                if(gameObject.name.StartsWith("type")){
                        HapticMaterial hapticMaterial=gameObject.GetComponent<HapticMaterial>();
                       if(hapticMaterial!=null ){
                            hapticMaterial.hStiffness=slTileStiff.value;
                            hapticMaterial.hDamping=slDamp.value;
                            hapticMaterial.hFrictionD=slDynTile.value;
                            hapticMaterial.hFrictionS=slStatTile.value;
                       } 

                }


        }

 }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UnityEngine.Cursor.visible=true;
        UnityEngine.Cursor.lockState=CursorLockMode.None;

      

       if(presetSettings.Instance.globalColliderPreset!=null && presetSettings.Instance.globalTilePreset!=null){
            radioColliderPresets.value=presetSettings.Instance.globalColliderPreset;
            radioTilePresets.value=presetSettings.Instance.globalTilePreset;
        }else
        {
        radioColliderPresets.value=1;
         radioTilePresets.value=-1;

        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
      
           
    }


   
}
