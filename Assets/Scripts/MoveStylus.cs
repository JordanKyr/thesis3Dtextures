using UnityEngine;

public class MoveStylus : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Camera cameraPosition;
    

    //public GameObject simpleStylus;

         //Vector3 rayOrigin = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));


    void Start()
    {
           Debug.Log("Screen Height : " + Screen.height);
             Debug.Log("Screen Width : " + Screen.width);
        // 
    }

    // Update is called once per frame
    void Update()
    {
       transform.position=cameraPosition.ScreenToWorldPoint(new Vector3( (Screen.width/2) ,  (Screen.height /2)  ,  (cameraPosition.nearClipPlane+ 0.5f)) );
    }
}
