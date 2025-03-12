using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;


public class tutorialMenu : MonoBehaviour
{

    public UIDocument menuDoc;
    private Button buttonStart,buttonExit;
    private VisualElement visualElement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
            pauseGame();  //stamataei to paixnidi stin enarksi
            
            visualElement=menuDoc.rootVisualElement;
        buttonStart=visualElement.Q<Button>("startButton");     //sisxetisi ton stoixeion toy UI me kodika
        buttonExit=visualElement.Q<Button>("exitButton");

        buttonStart.clicked += resumeGame;
        buttonExit.clicked += exitTutorial;
    }

    void OnEnable()
    {
            

    }

    // Update is called once per frame
    void Update()
    {
    
    }

    void pauseGame(){

        Time.timeScale=0; //pagonei to paixnidi
    }

    void resumeGame(){

        Time.timeScale=1; //epistrefei sto paixnidi
        menuDoc.rootVisualElement.visible=false;  //krivei to menou
    }

    void exitTutorial(){
        SceneManager.LoadScene("2");
        resumeGame();
    }
}
