using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.UIElements;
public class StylusSettings : MonoBehaviour
{

    public UIDocument uiDocument;
    public GameObject hapticCollider;
    public GameObject hapticActor;
    public GameObject specialTile;

    private VisualElement vElement;
    private Slider slStiffness, slStFriction, slDynFriction,  slMagn, slTileStiff, slDamp, slStatTile, slDynTile;
    private SliderInt slFreq;

    private Button applyHaptic, applyGlobal, applyTiles;
    private Toggle enableVib;


    void OnEnable(){
        

        vElement=uiDocument.rootVisualElement;                  //sisxetisi ton stoixeion toy UI me kodika

        slStiffness=vElement.Q<Slider>("Stiffness");    
        slDynFriction=vElement.Q<Slider>("DynamicFriction");
        slStFriction=vElement.Q<Slider>("StaticFriction");

        slFreq=vElement.Q<SliderInt>("Frequency");
        slMagn=vElement.Q<Slider>("Magnitude");
        
        slTileStiff=vElement.Q<Slider>("TileStiffness");
        slDamp=vElement.Q<Slider>("TileDamping");
        slDynTile=vElement.Q<Slider>("TileDynamic");
        slStatTile=vElement.Q<Slider>("TileStatic");



        applyTiles=vElement.Q<Button>("ApplyTiles");

        applyHaptic=vElement.Q<Button>("ApplyHaptic");

        applyGlobal=vElement.Q<Button>("ApplyGlobal");

        enableVib=vElement.Q<Toggle>("EnableVibration");

        if (hapticCollider != null)                                                                             //pairno arxikes times oti exei idi to plugin apo to Inspector
        {
            slStiffness.value = hapticCollider.GetComponent<HapticCollider>().hStiffness;
            slDynFriction.value = hapticCollider.GetComponent<HapticCollider>().hFrictionD;
            slStFriction.value=hapticCollider.GetComponent<HapticCollider>().hFrictionS;
        }

        if(hapticActor != null)
        {
            slFreq.value=hapticActor.GetComponent<HapticPlugin>().VibrationGFrequency;
            slMagn.value=hapticActor.GetComponent<HapticPlugin>().VibrationGMag;
            enableVib.value=hapticActor.GetComponent<HapticPlugin>().enable_Vibration;

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
        applyGlobal.clicked+=ApplyGlobal;
        applyTiles.clicked+=ApplyTiles;
    }


    void ApplyHaptic(){                                                                                //apply tis allages sta sliders
        hapticCollider.GetComponent<HapticCollider>().hFrictionS= slStFriction.value;
        hapticCollider.GetComponent<HapticCollider>().hStiffness= slStiffness.value;
        hapticCollider.GetComponent<HapticCollider>().hFrictionD= slDynFriction.value;

    }

    void ApplyGlobal(){

        hapticActor.GetComponent<HapticPlugin>().VibrationGFrequency=slFreq.value;
        hapticActor.GetComponent<HapticPlugin>().VibrationGMag=slMagn.value;	
        hapticActor.GetComponent<HapticPlugin>().enable_Vibration=enableVib.value;
 }

    void ApplyTiles(){
        //specialTile.GetComponent<HapticMaterial>().hStiffness=slTileStiff.value;    //allazo ola ta special tiles

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
