namespace GGJ2023.Audio
{
    public class AudioPlayData
    {
        public float volume, pitch;
        public bool loop;

        private AudioPlayData(Builder builder)
        {
            this.volume = builder.volume;
            this.pitch = builder.pitch;
            this.loop = builder.loop;
        }
        public class Builder
        {
            public float volume, pitch;
            public bool loop;

            public Builder(float volume, float pitch, bool loop)
            {
                this.volume = volume;
                this.pitch = pitch;
                this.loop = loop;
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
            public AudioPlayData build(){
                return new AudioPlayData(this);
            }
        }
    }
}