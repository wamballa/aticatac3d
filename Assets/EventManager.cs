using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;   

public class EventManager : MonoBehaviour
{
    public UnityEvent<string> onDropPickup;


}

// EVENT MANAGER

//using UnityEngine.Events;

//public class MyEventManager : MonoBehaviour
//{
//    // Event with parameters
//    public UnityEvent<int, string> onSimpleEventWithParameters;

//    // Event without parameters
//    public UnityEvent onSimpleEventWithoutParameters;
//}


// LISTENER

//public MyEventManager eventManager;

//void Start()
//{
//    eventManager.onSimpleEventWithParameters.AddListener(MyMethodWithParameters);
//    eventManager.onSimpleEventWithoutParameters.AddListener(MyMethodWithoutParameters);
//}

//void MyMethodWithParameters(int num, string text)
//{
//    Debug.Log($"Simple event with parameters raised! Number: {num}, Text: {text}");
//}

//void MyMethodWithoutParameters()
//{
//    Debug.Log("Simple event without parameters raised!");
//}

// TRIGGER

//// Trigger event with parameters
//eventManager.onSimpleEventWithParameters.Invoke(10, "Hello world");

//// Trigger event without parameters
//eventManager.onSimpleEventWithoutParameters.Invoke();
