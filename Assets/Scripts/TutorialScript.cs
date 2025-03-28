using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.LowLevelPhysics;
using UnityEngine.UIElements;

public class TutorialScript : MonoBehaviour

{
    public UIDocument uIDocument;
    private VisualElement visualTutorialSteps;
    private Label labelSteps;
    private float doubleTap=0.3f;    //xroniko parathiro gia double tap
    private float lastTap=0f;       //teletaio patima space
    private bool isWaiting=false, topButton=false, botButton=false;       //tsek an perimenei patima apo deftero space
    
    public HapticPlugin hapticPlugin;

    public Transform typeAStart,typeA2Start, typeBStart, typeCStart, typeDStart, asfaltosStart,pavementStart, player, playerRotation;  //simio anaforas gia metafora tou paikti sta diaforetika tiles
    private int index=0;

    public FirstPersonController fpc;
    public AudioSource sourceTutorial;
    public AudioClip[] clipsTutorial;

    public SphereCollider sphereCollider;

    private enum tutorialStep { waitReturn, waitW, waitS,waitD,waitA, waitTopButton, waitBotButton}
    private tutorialStep currentStep=tutorialStep.waitReturn;

    void OnEnable()
    {
     

       

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        
           visualTutorialSteps =uIDocument.rootVisualElement;
        labelSteps=visualTutorialSteps.Q<Label>("labelSteps");


    }

    // Update is called once per frame
    void Update()
    {
      //Debug.Log($"Top Button: {hapticPlugin.topButton}, Bottom Button: {hapticPlugin.botButton}");



                                 //elegxos an exei patithei space
        if(Input.GetKeyDown(KeyCode.Space)){                            //elegxos an exei patithei prin space kai einai sta xronia plaisia
            if(isWaiting && Time.time - lastTap<=doubleTap){
                Debug.Log("Double Tap");
                isWaiting=false;
               
                onDoubleSpace();
             

            }
            else {                              //enimerosi protou tap
                lastTap=Time.time;
                isWaiting=true;
              
            }

        }
        if(isWaiting && Time.time-lastTap>doubleTap) {              //diaxeirisi single tap
            isWaiting=false;
            Debug.Log("Single Tap");
            

        }


        switch(currentStep)
        {
            case tutorialStep.waitReturn:
                if(Input.GetKeyDown(KeyCode.Return)){
                 PlayAudioTutorial(0);
                 labelSteps.text="Press W to move forward";
                 currentStep=tutorialStep.waitW;
                }
            break;
            case tutorialStep.waitW:
                if(Input.GetKeyDown(KeyCode.W)){
                 PlayAudioTutorial(1);
                 labelSteps.text="Press S to move backwards";
                 currentStep=tutorialStep.waitS;
                }
            break;
                case tutorialStep.waitS:
                if(Input.GetKeyDown(KeyCode.S)){
                 PlayAudioTutorial(2);
                 labelSteps.text="Press D to move rightwards";
                 currentStep=tutorialStep.waitD;
                }
            break;

                case tutorialStep.waitD:
                if(Input.GetKeyDown(KeyCode.D)){
                 PlayAudioTutorial(3);
                 labelSteps.text="Press A to move leftwards";
                 currentStep=tutorialStep.waitA;
                }
            break;
    
                    case tutorialStep.waitA:
                if(Input.GetKeyDown(KeyCode.A)){
                PlayAudioTutorial(4);
                labelSteps.text="Press the top button to turn left";
                 currentStep=tutorialStep.waitTopButton;
                
                }
                break;
            case tutorialStep.waitTopButton:
                if(hapticPlugin.topButton){
                PlayAudioTutorial(5);
                 labelSteps.text="Press the bottom button to turn right";
                 currentStep=tutorialStep.waitBotButton;
                }
            break;
              case tutorialStep.waitBotButton:
                if(hapticPlugin.botButton){
                 
                 labelSteps.visible=false;
                }
            break;
    
    }

    }
    

    void onDoubleSpace(){
    
        UnityEngine.Vector3 newPosition=player.position;
            switch(index)                   //switch case gia metafora toy paikti se alla block
            {
                case 0: 
                    Debug.Log ("Go to A");
                    newPosition.x=typeAStart.position.x;        
                    newPosition.z=typeAStart.position.z;
                    player.position=newPosition;
                    PlayAudioTutorial(index+6);
                    index++;

                break;

                  case 1: 
                    Debug.Log ("Go to A2");
                    newPosition.x=typeA2Start.position.x;        
                    newPosition.z=typeA2Start.position.z;
                    player.position=newPosition;
                    PlayAudioTutorial(index+6);
                    index++;

                break;

                 case 2: 
                    Debug.Log ("Go to B");
                    newPosition.x=typeBStart.position.x;
                    newPosition.z=typeBStart.position.z;
                    player.position=newPosition;
                    PlayAudioTutorial(index+6);
                    index++;
                break;

                 case 3: 
                    Debug.Log ("Go to C");
                    newPosition.x=typeCStart.position.x;
                    newPosition.z=typeCStart.position.z;
                    player.position=newPosition;
                    PlayAudioTutorial(index+6);
                    index++;
                break;

                 case 4: 
                    Debug.Log ("Go to D");
                    newPosition.x=typeDStart.position.x;
                    newPosition.z=typeDStart.position.z;
                    player.position=newPosition;
                    PlayAudioTutorial(index+6);
                    index++;
                break;

                case 5: 
                    Debug.Log ("Go to Pavement");
                    newPosition.x=pavementStart.position.x;
                    newPosition.z=pavementStart.position.z;
                    player.position=newPosition;
                    PlayAudioTutorial(index+6);
                    index++;
                break;


                
                case 6: 
                    Debug.Log ("Go to Street");
                    newPosition.x=asfaltosStart.position.x;
                    newPosition.z=asfaltosStart.position.z;
                    player.position=newPosition;
                    PlayAudioTutorial(index+6);
                    index=0;
                break;
            }   fpc.UpdateTargetPos(player.position); //enimerosi toy ActorHolder gia to neo position (alternative moving system)
                playerRotation.localRotation= UnityEngine.Quaternion.Euler(0f, 180f, 0f); //enimerosi toy Touch gia na exei sosti fora o paiktis
    }

#region AUDIO MANAGER                                                       //manager gia na paizoun ta audio apo lista me clips
    public void PlayAudioTutorial(int index){                           
        if(index>=0 && index< clipsTutorial.Length){
                sourceTutorial.clip=clipsTutorial[index];
                sourceTutorial.PlayDelayed(1f);         //play with delay
        
        }   else Debug.LogWarning("öut of bounds clip");

    }
#endregion

}
