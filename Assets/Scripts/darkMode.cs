using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class darkMode : MonoBehaviour


{


    public UIDocument darkModeUI;
    private VisualElement visualElement;
    private Label labelDarkMode;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
           visualElement=darkModeUI.rootVisualElement.Q<VisualElement>("darkMode");
           labelDarkMode=darkModeUI.rootVisualElement.Q<Label>("labelDarkMode");
            if (visualElement != null)
        {
            visualElement.visible = false;
        }
        else
        {
            Debug.LogError("VisualElement 'darkMode' not found!");
        }
      
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightControl) && Time.timeScale==1 ){
          // darkModeUI.rootVisualElement.visible=true;                 NEEDS FIXING
            // labelDarkMode.visible=true;
            Debug.Log(darkModeUI.rootVisualElement.visible); } 
        if(Input.GetKeyDown(KeyCode.RightControl) &&  visualElement.visible==true ) visualElement.visible=false;
    }
    }

