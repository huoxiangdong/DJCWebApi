namespace DJCWebApi.Areas.HelpPage
{
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class InvalidSample
    {
        [CompilerGenerated, DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string <ErrorMessage>k__BackingField;

        public InvalidSample(string errorMessage)
        {
            if (errorMessage == null)
            {
                throw new ArgumentNullException("errorMessage");
            }
            this.ErrorMessage = errorMessage;
        }

        public override bool Equals(object obj)
        {
            InvalidSample sample = obj as InvalidSample;
            return ((sample != null) && (this.ErrorMessage == sample.ErrorMessage));
        }

        public override int GetHashCode() => 
            this.ErrorMessage.GetHashCode();

        public override string ToString() => 
            this.ErrorMessage;

        public string ErrorMessage { get; private set; }
    }
}

