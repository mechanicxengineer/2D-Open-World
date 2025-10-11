using System;
using UnityEngine;

[CreateAssetMenu]
public class BoolValue : ScriptableObject, ISerializationCallbackReceiver
{
	public bool initialValue;
	[NonSerialized]
	public bool runtimeValue;
    public void OnAfterDeserialize() => runtimeValue = initialValue;

    public void OnBeforeSerialize() { }
}