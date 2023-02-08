using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace GGJ2023.Audio
{
    [CreateAssetMenu(fileName = "AudioDatabase", menuName = "Audio/Database", order = 0)]
    public class AudioDatabaseObject : ScriptableObject
    {
        public string name;
        public float volume = 1;
        public float pitch = 1;
        public List<AudioGroupData> groups;
        [HideInInspector] public Dictionary<string, AudioGroupData> groupDictionary;

        public void Init()
        {
            groupDictionary = new Dictionary<string, AudioGroupData>();
            for (int i = 0; i < groups.Count; i++)
            {
                groupDictionary.Add(groups[i].groupName, groups[i]);
            }
        }

        public bool CheckGroup(string groupName)
        {
            return groupDictionary.ContainsKey(groupName);
        }
        public AudioGroupData GetGroup(string groupName)
        {
            if (CheckGroup(groupName))
            {
                return groupDictionary[groupName];
            }
            return null;
        }
    }
}