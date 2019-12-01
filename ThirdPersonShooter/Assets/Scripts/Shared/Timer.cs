using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    class TimedEvent
    {
        public float timeToExecute;
        public Callback Method;
    }

    List<TimedEvent> events;
    public delegate void Callback();

    void Awake()
    {
        events = new List<TimedEvent>();
    }

    public void Add(Callback method, float inSeconds)
    {
        events.Add(new TimedEvent
        {
            Method = method,
            timeToExecute = Time.time + inSeconds
        });
    }

    // Update is called once per frame
    void Update()
    {
        if (events.Count == 0)
            return;

        for(int i = 0; i < events.Count; i++)
        {
            var timedEvent = events[i];
            if(timedEvent.timeToExecute <= Time.time)
            {
                timedEvent.Method();
                events.Remove(timedEvent);
            }
        }
    }
}
