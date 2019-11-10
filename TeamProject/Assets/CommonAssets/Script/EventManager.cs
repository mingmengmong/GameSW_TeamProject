using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using script;

namespace common {
    /*
    * Copyright 2017 Ben D'Angelo
    *
    * MIT License
    *
    * Permission is hereby granted, free of charge, to any person obtaining a copy of this
    * software and associated documentation files (the "Software"), to deal in the Software
    * without restriction, including without limitation the rights to use, copy, modify, merge,
    * publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons
    * to whom the Software is furnished to do so, subject to the following conditions:
    *
    * The above copyright notice and this permission notice shall be included in all copies or
    * substantial portions of the Software.
    *
    * THE SOFTWARE IS PROVIDED *AS IS*, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
    * INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
    * PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE
    * FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
    * OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
    * DEALINGS IN THE SOFTWARE.
    * 
    * Usage : 
    *   EventManager.Instance.AddListener<NewGameClickedEvent>(OnNewGameClicked);
    *   EventManager.Instance.RemoveListener<NewGameClickedEvent>(OnNewGameClicked);
    *   
    *   EventManager.Instance.TriggerEvent( new NewGameClickedEvent() );
    *   
    */
    using UnityEngine;
    using System.Collections;
    using System.Collections.Generic;

    public class EventManager : MonoBehaviour {
        public bool LimitQueueProcesing = false;
        public float QueueProcessTime = 0.0f;
        private static EventManager _Instance = null;
        private Queue m_eventQueue = new Queue();

        public delegate void EventDelegate<T>(T e) where T : ObjectEvent;
        private delegate void EventDelegate(ObjectEvent e);

        private Dictionary<System.Type, EventDelegate> delegates = new Dictionary<System.Type, EventDelegate>();
        private Dictionary<System.Delegate, EventDelegate> delegateLookup = new Dictionary<System.Delegate, EventDelegate>();
        private Dictionary<System.Delegate, System.Delegate> onceLookups = new Dictionary<System.Delegate, System.Delegate>();


        static bool _initialized = false;


        // override so we don't have the typecast the object
        public static EventManager Instance {
            get {
                if (_Instance == null) {
                    _Instance = GameObject.FindObjectOfType(typeof(EventManager)) as EventManager;                    
                }
                return _Instance;
            }
        }


        public void Awake() {
            if (!_initialized ) {
                _Instance = this;                
                DontDestroyOnLoad(gameObject);
                _initialized = true;
            } else {
                Destroy(gameObject);
            }
            
        }

        private EventDelegate AddDelegate<T>(EventDelegate<T> del) where T : ObjectEvent {
            // Early-out if we've already registered this delegate
            if (delegateLookup.ContainsKey(del))
                return null;

            // Create a new non-generic delegate which calls our generic one.
            // This is the delegate we actually invoke.
            EventDelegate internalDelegate = (e) => del((T)e);
            delegateLookup[del] = internalDelegate;

            EventDelegate tempDel;
            if (delegates.TryGetValue(typeof(T), out tempDel)) {
                delegates[typeof(T)] = tempDel += internalDelegate;
            } else {
                delegates[typeof(T)] = internalDelegate;
            }

            return internalDelegate;
        }

        public void AddListener<T>(EventDelegate<T> del) where T : ObjectEvent {
            AddDelegate<T>(del);
        }

        public void AddListenerOnce<T>(EventDelegate<T> del) where T : ObjectEvent {
            EventDelegate result = AddDelegate<T>(del);

            if (result != null) {
                // remember this is only called once
                onceLookups[result] = del;
            }
        }

        public void RemoveListener<T>(EventDelegate<T> del) where T : ObjectEvent {
            EventDelegate internalDelegate;
            if (delegateLookup.TryGetValue(del, out internalDelegate)) {
                EventDelegate tempDel;
                if (delegates.TryGetValue(typeof(T), out tempDel)) {
                    tempDel -= internalDelegate;
                    if (tempDel == null) {
                        delegates.Remove(typeof(T));
                    } else {
                        delegates[typeof(T)] = tempDel;
                    }
                }

                delegateLookup.Remove(del);
            }
        }

        public void RemoveAll() {
            delegates.Clear();
            delegateLookup.Clear();
            onceLookups.Clear();
        }

        public bool HasListener<T>(EventDelegate<T> del) where T : ObjectEvent {
            return delegateLookup.ContainsKey(del);
        }

        public void TriggerEvent(ObjectEvent e) {
            EventDelegate del;
            if (delegates.TryGetValue(e.GetType(), out del)) {
                del.Invoke(e);

                // remove listeners which should only be called once
                foreach (EventDelegate k in delegates[e.GetType()].GetInvocationList()) {
                    if (onceLookups.ContainsKey(k)) {
                        delegates[e.GetType()] -= k;

                        if (delegates[e.GetType()] == null) {
                            delegates.Remove(e.GetType());
                        }

                        delegateLookup.Remove(onceLookups[k]);
                        onceLookups.Remove(k);
                    }
                }
            } else {
                //Debug.LogWarning("Event: " + e.GetType() + " has no listeners");
            }
        }

        //Inserts the event into the current queue.
        public bool QueueEvent(ObjectEvent evt) {
            if (!delegates.ContainsKey(evt.GetType())) {
                Debug.LogWarning("EventManager: QueueEvent failed due to no listeners for event: " + evt.GetType());
                return false;
            }

            m_eventQueue.Enqueue(evt);
            return true;
        }

        //Every update cycle the queue is processed, if the queue processing is limited,
        //a maximum processing time per update can be set after which the events will have
        //to be processed next update loop.
        void Update() {
            float timer = 0.0f;
            while (m_eventQueue.Count > 0) {
                if (LimitQueueProcesing) {
                    if (timer > QueueProcessTime)
                        return;
                }

                ObjectEvent evt = m_eventQueue.Dequeue() as ObjectEvent;
                TriggerEvent(evt);

                if (LimitQueueProcesing)
                    timer += Time.deltaTime;
            }
        }

        public void OnApplicationQuit() {
            RemoveAll();
            m_eventQueue.Clear();
            _Instance = null;
        }

        


        public IEnumerator TriggerEventWithPostProcess<T>(IFEventPostProcessProvider eventPostProcessProvider, T hookableEvent) where T : ObjectEventInvolvingPostProcess {
            TriggerEvent(hookableEvent);

            GameObject[] postProcesses = eventPostProcessProvider.GetPostProcessors(hookableEvent);
            if (postProcesses != null && postProcesses.Length > 0) {
                yield return StartCoroutine(EventPostProcess(hookableEvent, postProcesses));
            }
        }

        public IEnumerator TriggerEventWithPostProcess<T>(IFEventPostProcessProvider eventPostProcessProvider, T hookableEvent, Action callback) where T : ObjectEventInvolvingPostProcess {
            yield return StartCoroutine(TriggerEventWithPostProcess(eventPostProcessProvider, hookableEvent));
            if (callback != null) {
                callback();
            }
        }

        IEnumerator EventPostProcess<T>(T slotEvent , GameObject[] postProcesses) where T : ObjectEventInvolvingPostProcess {           
            
            foreach (GameObject obj in postProcesses) {
                IFEventPostProcess<T> postProcess = obj.GetInterface<IFEventPostProcess<T>>();
                if (postProcess == null) {
                    Debug.LogWarning(obj + " was not implements IFEventPostProcess<" + typeof(T).FullName + ">");
                } else {
                    yield return StartCoroutine(postProcess.PostProcess_(slotEvent));
                }

            }
            
        }



    }
}