using System.Reflection.Emit;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Label = UnityEngine.UIElements.Label;


public class mainMenuScript : MonoBehaviour
{   

    public static mainMenuScript Instance { get; set;}

    public UIDocument uIDocument;
    public VisualElement visualElement;
    private Button buttonTutorial, buttonGame1, buttonGame2, buttonGame3;

    private Label labelColliderSelected, labelMaterialSelected, labelGame1Time, labelGame2Time, labelGame2Mistakes, labelGame3Time, labelGame3Percent ,labelCorrectOrder;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        if(uIDocument !=null){
        visualElement=uIDocument.rootVisualElement;
        buttonGame1=visualElement.Q<Button>("buttonGame1");
        buttonGame2=visualElement.Q<Button>("buttonGame2");
        buttonGame3=visualElement.Q<Button>("buttonGame3");
        buttonTutorial=visualElement.Q<Button>("buttonTutorial");

        labelColliderSelected=visualElement.Q<Label>("labelColliderSelected");
        labelMaterialSelected=visualElement.Q<Label>("labelMaterialSelected");
        labelGame1Time=visualElement.Q<Label>("labelGame1Time");
        labelGame2Time=visualElement.Q<Label>("labelGame2Time");
        labelGame2Mistakes=visualElement.Q<Label>("labelGame2Mistakes");
        labelGame3Time=visualElement.Q<Label>("labelGame3Time");
        labelGame3Percent=visualElement.Q<Label>("labelGame3Percent");
        labelCorrectOrder=visualElement.Q<Label>("labelCorrectOrder");

        buttonGame1.clicked+=openGame1;
        buttonGame2.clicked+=openGame2;
        buttonGame3.clicked+=openGame3;
        buttonTutorial.clicked+=openTutorial;


     
        
        }
    }

      void Awake(){

        if(Instance ==null){

            Instance=this;
           DontDestroyOnLoad(gameObject);  //menei stis skines

        }
        else 
        {
            Destroy(gameObject);
        }

    }
  

    // Update is called once per frame
    void Update()
    {

        
        
    }
    void OnEnable()
    {
        
    }

    void openGame1(){
        SceneManager.LoadScene("Game 1");
        visualElement.visible=false;
    }



       void openGame2(){
        SceneManager.LoadScene("Game 2");
         visualElement.visible=false;
       
    }


       void openGame3(){
        SceneManager.LoadScene("Game 3");
         visualElement.visible=false;
    }

       void openTutorial(){
        SceneManager.LoadScene("Tutorial");
         visualElement.visible=false;
    }


    public void setColliderPreset(){
            if(presetSettings.Instance.globalColliderPreset!=-1) labelColliderSelected.text= "Haptic Collider Preset: "+ presetSettings.Instance.globalColliderPreset;

    }

     public void setMaterialPreset(){
            if(presetSettings.Instance.globalTilePreset!=-1) labelMaterialSelected.text= "HapticMaterial Preset: "+ presetSettings.Instance.globalTilePreset;
    }

    public void setGame1Time(){
            if(globalSettings.Instance.globalGame1Time!=0) labelGame1Time.text= "Time: "+globalSettings.Instance.globalGame1Time.ToString("F2");

    }

     public void setGame2Time(){
            if(globalSettings.Instance.globalGame2Time!=0) labelGame2Time.text= "Time: "+globalSettings.Instance.globalGame2Time.ToString("F2");

    }


    public void setGame2Mistakes(){
        if(globalSettings.Instance.globalGame2Mistakes!=-1) labelGame2Mistakes.text="Mistakes: "+ globalSettings.Instance.globalGame2Mistakes;

    }

    public void setGame3Percent(){
        if(globalSettings.Instance.globalGame3Percent!=-1.0f) labelGame3Percent.text="Percent on Path: "+ globalSettings.Instance.globalGame3Percent;
    }

    public void setGame3Time(){
        if(globalSettings.Instance.globalGame3Time!=0) labelGame3Time.text= "Time: "+globalSettings.Instance.globalGame3Time.ToString("F2");
    }

    public void setCorrectOrder(){
        if(globalSettings.Instance.globalCorrectOrder!="") labelCorrectOrder.text="Correct Order: "+globalSettings.Instance.globalCorrectOrder;
    }
}
