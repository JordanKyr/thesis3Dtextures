using UnityEngine;

public class displayChange : MonoBehaviour
{
    [SerializeField] private  Camera camera1;  // Assign in Inspector
    [SerializeField] private  Camera camera2;  // Assign in Inspector

    void Start()
    {
        // Start with Camera 1 active, Camera 2 off
        SetActiveCamera(camera1);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            SetActiveCamera(camera1);
        }

        if (Input.GetKeyDown(KeyCode.Equals) || Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            SetActiveCamera(camera2);
        }
    }

    void SetActiveCamera(Camera activeCam)
    {
        camera1.enabled = (activeCam == camera1);
        camera2.enabled = (activeCam == camera2);
    }
}
