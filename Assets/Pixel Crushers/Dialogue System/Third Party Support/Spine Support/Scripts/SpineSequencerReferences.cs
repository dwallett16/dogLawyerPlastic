using UnityEngine;
using System.Collections.Generic;

namespace PixelCrushers.DialogueSystem.SpineSupport
{

    /// <summary>
    /// This component holds references to Spine AnimationReferenceAssets
    /// so the SpineAnimation() sequencer command can access them.
    /// </summary>
    public class SpineSequencerReferences : MonoBehaviour
    {
        public Spine.Unity.SkeletonAnimation skeletonAnimation;
        public List<Spine.Unity.AnimationReferenceAsset> animationReferenceAssets = new List<Spine.Unity.AnimationReferenceAsset>();
    }
}