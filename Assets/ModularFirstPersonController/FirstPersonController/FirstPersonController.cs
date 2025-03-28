// CHANGE LOG
// 
// CHANGES || version VERSION
//
// "Enable/Disable Headbob, Changed look rotations - should result in reduced camera jitters" || version 1.0.1

//edited by Iordanis Kyriazidis 9/2/2025

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEditor.UIElements;


#if UNITY_EDITOR
        using UnityEditor;
        using UnityEditor.UIElements; 
        using UnityEngine.UIElements; 
        using System.Net;
#endif

public class FirstPersonController : MonoBehaviour
{
    #region Camera Movement Variables

    public Camera playerCamera;
    private Rigidbody rb;
   


    public float fov = 60f;
    public bool invertCamera = false;
    public bool cameraCanMove = true;
    public float mouseSensitivity = 2f;
    public float maxLookAngle = 200f;

    // Crosshair
    public bool lockCursor = true;
  

    // Internal Variables
    private float yaw = 0.0f;
    private float pitch = 0.0f;

    #endregion

    #region Movement Variables

    public bool playerCanMove = true;
    public float walkSpeed = 5f;
    public float maxVelocityChange = 10f;
    #endregion
    #region Alternative Movement Variables
        public float tileSize=0.4f;
        public Vector3 targetPos;
        private bool inMotion;
        public LayerMask limitLayer;  //Layer gia ta oria
    public float altWalkSpeed = 7f;
    private int mistakeCount=0;
    

    #endregion

    #region Turn 90

    public KeyCode turnLeft=KeyCode.E;
    public KeyCode turnRight=KeyCode.Q;

    public GameObject Player;
    public AudioSource audioSourceLeft, audioSourceRight, audioSourceLimit;



    #endregion
   

       private void Awake()
    {
        rb = GetComponent<Rigidbody>();


        // Set internal variables
        playerCamera.fieldOfView = fov;

        
    }

    void Start()
    {
           targetPos=transform.position;

        if(lockCursor)
        {
            
        UnityEngine.Cursor.visible=true;
        UnityEngine.Cursor.lockState=CursorLockMode.None;
        }
       
    }

