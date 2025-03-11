using UnityEngine;

public class invisible : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
public GameObject myObject;

    
    void Start()
    {

    myObject.GetComponent<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
