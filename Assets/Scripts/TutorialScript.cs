using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class TutorialScript : MonoBehaviour

{
    public float doubleTap=0.3f;    //xroniko parathiro gia double tap
    private float lastTap=0f;       //teletaio patima space
    private bool isWaiting=false;       //tsek an perimenei patima apo deftero space


    public Transform typeAStart, typeBStart, typeCStart, typeDStart, asfaltosStart, player, playerRotation;  //simio anaforas gia metafora tou paikti sta diaforetika tiles
    private int index=0;

    public AudioSource sourceTutorial;
    public AudioClip[] clipsTutorial;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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


    }

    void onDoubleSpace(){

        UnityEngine.Vector3 newPosition=player.position;
        playerRotation.rotation=UnityEngine.Quaternion.Euler(0f, 180f, 0f);   //arxikopoiisi rotation

            switch(index)                   //switch case gia metafora toy paikti se alla block
            {
                case 0: 
                    Debug.Log ("Go to A");
                    newPosition.x=typeAStart.position.x;        
                    newPosition.z=typeAStart.position.z;
                    player.position=newPosition;
                    PlayAudioTutorial(index+4);
                    index++;

                break;

                 case 1: 
                    Debug.Log ("Go to B");
                    newPosition.x=typeBStart.position.x;
                    newPosition.z=typeBStart.position.z;
                    player.position=newPosition;
                    PlayAudioTutorial(index+4);
                    index++;
                break;

                 case 2: 
                    Debug.Log ("Go to C");
                    newPosition.x=typeCStart.position.x;
                    newPosition.z=typeCStart.position.z;
                    player.position=newPosition;
                    PlayAudioTutorial(index+4);
                    index++;
                break;

                 case 3: 
                    Debug.Log ("Go to D");
                    newPosition.x=typeDStart.position.x;
                    newPosition.z=typeDStart.position.z;
                    player.position=newPosition;
                    PlayAudioTutorial(index+4);
                    index++;
                break;

                case 4: 
                    Debug.Log ("Go to Start");
                    newPosition.x=asfaltosStart.position.x;
                    newPosition.z=asfaltosStart.position.z;
                    player.position=newPosition;
                    
                    index=0;
                break;
            }
  

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
