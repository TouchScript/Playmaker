/*
 * @author Valentin Simonov / http://va.lent.in/
 */

using System;
using HutongGames.PlayMaker;

namespace TouchScript.Modules.Playmaker.Actions
{
    [ActionCategory("TouchScript")]
    [HutongGames.PlayMaker.Tooltip("Sends events for touch events.")]
    public class TouchEvent : FsmStateAction
    {

        #region Input

        #endregion

        #region Output

        [UIHint(UIHint.FsmEvent)]
        public FsmEvent BeganEvent;

        [UIHint(UIHint.FsmEvent)]
        public FsmEvent MovedEvent;

        [UIHint(UIHint.FsmEvent)]
        public FsmEvent EndedEvent;

        [UIHint(UIHint.FsmEvent)]
        public FsmEvent CancelledEvent;

        [UIHint(UIHint.FsmInt)]
        public FsmInt Id;

        [UIHint(UIHint.FsmGameObject)]
        public FsmGameObject Target;

        [UIHint(UIHint.FsmVector2)]
        public FsmVector2 ScreenPosition;

        [UIHint(UIHint.FsmVector2)]
        public FsmVector2 PreviousScreenPosition;

        #endregion

        #region Private variables

        #endregion

        #region FSM methods

        public override void Reset()
        {
            BeganEvent = null;
            MovedEvent = null;
            EndedEvent = null;
            CancelledEvent = null;
            Id = null;
            Target = null;
            ScreenPosition = null;
            PreviousScreenPosition = null;
        }

        public override void OnEnter()
        {
            TouchManager.Instance.TouchBegan += touchBeganHandler;
            TouchManager.Instance.TouchMoved += touchMovedHandler;
            TouchManager.Instance.TouchEnded += touchEndedHandler;
            TouchManager.Instance.TouchCancelled += touchCancelledHandler;
        }

        public override void OnExit()
        {
            TouchManager.Instance.TouchBegan -= touchBeganHandler;
            TouchManager.Instance.TouchMoved -= touchMovedHandler;
            TouchManager.Instance.TouchEnded -= touchEndedHandler;
            TouchManager.Instance.TouchCancelled -= touchCancelledHandler;
        }

        #endregion

        #region Private functions

        private void updateTouchData(TouchPoint touch)
        {
            if (ScreenPosition != null) ScreenPosition.Value = touch.Position;
            if (PreviousScreenPosition != null) PreviousScreenPosition.Value = touch.PreviousPosition;
            if (Id != null) Id.Value = touch.Id;
            if (Target != null) Target.Value = touch.Target.gameObject;
        }

        private void touchBeganHandler(object sender, TouchEventArgs touchEventArgs)
        {
            if (BeganEvent == null) return;
            updateTouchData(touchEventArgs.Touch);
            Fsm.Event(BeganEvent);
        }

        private void touchMovedHandler(object sender, TouchEventArgs touchEventArgs)
        {
            if (MovedEvent == null) return;
            updateTouchData(touchEventArgs.Touch);
            Fsm.Event(MovedEvent);
        }

        private void touchEndedHandler(object sender, TouchEventArgs touchEventArgs)
        {
            if (MovedEvent == null) return;
            updateTouchData(touchEventArgs.Touch);
            Fsm.Event(MovedEvent);
        }

        private void touchCancelledHandler(object sender, TouchEventArgs touchEventArgs)
        {
            if (MovedEvent == null) return;
            updateTouchData(touchEventArgs.Touch);
            Fsm.Event(MovedEvent);
        }

        #endregion

    }
}
