using UnityEngine;

public class globalSettings : MonoBehaviour
{
    public static globalSettings Instance { get; set;}
        public float globalGame1Time=0f, globalGame2Time=0f, globalGame3Time=0f, globalGame3Percent=-1.0f;          //global apothikeusi metavliton
        public int globalGame2Mistakes=-1;
        public string globalCorrectOrder="";


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
    
}
