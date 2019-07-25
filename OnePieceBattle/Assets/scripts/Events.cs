using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable] 
public class ObjectEvent : UnityEvent<GameObject> { }
[System.Serializable]
public class BoolEvent : UnityEvent<bool> { }