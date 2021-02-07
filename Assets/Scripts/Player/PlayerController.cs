﻿using UnityEngine;
using PixelCrushers.DialogueSystem;

public class PlayerController : MonoBehaviour {

    public float speed;

    new private Rigidbody2D rigidbody2D;
    private Animator animator;
    public ParticleSystem smokeParticleSystem;
    private float currentDirection;
    
    public SpineAnimatorController spineAnimatorController;
    public enum PlayerState {
        Idle,
        Walk,
        SmokeStart,
        Smoking,
        SmokeEnd
    }

    private PlayerState currentState, previousState;
    bool stateChanged;
    float moveHorizontal = 0f;
    bool isInConversation = false;
    public SpriteRenderer spriteRenderer;

    // Use this for initialization
    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rigidbody2D = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
        if (transform.rotation.y == 0) currentDirection = 1f; else currentDirection = -1f;
        spineAnimatorController = GetComponent<SpineAnimatorController>();
        currentState = PlayerState.Idle;
        previousState = currentState;
        spineAnimatorController.PlayNewAnimation("Idle", true);

        spriteRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.TwoSided;
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        //Store the current horizontal input in the float moveHorizontal.
        if ((currentState == PlayerState.Idle || currentState == PlayerState.Walk) && !isInConversation) {
            moveHorizontal = Input.GetAxisRaw (Constants.Horizontal);

            //Movement
            Vector2 movement = new Vector2 (moveHorizontal * speed, 0);
            rigidbody2D.velocity = movement;

            //Flip image
            if (currentDirection != moveHorizontal && moveHorizontal != 0) {
                currentDirection = moveHorizontal;
                var scale = transform.localScale;
                if(movement.x < 0)
                    scale.x = scale.x >=0 ? scale.x * -1 : scale.x;
                else
                    scale.x = scale.x < 0 ? scale.x * -1 : scale.x;
                transform.localScale = scale;
                
            }
        }

        //Determine next state
        if (moveHorizontal != 0) {
            currentState = PlayerState.Walk;
        }
        else {
            if (Input.GetButtonDown(Constants.Smoke)) {
                currentState = PlayerState.SmokeStart;
            }
            else if(Input.GetButtonDown(Constants.CaseStatus)) {
                DialogueManager.StartConversation("CaseStatus", this.gameObject.transform.Find("Monologue"));
            }
            else  if (!IsSmoking()) {
                currentState = PlayerState.Idle;
                currentDirection = 0;
            }
        }

        if (IsSmoking()) {
            if (currentState == PlayerState.SmokeStart) {
                if (spineAnimatorController.IsAnimationComplete()) {
                    currentState = PlayerState.Smoking;
                }
            }
            else if (currentState == PlayerState.Smoking) {
                if (spineAnimatorController.IsAnimationComplete() && !Input.GetButton(Constants.Smoke)) {
                    currentState = PlayerState.SmokeEnd;
                }
            }
            else if (currentState == PlayerState.SmokeEnd) {
                if (spineAnimatorController.IsAnimationComplete()) {
                    currentState = PlayerState.Idle;
                }
            }
        }

        stateChanged = previousState != currentState;
        previousState = currentState;
        if (stateChanged) HandleStateChange();
    }

    void HandleStateChange() {
        string stateName = null;
        bool loop = false;
        switch (currentState) {
            case PlayerState.Idle:
                stateName = "Idle";
                loop = true;
                break;
            case PlayerState.Walk:
                stateName = "Walk";
                loop = true;
                break;
            case PlayerState.SmokeStart:
                stateName = "SmokeStart";
                loop = false;
                break;
            case PlayerState.Smoking:
                stateName = "Smoking";
                loop = true;
                break;
            case PlayerState.SmokeEnd:
                stateName = "SmokeEnd";
                loop = false;
                break;
            default:
                break;
        }

        spineAnimatorController.PlayNewAnimation(stateName, loop);
    }

    private bool IsSmoking() {
        if (currentState == PlayerState.SmokeStart || currentState == PlayerState.Smoking
        || currentState == PlayerState.SmokeEnd) {
            return true;
        }
        return false;
    }

    public void AnimationEvent(string eventName) {
        Debug.Log("Animation event hit!");

        if (eventName == "SmokeExhaleStart") {
            smokeParticleSystem.Play();
        }

        if (eventName == "SmokeExhaleStop") {
            smokeParticleSystem.Stop();
        }
    }

    void OnConversationStart() {
        isInConversation = true;
    }

    void OnConversationEnd() {
        isInConversation = false;
    }
}