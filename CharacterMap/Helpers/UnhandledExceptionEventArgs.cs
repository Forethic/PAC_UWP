using System;

namespace CharacterMap.Helpers
{
    public class UnhandledExceptionEventArgs : EventArgs
    {
        public bool Handled { get; set; }

        public Exception Exception { get; set; }
    }
}