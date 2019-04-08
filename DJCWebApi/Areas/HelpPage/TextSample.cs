namespace DJCWebApi.Areas.HelpPage
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class TextSample
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <Text>k__BackingField;

        public TextSample(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException("text");
            }
            this.Text = text;
        }

        public override bool Equals(object obj)
        {
            TextSample sample = obj as TextSample;
            return ((sample != null) && (this.Text == sample.Text));
        }

        public override int GetHashCode() => 
            this.Text.GetHashCode();

        public override string ToString() => 
            this.Text;

        public string Text { get; private set; }
    }
}

