using System.Numerics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class StylusToCamera : MonoBehaviour


  
{

    public GameObject ActorRot;     //VAZO TO TOUCH GIA NA MIN STRIVEI O PAIKTIS ME TI FORA TOU STYLUS

    // Start is called once before the first execution of Update after the MonoBehaviour is created
      // public float sensY;
    //public float sensX;

    //public Transform orientation;

    //float xRotation;
    //float yRotation;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()

    
    {
            //UnityEngine.Vector3  eulerRotation = new UnityEngine.Vector3(transform.eulerAngles.x,ActorRot.transform.eulerAngles.y*-1, transform.eulerAngles.z);

            //transform.rotation = UnityEngine.Quaternion.Euler(eulerRotation);

          transform.rotation= UnityEngine.Quaternion.Euler(transform.eulerAngles.x, ActorRot.transform.eulerAngles.y*-1,0);



           // orientation.rotation=UnityEngine.Quaternion.Euler(0, yRotation, 0);

           // UnityEngine.Vector3  eulerRotation = new UnityEngine.Vector3(transform.eulerAngles.x,ActorRot.transform.eulerAngles.y, transform.eulerAngles.z);

           // transform.rotation = UnityEngine.Quaternion.Euler(eulerRotation)
    }
}
