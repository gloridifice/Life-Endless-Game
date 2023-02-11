using UnityEngine.Audio;

namespace GGJ2023.Audio
{
    public class AudioPlayData
    {
        public float volume, pitch;
        public bool loop;
        public AudioMixerGroup mixerGroup;

        private AudioPlayData(Builder builder)
        {
            this.volume = builder.volume;
            this.pitch = builder.pitch;
            this.loop = builder.loop;
            this.mixerGroup = builder.mixerGroup;
        }
        public class Builder
        {
            public float volume, pitch;
            public bool loop;
            public AudioMixerGroup mixerGroup;

            public Builder(float volume, float pitch, bool loop, AudioMixerGroup group)
            {
                this.volume = volume;
                this.pitch = pitch;
                this.loop = loop;
                this.mixerGroup = group;
            }

            public Builder()
            {
                volume = 1;
                pitch = 1;
                loop = false;
            }
            public Builder Volume(float value)
            {
                this.volume = value;
                return this;
            }
            public Builder Pitch(float value)
            {
                this.pitch = value;
                return this;
            }

            public Builder Loop(bool value)
            {
                this.loop = value;
                return this;
            }

            public Builder MixerGroup(AudioMixerGroup group)
            {
                this.mixerGroup = group;
                return this;
            }
            public AudioPlayData build(){
                return new AudioPlayData(this);
            }
        }
    }
}