using UnityEngine;

public class LaserGoal : MonoBehaviour
{
    public GameObject doorToBeOpened;
    public GameObject player;  // reference to the player's game object
    public bool trigger = false;
  

    void Start()
    {
        InvokeRepeating("TriggerChange", 0, 2f);
    }

    void Update()
    {
        if (trigger)
        {
            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y-1f, player.transform.position.z); // teleport the player -10 units along the y-axis
        }
    }

    void TriggerChange(){
        trigger = false;
    }

    void OnCollusionEnter(Collider collider){
        if(collider.gameObject.tag == "LaserBeam"){
            trigger = true;
        }
    }
}