    private void Update()
    {
        #region Camera

        // Control camera movement
        if(cameraCanMove)
        {
            yaw = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * mouseSensitivity;

            if (!invertCamera)
            {
                pitch -= mouseSensitivity * Input.GetAxis("Mouse Y");
            }
            else
            {
                // Inverted Y
                pitch += mouseSensitivity * Input.GetAxis("Mouse Y");
            }

            // Clamp pitch between lookAngle
            pitch = Mathf.Clamp(pitch, -maxLookAngle, maxLookAngle);

            transform.localEulerAngles = new Vector3(0, yaw, 0);
            playerCamera.transform.localEulerAngles = new Vector3(pitch, 0, 0);
        }

        
        #endregion
/*                                  //disable movement with E and Q
        #region Turn 90
    	if(Input.GetKeyDown(turnLeft)){
             Player.transform.Rotate(0, 90.0f, 0);
             audioLeft();
        }
        
        if(Input.GetKeyDown(turnRight)){
             Player.transform.Rotate(0, -90.0f, 0);
             audioRight();
        } 
        #endregion 
  */      
       

        CheckGround();
            #region Movement

        if (playerCanMove)
        {
            // Calculate how fast we should be moving
            Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal")*-1.0f, 0, Input.GetAxis("Vertical")*-1.0f);   
            // All movement calculations while walking

           if (targetVelocity.magnitude > 1)       //otan patas dio koumpia na min trexei pio grigora
        {
            targetVelocity.Normalize();
        }
     
        
             targetVelocity = transform.TransformDirection(targetVelocity) * walkSpeed;

                // Apply a force that attempts to reach our target velocity
                Vector3 velocity = rb.linearVelocity;
                Vector3 velocityChange = (targetVelocity - velocity);
                velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
                velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
                velocityChange.y = 0;

                rb.AddForce(velocityChange, ForceMode.VelocityChange);
            
        }else{                                                      //ALTERNATIVE MOVING METHOD
               
             if (!inMotion)             //an den einai se kinisi allazei to target position
        {
            
           Vector3 direction = Vector3.zero;  //pairno to direction toy paikti se periptosi poy exei gyrisei me ta koumpia
            
            if (Input.GetKeyDown(KeyCode.W))
            {
                direction +=  -transform.forward ;
            }
            else if (Input.GetKeyDown(KeyCode.S)) 
            {
               direction += transform.forward;
            }
            else if (Input.GetKeyDown(KeyCode.A)) 
            {
               direction += transform.right;
            }
            else if (Input.GetKeyDown(KeyCode.D)) 
            {
                direction += -transform.right;
            }


            if (direction!=Vector3.zero && isInLimits(targetPos+direction*tileSize))
            {
                targetPos +=direction* tileSize;
                inMotion= true;
            }
        }



        if (inMotion)
        {
            
            transform.position = Vector3.MoveTowards(transform.position, targetPos, altWalkSpeed * Time.deltaTime);

           
            if (transform.position == targetPos)
            {
                inMotion = false;
            }
        }
        }

        #endregion

    }
    public void audioLeft(){
        if(audioSourceLeft!=null && !audioSourceLeft.isPlaying) audioSourceLeft.Play();
    }
    public void audioRight(){
        if(audioSourceRight!=null && !audioSourceRight.isPlaying) audioSourceRight.Play();
    }
    void FixedUpdate()
    {
    
    }
    private bool isInLimits(Vector3 targetPos){             //elegxos me raycast an iparxei empodio
    
        
        Vector3 direction = (targetPos - transform.position).normalized;
         RaycastHit hit;
        
        Debug.DrawRay(transform.position, direction * tileSize, Color.red, 10f);
       if (Physics.Raycast(transform.position, (targetPos - transform.position).normalized, out hit, tileSize, limitLayer))
        {
            
           Debug.Log("limit on the way"); 
           
           if(audioSourceLimit!=null && !audioSourceLimit.isPlaying) {
            audioSourceLimit.Play();
           mistakeCount++;
           }
           return false; 
          
        }return true;
    }

    public int getMistakeCount(){

        return mistakeCount;
    }

    public void UpdateTargetPos(Vector3 newPos){
        targetPos=newPos;

    }

    // Sets isGrounded based on a raycast sent straigth down from the player object
    private void CheckGround()
    {
        Vector3 origin = new Vector3(transform.position.x, transform.position.y - (transform.localScale.y * .5f), transform.position.z);
        Vector3 direction = transform.TransformDirection(Vector3.down);
        float distance = .75f;

        if (Physics.Raycast(origin, direction, out RaycastHit hit, distance))
        {
            Debug.DrawRay(origin, direction * distance, Color.red);
          
        }  
    }    
}



