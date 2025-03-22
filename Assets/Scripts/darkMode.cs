using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class darkMode : MonoBehaviour


{


    public UIDocument darkModeUI;
    private VisualElement visualElement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
           visualElement=darkModeUI.rootVisualElement.Q<VisualElement>("darkMode");
         
            if (visualElement != null)
        {
            visualElement.visible = false;
          
        }
        else
        {
            Debug.LogError("darkMode not found!");
        }
      
    }

    // Update is called once per frame
    void Update()
    {
         

        if(Input.GetKeyDown(KeyCode.RightControl)  && Time.timeScale==1){
          if(visualElement.visible==false) { 
            visualElement.visible=true;
          }
          else visualElement.visible=false;
          
        }

        
    }}

