using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class InventorySaver : MonoBehaviour
{
	[SerializeField] private PlayerInventory playerInventory;

	private void OnEnable()
	{
		playerInventory.myInventory.Clear();
		LoadScriptableObjects(); 
	}
	private void OnDisable() { SaveScriptableObjects(); }

    public void ResetScriptablesObjects()
	{
		int i = 0;
		while (File.Exists(Application.persistentDataPath + string.Format("/{0}.inv", i)))
		{
			File.Delete(Application.persistentDataPath + string.Format("/{0}.inv", i));
			i++;
		}
	}

	public void SaveScriptableObjects()
	{
		ResetScriptablesObjects();
		for (int i = 0; i < playerInventory.myInventory.Count; i++)
		{
			FileStream file = File.Create(Application.persistentDataPath + string.Format("/{0}.inv", i));
			BinaryFormatter bf = new BinaryFormatter();
			var json = JsonUtility.ToJson(playerInventory.myInventory[i]);
			bf.Serialize(file, json);
			file.Close();
		}
	}

	public void LoadScriptableObjects()
	{
		int i = 0;
		while (File.Exists(Application.persistentDataPath + string.Format("/{0}.inv", i)))
		{
			var temp = ScriptableObject.CreateInstance<InventoryItem>();
			Debug.Log(Application.persistentDataPath + string.Format("/{0}.inv", i));
			FileStream file = File.Open(Application.persistentDataPath + string.Format("/{0}.inv", i), FileMode.Open);
			BinaryFormatter bf = new BinaryFormatter();
			JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), temp);
			file.Close();
			playerInventory.myInventory.Add(temp);
			i++;
		}

	} 
}