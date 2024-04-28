using UnityEngine;

namespace SoundlightInteractive.EventSystem
{
    public abstract class Actor : MonoBehaviour
    {
        public EventManager Manager => EventManager.Instance;

        public bool isActive;

        protected virtual void OnEnable()
        {
            Listen(true);
        }

        protected virtual void OnDisable()
        {
            Listen(false);
        }

        protected virtual void Listen(bool status)
        {
            
        }

        protected virtual void Push(string eventName, params object[] arguments)
        {
            Manager.Push(eventName, arguments);
        }

        protected virtual void Register(string eventName, Event @event)
        {
            Manager.Register(eventName, @event);
        }

        protected virtual void Unregister(string eventName, Event @event)
        {
            Manager.Unregister(eventName, @event);
        }

        public abstract void ResetActor();

        public abstract void InitializeActor();
    }
}