using UnityEngine.Audio;

namespace GGJ2023.Audio
{
    public class AudioPlayData
    {
        public float volume, pitch;
        public bool loop, fade, stopOnSceneChange;
        public AudioMixerGroup mixerGroup;

        private AudioPlayData(Builder builder)
        {
            this.volume = builder.volume;
            this.pitch = builder.pitch;
            this.loop = builder.loop;
            this.mixerGroup = builder.mixerGroup;
            this.fade = builder.fade;
            this.stopOnSceneChange = builder.stopOnSceneChange;

        }
        public class Builder
        {
            public float volume, pitch;
            public bool loop, fade, stopOnSceneChange;
            public AudioMixerGroup mixerGroup;

            public Builder(float volume, float pitch, bool loop, AudioMixerGroup group, bool fade)
            {
                this.volume = volume;
                this.pitch = pitch;
                this.loop = loop;
                this.mixerGroup = group;
                this.fade = fade;
            }

            public Builder()
            {
                volume = 1;
                pitch = 1;
                loop = false;
                fade = false;
                stopOnSceneChange = true;
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
            public Builder Fade(bool doFade)
            {
                this.fade = doFade;
                return this;
            }
            public Builder StopOnSceneChanged(bool stopOnChanged)
            {
                this.stopOnSceneChange = stopOnChanged;
                return this;
            }
            public AudioPlayData build(){
                return new AudioPlayData(this);
            }
        }
    }
}