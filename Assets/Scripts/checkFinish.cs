using UnityEngine;

public class checkFinish : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private float distance=15f;  //apostasi apo dapedo

    public AudioSource finishAudio;
    private bool isFinish=false;
    [SerializeField]private LayerMask isFinishPlane;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            checkFinishPlane();
    }

        private void checkFinishPlane(){               //elegxos me raycast an yparxei apo kato to sosto tile
        RaycastHit raycastHit;                                                                            
        if(Physics.Raycast(transform.position,Vector3.down, out  raycastHit, distance, isFinishPlane))          //elegxos an exei paei sto finish plane gia na teleiosei to paixnidi
        {
            if(raycastHit.collider.gameObject.name.Equals("finishPlane"))
            {
                    isFinish=true;
                   Debug.Log($"GAME OVER");    
                   Time.timeScale=0;
                    audioFinish();
            } else isFinish=false;
           
        }


    }
 public void audioFinish(){
        if(finishAudio!=null && !finishAudio.isPlaying) finishAudio.Play();
    }

}
