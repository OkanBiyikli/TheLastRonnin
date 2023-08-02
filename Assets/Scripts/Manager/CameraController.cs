using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;//kameranın takip edeceği player
    public Transform room; //geçiş yapcağımız odalar
    public Transform activeRoom;// içinde olduğumuz aktif oda

    [Range(-5, 5)]
    public float minModX, maxModX, minModY, maxModY;


    public static CameraController instance;

    private void Awake() 
    {
        if(instance == null)    
        {
            instance = this;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        var minPosY = activeRoom.GetComponent<BoxCollider2D>().bounds.min.y + minModY;
        var maxPosY = activeRoom.GetComponent<BoxCollider2D>().bounds.min.y + maxModY;
        var minPosX = activeRoom.GetComponent<BoxCollider2D>().bounds.min.x + minModX;
        var maxPosX = activeRoom.GetComponent<BoxCollider2D>().bounds.min.x + maxModX;

        Vector3 clampedPos = new Vector3(Mathf.Clamp(player.position.x, minPosX, maxPosX), 
            Mathf.Clamp(player.position.y, minPosY, maxPosY), Mathf.Clamp(player.position.z, -10, -10));

        transform.position = new Vector3(clampedPos.x, clampedPos.y, clampedPos.z);
    }
}
