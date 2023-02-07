 using UnityEngine;

 namespace GGJ2023.Audio
 {
     [System.Serializable]
     public class AudioData
     {

         public float volume = 1, pitch = 1;
         public AudioClip audioClip;

         public AudioData(AudioClip clip)
         {
             audioClip = clip;
         }
     }
 }