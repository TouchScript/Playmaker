/*
 * @author Valentin Simonov / http://va.lent.in/
 */

using HutongGames.PlayMaker;
using TouchScript.Gestures;
using TouchScript.Hit;
using UnityEngine;

namespace TouchScript.Modules.Playmaker.Actions
{
    [ActionCategory("TouchScript")]
    [HutongGames.PlayMaker.Tooltip("Retrieves 3D hit details from a gesture.")]
    public class Get3DHitResult : FsmStateAction
    {
        [HutongGames.PlayMaker.Tooltip("The GameObject that owns the Gesture.")]
        public FsmOwnerDefault GameObject;

        [UIHint(UIHint.Behaviour)]
        [HutongGames.PlayMaker.Tooltip("The name of the Gesture.")]
        public FsmString Gesture;

        [HutongGames.PlayMaker.Tooltip("Optionally drag a component directly into this field (gesture name will be ignored).")]
        public Component Component;

        #region Output

        [UIHint(UIHint.Variable)]
        public FsmObject Target;
        
        [UIHint(UIHint.Variable)]
        public FsmObject Collider;
        
        [UIHint(UIHint.Variable)]
        public FsmObject Rigidbody;

        [UIHint(UIHint.Variable)]
        public FsmVector3 Normal;

        [UIHint(UIHint.Variable)]
        public FsmVector3 Point;

        #endregion

        private Gesture gesture;

        public override void Reset()
        {
            GameObject = null;
            Gesture = null;
            Component = null;

            Target = null;
            Collider = null;
            Rigidbody = null;
            Normal = Vector3.zero;
            Point = Vector3.zero;
        }

        public override void OnEnter()
        {
            gesture = GestureUtils.GetGesture<Gesture>(Fsm, GameObject, Gesture, Component, false);
            if (gesture == null)
            {
                LogError("Gesture is missing");
                return;
            }

            TouchHit hit;
            gesture.GetTargetHitResult(out hit);
            if (hit.Type != TouchHit.TouchHitType.Hit3D) return;

            if (Target != null) Target.Value = hit.Transform;
            if (Rigidbody != null) Rigidbody.Value = hit.RaycastHit.rigidbody;
            if (Collider != null) Collider.Value = hit.RaycastHit.collider;
            if (Normal != null) Normal.Value = hit.Normal;
            if (Point != null) Point.Value = hit.Point;

            Finish();
        }

        public override string ErrorCheck()
        {
            if (GestureUtils.GetGesture<Gesture>(Fsm, GameObject, Gesture, Component, false) == null) return "Gesture is missing";
            return null;
        }

    }
}
