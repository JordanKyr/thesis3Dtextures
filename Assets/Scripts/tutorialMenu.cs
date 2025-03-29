using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;


public class tutorialMenu : MonoBehaviour
{

    public UIDocument menuDoc;
    private Button buttonStart,buttonMainMenu;
    private VisualElement visualElement;



    void Awake()
    {
        pauseGame();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
           
           // pauseGame();  //stamataei to paixnidi stin enarksi
            
            visualElement=menuDoc.rootVisualElement;
        buttonStart=visualElement.Q<Button>("startTutorial");     //sisxetisi ton stoixeion toy UI me kodika
        
        buttonMainMenu=visualElement.Q<Button>("buttonMainMenu");

        buttonMainMenu.clicked+= openMainMenu;
        buttonStart.clicked += resumeGame;
   

    
    }

    void OnEnable()
    {
            

    }

    // Update is called once per frame
    void Update()
    {
     
     
     if(Input.GetKeyDown(KeyCode.Escape)){                              //an patithei to escape anoiggei to menu kai stamataei to paixnidi
        menuDoc.rootVisualElement.visible=true; 
        buttonStart.text="Resume Tutorial";
        pauseGame();
    }
    
    }

    void openMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
        mainMenuScript.Instance.visualElement.visible=true;

    }
    void pauseGame(){

        Time.timeScale=0; //pagonei to paixnidi
    }

    void resumeGame(){
      
        Time.timeScale=1; //epistrefei sto paixnidi
        menuDoc.rootVisualElement.visible=false;  //krivei to menou
    }



  
}
