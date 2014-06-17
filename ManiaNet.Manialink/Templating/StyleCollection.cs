using System;
using System.Collections.Generic;
using System.Linq;

namespace ManiaNet.Manialink.Templating
{
    /// <summary>
    /// Represents information about the styles used for different elements in a manialink.
    /// </summary>
    public class StyleCollection
    {
        /// <summary>
        /// Gets or sets the style for a message box quad.
        /// </summary>
        public string MessageBox { get; set; }

        /// <summary>
        /// Gets or sets the style for a Button in a message box.
        /// </summary>
        public string MessageBoxButton { get; set; }

        // TODO: Add more; this is just an example.
    }
}