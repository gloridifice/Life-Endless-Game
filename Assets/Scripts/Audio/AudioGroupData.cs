using System.Collections.Generic;
using UnityEngine;

namespace GGJ2023.Audio
{
    [System.Serializable]
    public class AudioGroupData
    {

        public string groupName;
        public float groupVolume = 1f;
        public float groupPitch = 1f;
        public bool groupLoop = false;
        public PlayModeType groupPlayMode = PlayModeType.Normal;

        public List<AudioData> groupData;
        public int DataCount => groupData.Count;

        // -- 非储存字段
        [HideInInspector]
        private int lastPlayIndex = -1;

        public AudioGroupData(string name)
        {
            groupName = name;
            groupVolume = 1f;
            groupPitch = 1f;
            groupLoop = false;
            groupPlayMode = PlayModeType.Normal;
            groupData = new List<AudioData>();
        }

        public void ResetPlayIndex()
        {
            lastPlayIndex = -1;
        }

        public int MoveNextIndex()
        {
            if (groupPlayMode == PlayModeType.Random)
            {
                lastPlayIndex = Random.Range(0, groupData.Count);
            }
            else
            {
                lastPlayIndex++;
                lastPlayIndex %= DataCount;
                if (lastPlayIndex >= DataCount)
                {
                    lastPlayIndex -= DataCount;
                }
            }

            return lastPlayIndex;
        }

        public void PutAudioData(string dataName, string clipName, AudioClip clip)
        {
            //AudioData data = new AudioData(1,1, clip);
            //groupData.Add(data);
        }

        public AudioData GetNextAudioData()
        {
            if (DataCount > 0)
            {
                int index = MoveNextIndex();
                return groupData[index];
            }

            return null;
        }
    }

    public enum PlayModeType
    {
        Normal,Random
    }
}