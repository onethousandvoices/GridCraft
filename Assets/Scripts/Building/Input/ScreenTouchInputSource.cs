using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

namespace GridCraft.Construction
{
    public interface IPointerInputSource
    {
        void Disable();

        void Enable();

        bool TryGetActivePointer(out Vector2 screenPosition, out int pointerId);
    }

    public sealed class ScreenTouchInputSource : IPointerInputSource
    {
        public void Disable() => EnhancedTouchSupport.Disable();

        public void Enable() => EnhancedTouchSupport.Enable();

        public bool TryGetActivePointer(out Vector2 screenPosition, out int pointerId)
        {
            var touches = Touch.activeTouches;
            var count = touches.Count;
            for (var i = 0; i < count; i++)
            {
                var touch = touches[i];
                if (touch.phase is TouchPhase.Ended or TouchPhase.Canceled) continue;

                screenPosition = touch.screenPosition;
                pointerId = touch.touchId;
                return true;
            }

            screenPosition = default;
            pointerId = default;
            return false;
        }
    }
}