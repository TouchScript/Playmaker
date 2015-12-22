﻿/*
 * @author Valentin Simonov / http://va.lent.in/
 */

using System;
using HutongGames.PlayMaker;
using TouchScript.Gestures;
using UnityEngine;

namespace TouchScript.Modules.Playmaker.Actions
{
    [ActionCategory("TouchScript")]
    [HutongGames.PlayMaker.Tooltip("Sends an event when an object is pressed.")]
    public class Pressed : FsmStateAction
    {

        #region Input

        [HutongGames.PlayMaker.Tooltip("The GameObject that owns the Gesture.")]
        public FsmOwnerDefault GameObject;

        [HutongGames.PlayMaker.Tooltip("Optionally drag a component directly into this field (gesture name will be ignored).")]
        public Component Component;

        #endregion

        #region Output

        public FsmEvent SendEvent;

        #endregion

        #region Private variables

        protected PressGesture gesture;

        #endregion

        #region FSM methods

        public override void Reset()
        {
            GameObject = null;
            Component = null;
            SendEvent = null;
        }

        public override void OnEnter()
        {
            gesture = GestureUtils.GetGesture<PressGesture>(Fsm, GameObject, Component, true);
            if (gesture == null)
            {
                LogError("Gesture is missing");
                return;
            }

            gesture.Pressed += gesturePressedHandler;
        }

        public override void OnExit()
        {
            if (gesture == null) return;
            gesture.Pressed -= gesturePressedHandler;
        }

        #endregion

        #region Private functions

        private void gesturePressedHandler(object sender, EventArgs e)
        {
            if (SendEvent == null) return;
            Fsm.Event(SendEvent);
        }

        #endregion

    }
}