using UnityEngine;

public class presetSettings : MonoBehaviour
{
    public static presetSettings Instance {get;set;}
    public int globalTilePreset , globalColliderPreset ;

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
