using System.Collections.Generic;
using UnityEngine;

namespace GGJ2023.Audio
{
    [CreateAssetMenu(fileName = "AudioDatabase", menuName = "Audio/RootDatabase", order = 0)]
    public class AudioRootDatabaseObject : ScriptableObject
    {
        [HideInInspector]
        public List<string> databaseNames;
        public List<AudioDatabaseObject> databaseObjects;

        public void Init()
        {
            for (int i = 0; i < databaseObjects.Count; i++)
            {
                databaseNames.Add(databaseObjects[i].name);
                databaseObjects[i].Init();
            }
        }

        public bool CheckDatabase(string databaseName)
        {
            return databaseNames.Contains(databaseName);
        }
        public AudioDatabaseObject GetDatabase(string databaseName)
        {
            if (CheckDatabase(databaseName))
            {
                return databaseObjects[databaseNames.IndexOf(databaseName)];
            }

            return null;
        }
    }
}