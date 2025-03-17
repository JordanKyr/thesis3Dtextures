using UnityEngine;

public class limitYAxis : MonoBehaviour
{

    public float minY=0f;   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         Vector3 position = transform.position;

      
        position.y = Mathf.Clamp(position.y, minY, float.PositiveInfinity);

        
        transform.position = position;
    }
}
