namespace DJCWebApi.Areas.HelpPage
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class ImageSample
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Src>k__BackingField;

        public ImageSample(string src)
        {
            if (src == null)
            {
                throw new ArgumentNullException("src");
            }
            this.Src = src;
        }

        public override bool Equals(object obj)
        {
            ImageSample sample = obj as ImageSample;
            return ((sample != null) && (this.Src == sample.Src));
        }

        public override int GetHashCode() => 
            this.Src.GetHashCode();

        public override string ToString() => 
            this.Src;

        public string Src { get; private set; }
    }
}

