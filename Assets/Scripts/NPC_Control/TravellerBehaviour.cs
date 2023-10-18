using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; 


public class TravellerBehaviour : MonoBehaviour
{
    // set locations 
    [SerializeField] private List<Transform> rooms;
    [SerializeField] private List<string> roomText;

    [SerializeField] private List<Transform> Locations; //Every location in chosen room 

    [SerializeField] private Animator TravellerAnimator;

    // Figure out activity intervalls 
    private float breakTime;
    [SerializeField] private float minTime = 1; 
    [SerializeField] private float maxTime = 5;
    private float time; 

    private int _locationIndex = 0;  
    private bool doRound = true; 

    private NavMeshAgent _agent; 

    private bool hasDestination = false;   // Bool to not run setting destination every time 


    [SerializeField] private List<string> TextToPlayer; 
     
    
   
   
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        //speechControl = GameObject.Find("RM dialogue Canvas").GetComponent<SpatialUIBehaviour>();

        // initialize first room 
        InitializeNewRoom(Random.Range(0, rooms.Count));
    }

    void Update()
    {

        if(TravellerAnimator != null)
        {
            NPCAnimation();
        }
        
        
        HandleConsumingRound();
        if(!doRound)
        {
            time += Time.deltaTime;
            if (time >= breakTime)
            {
                int newRoomIndex = Random.Range(0, rooms.Count);
                InitializeNewRoom(newRoomIndex);
                SetRandomTime();
            }
        }
    }

    private void OnTriggerEnter(Collider other) // talk to player when bumping 
    {
        if (other.CompareTag("Player"))
        {
            //speechControl.CreateTextBox(TextToPlayer[Random.Range(0, TextToPlayer.Count)]);
        }
    }

    void InitializeNewRoom(int _newRoom)
    {
        // reset old route 
        Locations.Clear();
        _locationIndex = 0; 
        time = 0;

        // get locations of new room 
        foreach (Transform child in rooms[_newRoom])
        {
            Locations.Add(child);
        }

        Transform lsRecent = Locations[0];
        Transform idleLocation = Locations[1];
        doRound = true;
    }

    void NPCAnimation() // show walk or idle animation 
    {
        if (doRound || hasDestination)
        {
            TravellerAnimator.SetBool("IsWalking", true);
        }
        else if (!hasDestination && (_agent.remainingDistance > 0.1f))
        {
            TravellerAnimator.SetBool("IsWalking", true);
        }
        else
        {
            TravellerAnimator.SetBool("IsWalking", false);
        }
    }


    void HandleConsumingRound() // walk to each point and return to the idle location 
    {
        if (doRound) 
        { 
            if (_agent.remainingDistance < 0.4f && !_agent.pathPending)
            {
                if (Locations.Count == 0) { return; }

                if (_locationIndex != Locations.Count)
                {
                    _agent.destination = Locations[_locationIndex].position;
                    _locationIndex = (_locationIndex + 1);
                }
                else 
                {
                    doRound = false;
                    hasDestination = false;
                    _agent.destination = Locations[1].position;
                }
            } 
        }
    }
    
    void SetRandomTime()
        {
            breakTime = Random.Range(minTime, maxTime);
        }
     
}
