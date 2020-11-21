using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class SpineAnimatorController : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;
    public List<string> listOfAllAnimations;
    private Spine.AnimationState animationState;
    private bool isAnimationComplete;
    private bool isAnimationStarted;
    private bool isAnimationEnded;


    void Start() {
        skeletonAnimation.AnimationState.Complete += delegate (TrackEntry trackEntry) {
            Debug.Log("Animation complete");
            isAnimationComplete = true;
            isAnimationStarted = false;
        };
        skeletonAnimation.AnimationState.Start += delegate (TrackEntry trackEntry) {
            Debug.Log("Animation started");
            isAnimationComplete = false;
            isAnimationStarted = true;
        };
        skeletonAnimation.AnimationState.End += delegate (TrackEntry trackEntry) {
            Debug.Log("Animation ended");
        };

        skeletonAnimation.AnimationState.Event += HandleEvent;

        //capitalize animation names
        for(int i = 0; i < listOfAllAnimations.Count; i++) {
            if (listOfAllAnimations[i].Length == 1) {
                listOfAllAnimations[i] = char.ToUpper(listOfAllAnimations[i][0]) + listOfAllAnimations[i].Substring(1);
            }
        }
    }

    public void OnSpineAnimationStart(TrackEntry trackEntry) {
        isAnimationStarted = true;
        isAnimationComplete = false;
    }

    public void OnSpineAnimationComplete(TrackEntry trackEntry) {
        isAnimationComplete = true;
        isAnimationStarted = false;
    }

    public void OnSpineAnimationEnd(TrackEntry trackEntry) {
        isAnimationEnded = true;
    }

    void HandleEvent (TrackEntry trackEntry, Spine.Event e) {
      SendMessage("AnimationEvent", e.Data.Name);
   }

    
    public bool IsAnimationComplete() {
        return isAnimationComplete;
    }

    public bool IsAnimationStarted() {
        return isAnimationStarted;
    }

    public bool IsAnimationEnded() {
        return isAnimationEnded;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayNewAnimation(string animation, bool loop) {
        if (listOfAllAnimations.Contains(animation)) {
            skeletonAnimation.AnimationState.SetAnimation(0, animation, loop);
        }
    }
}
