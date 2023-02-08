using System.Collections.Generic;
using UnityEngine;

namespace GGJ2023.Audio
{
    [CreateAssetMenu(fileName = "AudioDatabase", menuName = "Audio/RootDatabase", order = 0)]
    public class AudioRootDatabaseObject : ScriptableObject
    {
        [HideInInspector]
        public Dictionary<string, AudioDatabaseObject> databaseDictionary;
        public List<AudioDatabaseObject> databaseObjects;

        public void Init()
        {
            databaseDictionary = new Dictionary<string, AudioDatabaseObject>();
            for (int i = 0; i < databaseObjects.Count; i++)
            {
                databaseDictionary.Add(databaseObjects[i].name, databaseObjects[i]);
                databaseObjects[i].Init();
            }
        }

        public bool CheckDatabase(string databaseName)
        {
            return databaseDictionary.ContainsKey(databaseName);
        }
        public AudioDatabaseObject GetDatabase(string databaseName)
        {
            if (CheckDatabase(databaseName))
            {
                return databaseDictionary[databaseName];
            }

            return null;
        }
    }
}