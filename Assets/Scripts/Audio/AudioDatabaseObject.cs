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
        public float patch = 1;
        public List<AudioGroupData> groups;
        [HideInInspector] public List<string> groupNames;

        public void Init()
        {
            groupNames = new List<string>();
            for (int i = 0; i < groups.Count; i++)
            {
                groupNames.Add(groups[i].groupName);
            }
        }

        public bool CheckGroup(string groupName)
        {
            return groupNames.Contains(groupName);
        }
        public AudioGroupData GetGroup(string groupName)
        {
            if (CheckGroup(groupName))
            {
                int index = groupName.IndexOf(groupName);
                return groups[index];
            }

            return null;
        }
    }
}