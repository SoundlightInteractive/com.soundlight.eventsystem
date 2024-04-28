using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace SoundlightInteractive.EventSystem
{
    [DefaultExecutionOrder(-1001)]
    public class EventManager : MonoBehaviour
    {
        public static EventManager Instance;

        private Dictionary<string, List<Event>> _events = new();

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            else
            {
                Destroy(gameObject);
            }

            gameObject.hideFlags = HideFlags.HideInHierarchy;

#if UNITY_EDITOR

            EditorApplication.RepaintHierarchyWindow();

#endif
        }

        public void Push(string eventName, params object[] arguments)
        {
            if (!_events.ContainsKey(eventName))
            {
                return;
            }

            List<KeyValuePair<string, List<Event>>> events = _events.Where(x => x.Key == eventName).ToList();

            foreach (Event @event in events.Select(myEvent => myEvent.Value.ToList()).SelectMany(eventList => eventList))
            {
                @event?.Invoke(arguments);
            }
        }

        public void Register(string eventName, Event @event)
        {
            List<Event> events;

            if (!_events.ContainsKey(eventName))
            {
                events = new List<Event> { @event };
                _events.Add(eventName, events);
            }

            else
            {
                events = _events[eventName];
                events.Add(@event);
            }
        }

        public void Unregister(string eventName, Event @event)
        {
            List<Event> events = _events[eventName];

            if (events.Count > 0)
            {
                events.Remove(@event);
            }

            if (events.Count == 0)
            {
                _events.Remove(eventName);
            }
        }
    }
}