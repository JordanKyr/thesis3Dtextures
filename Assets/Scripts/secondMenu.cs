using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
public class secondMenu : MonoBehaviour

{

    public UIDocument menuDoc;
    private Button buttonReturn,buttonMainMenu;
    private VisualElement visualElement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    { 
        pauseGame();  //stamataei to paixnidi stin enarksi

         visualElement=menuDoc.rootVisualElement;
         
         buttonReturn=visualElement.Q<Button>("returnToGame");
        buttonMainMenu=visualElement.Q<Button>("buttonMainMenu");

        buttonMainMenu.clicked+= openMainMenu;  
         
         
         buttonReturn.clicked +=resumeGame;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){                              //an patithei to escape anoiggei to menu kai stamataei to paixnidi
        menuDoc.rootVisualElement.visible=true; 
        pauseGame();
    }
    }
    void pauseGame(){

        Time.timeScale=0; //pagonei to paixnidi
    }

 void openMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
        mainMenuScript.Instance.visualElement.visible=true;

    }

    void resumeGame(){

        Time.timeScale=1; //epistrefei sto paixnidi
        menuDoc.rootVisualElement.visible=false;  //krivei to menou
    }
    
}



