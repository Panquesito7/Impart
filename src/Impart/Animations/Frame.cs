namespace Impart
{
    /// <summary>The class for a single Frame part of Animation.</summary>
    public struct Frame
    {
        /// <value>The position of the Frame.</value>
        public readonly Percent Position;

        /// <value>The type of Change.</value>
        public readonly ChangeType ChangeType;

        /// <value>The change to take place in the Frame.</value>
        public readonly object Change;

        /// <summary>Creates a Frame instance.</summary>
        /// <returns>A Frame instance.</returns>
        /// <param name="position">The position value.</param>
        /// <param name="changeType">The type of Change.</param>
        /// <param name="change">The change to take place.</param>
        public Frame(Percent position, ChangeType changeType, object change)
        {
            switch (changeType)
            {
                case ChangeType.BackgroundColor:

                    break;
                case ChangeType.ForegroundColor:
                    break;
                case ChangeType.Width:
                    break;
                case ChangeType.Height:
                    break;
                case ChangeType.Position:
                    break;
            }
            Position = position;
            ChangeType = changeType;
            Change = change;
        }
        /// <summary>Returns the instance as a String.</summary>
        /// <returns>A String instance.</returns>
        public override string ToString()
        {
            switch (ChangeType)
            {
                case ChangeType.BackgroundColor:
                    return $"{Position} {{  }}";
                case ChangeType.ForegroundColor:
                    break;
                case ChangeType.Width:
                    break;
                case ChangeType.Height:
                    break;
                case ChangeType.Position:
                    break;
            }
            return "";
        }
    }
}