// Custom Editor
#if UNITY_EDITOR
    [CustomEditor(typeof(FirstPersonController)), InitializeOnLoadAttribute]
    public class FirstPersonControllerEditor : Editor
    {
    FirstPersonController fpc;
    SerializedObject SerFPC;

    private void OnEnable()
    {
        fpc = (FirstPersonController)target;
        SerFPC = new SerializedObject(fpc);
    }

    public override void OnInspectorGUI()
    {
        SerFPC.Update();

        EditorGUILayout.Space();
        GUILayout.Label("Modular First Person Controller", new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold, fontSize = 16 });
        GUILayout.Label("By Jess Case", new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Normal, fontSize = 12 });
        GUILayout.Label("version 1.0.1", new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Normal, fontSize = 12 });
         GUILayout.Label("edited by Iordanis Kyriazidis on 9-2-2025", new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Normal, fontSize = 12 });
        EditorGUILayout.Space();

        #region Camera Setup

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        GUILayout.Label("Camera Setup", new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold, fontSize = 13 }, GUILayout.ExpandWidth(true));
        EditorGUILayout.Space();

        fpc.playerCamera = (Camera)EditorGUILayout.ObjectField(new GUIContent("Camera", "Camera attached to the controller."), fpc.playerCamera, typeof(Camera), true);
        fpc.fov = EditorGUILayout.Slider(new GUIContent("Field of View", "The camera’s view angle. Changes the player camera directly."), fpc.fov, 10f, 179f);
        fpc.cameraCanMove = EditorGUILayout.ToggleLeft(new GUIContent("Enable Camera Rotation", "Determines if the camera is allowed to move."), fpc.cameraCanMove);

        GUI.enabled = fpc.cameraCanMove;
        fpc.invertCamera = EditorGUILayout.ToggleLeft(new GUIContent("Invert Camera Rotation", "Inverts the up and down movement of the camera."), fpc.invertCamera);
        fpc.mouseSensitivity = EditorGUILayout.Slider(new GUIContent("Look Sensitivity", "Determines how sensitive the mouse movement is."), fpc.mouseSensitivity, .1f, 10f);
        fpc.maxLookAngle = EditorGUILayout.Slider(new GUIContent("Max Look Angle", "Determines the max and min angle the player camera is able to look."), fpc.maxLookAngle, 40, 90);
        GUI.enabled = true;

        fpc.lockCursor = EditorGUILayout.ToggleLeft(new GUIContent("Lock and Hide Cursor", "Turns off the cursor visibility and locks it to the middle of the screen."), fpc.lockCursor);

        EditorGUILayout.Space();

        #endregion

   #region Movement Setup

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        GUILayout.Label("Movement Setup", new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold, fontSize = 13 }, GUILayout.ExpandWidth(true));
        EditorGUILayout.Space();

        fpc.playerCanMove = EditorGUILayout.ToggleLeft(new GUIContent("Enable Player Movement (FPC movement)", "Determines if the player is allowed to move."), fpc.playerCanMove);

        GUI.enabled = fpc.playerCanMove;
        fpc.walkSpeed = EditorGUILayout.Slider(new GUIContent("Walk Speed", "Determines how fast the player will move while walking."), fpc.walkSpeed, .1f, 20.0f);
        GUI.enabled = true;

        EditorGUILayout.Space();

        #endregion

      
      #region Turn Player

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        GUILayout.Label("Turn Player Setup", new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold, fontSize = 13 }, GUILayout.ExpandWidth(true));
        EditorGUILayout.Space();
        
        fpc.Player = (GameObject)EditorGUILayout.ObjectField(new GUIContent("Player", "Player to Turn."), fpc.Player, typeof(GameObject), true);
        fpc.limitLayer=EditorGUILayout.MaskField(new GUIContent("Limit Layer", "Collider layer of the walls"), fpc.limitLayer,UnityEditorInternal.InternalEditorUtility.layers);   //input gia layermask


        GUI.enabled = true;

        #endregion

        #region Audio

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        GUILayout.Label("Audio Setup", new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold, fontSize = 13 }, GUILayout.ExpandWidth(true));
        EditorGUILayout.Space();
        
        fpc.audioSourceLeft = (AudioSource)EditorGUILayout.ObjectField(new GUIContent("Audio Left", "Audiosource Comp Left"), fpc.audioSourceLeft, typeof(AudioSource), true);
         fpc.audioSourceRight = (AudioSource)EditorGUILayout.ObjectField(new GUIContent("Audio Right", "Audiosource Comp Right"), fpc.audioSourceRight, typeof(AudioSource), true);
        fpc.audioSourceLimit = (AudioSource)EditorGUILayout.ObjectField(new GUIContent("Audio Limit", "Limit"), fpc.audioSourceLimit, typeof(AudioSource), true);

        GUI.enabled = true;

        #endregion

        //Sets any changes from the prefab
        if(GUI.changed)
        {
            EditorUtility.SetDirty(fpc);
            Undo.RecordObject(fpc, "FPC Change");
            SerFPC.ApplyModifiedProperties();
        }
    }

}

#endif