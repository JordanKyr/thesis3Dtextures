using System.Reflection.Emit;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class mainMenuScript : MonoBehaviour
{

    public UIDocument uIDocument;
    private VisualElement visualElement;
    private Button buttonTutorial, buttonGame1, buttonGame2, buttonGame3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        if(uIDocument !=null){
        visualElement=uIDocument.rootVisualElement;
        buttonGame1=visualElement.Q<Button>("buttonGame1");
        buttonGame2=visualElement.Q<Button>("buttonGame2");
        buttonGame3=visualElement.Q<Button>("buttonGame3");
        buttonTutorial=visualElement.Q<Button>("buttonTutorial");

        buttonGame1.clicked+=openGame1;
        buttonGame2.clicked+=openGame2;
        buttonGame3.clicked+=openGame3;
        buttonTutorial.clicked+=openTutorial;
        
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void openGame1(){
        SceneManager.LoadScene("Game 1");
    }



       void openGame2(){
        SceneManager.LoadScene("Game 2");
       
    }


       void openGame3(){
        SceneManager.LoadScene("Game 3");
    }

       void openTutorial(){
        SceneManager.LoadScene("Tutorial");
    }
}
