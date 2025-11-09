using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class GameSaveManager : MonoBehaviour
{
	public List<ScriptableObject> objects = new List<ScriptableObject>();
	private void OnEnable() { LoadScriptableObjects(); }
	private void OnDisable() { SaveScriptableObjects(); }

	public void ResetScriptablesObjects()
	{
		for (int i = 0; i < objects.Count; i++)
		{
			if (File.Exists(Application.persistentDataPath + string.Format("/{0}.dat", i)))
			{
				File.Delete(Application.persistentDataPath + string.Format("/{0}.dat", i));
			}
		}
	}

	public void SaveScriptableObjects()
	{
		for (int i = 0; i < objects.Count; i++)
		{
			FileStream file = File.Create(Application.persistentDataPath + string.Format("/{0}.dat", i));
			BinaryFormatter bf = new BinaryFormatter();
			var json = JsonUtility.ToJson(objects[i]);
			bf.Serialize(file, json);
			file.Close();
		}
	}

	public void LoadScriptableObjects()
	{
		for (int i = 0; i < objects.Count; i++)
		{
			if (File.Exists(Application.persistentDataPath + string.Format("/{0}.dat", i)))
			{
				FileStream file = File.Open(Application.persistentDataPath + string.Format("/{0}.dat", i), FileMode.Open);
				BinaryFormatter bf = new BinaryFormatter();
				JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), objects[i]);
				file.Close();
			}
		}
	} 
}