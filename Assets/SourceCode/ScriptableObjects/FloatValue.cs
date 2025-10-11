using System;
using System.Collections;
using UnityEngine;


[CreateAssetMenu]
public class FloatValue : ScriptableObject, ISerializationCallbackReceiver
{
	public float initialValue;
	[NonSerialized]
	public float runtimeValue;
    public void OnAfterDeserialize() => runtimeValue = initialValue;

    public void OnBeforeSerialize() { }
}