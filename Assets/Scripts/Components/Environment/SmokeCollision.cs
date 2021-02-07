using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class SmokeCollision : MonoBehaviour
{
    public Transform Actor;
    public Transform Conversant;
    public string Conversation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnParticleCollision(GameObject other) {
        Debug.Log("particle collision!");
         DialogueManager.StartConversation(Conversation, Actor, Conversant);
    }
}
