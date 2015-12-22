/*
 * @author Valentin Simonov / http://va.lent.in/
 */

using System;
using HutongGames.PlayMaker;
using TouchScript.Gestures;
using UnityEngine;

namespace TouchScript.Modules.Playmaker.Actions
{
    [ActionCategory("TouchScript")]
    [HutongGames.PlayMaker.Tooltip("Sends an event when a flick gesture is performed over the object.")]
    public class Flicked : FsmStateAction
    {

        #region Input

        [HutongGames.PlayMaker.Tooltip("The GameObject that owns the Gesture.")]
        public FsmOwnerDefault GameObject;

        [HutongGames.PlayMaker.Tooltip("Optionally drag a component directly into this field (gesture name will be ignored).")]
        public Component Component;

        #endregion

        #region Output

        [UIHint(UIHint.Variable)]
        public FsmVector2 ScreenFlickVector;

        [UIHint(UIHint.FsmEvent)]
        public FsmEvent SendEvent;

        #endregion

        #region Private variables

        protected FlickGesture gesture;

        #endregion

        #region FSM methods

        public override void Reset()
        {
            GameObject = null;
            Component = null;
            ScreenFlickVector = null;
            SendEvent = null;
        }

        public override void OnEnter()
        {
            gesture = GestureUtils.GetGesture<FlickGesture>(Fsm, GameObject, Component, true);
            if (gesture == null)
            {
                LogError("Gesture is missing");
                return;
            }

            gesture.Flicked += gestureFlickedHandler;
        }

        public override void OnExit()
        {
            if (gesture == null) return;
            gesture.Flicked -= gestureFlickedHandler;
        }

        #endregion

        #region Private functions

        private void gestureFlickedHandler(object sender, EventArgs e)
        {
            if (SendEvent == null) return;
            if (ScreenFlickVector != null) ScreenFlickVector.Value = gesture.ScreenFlickVector;
            Fsm.Event(SendEvent);
        }

        #endregion

    }
}