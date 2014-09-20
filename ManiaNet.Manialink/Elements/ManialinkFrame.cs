using System.Collections.Generic;

namespace ManiaNet.Manialink.Elements
{
    internal class ManialinkFrame : ManialinkElement
    {
        public ManialinkFrame()
            : base(ManialinkElement.ElementType.Frame)
        {
            this.Closed = false;
            this.validFields.Remove("scriptevents");
        }

        /// <summary>
        /// Add a ManialinkElement as child.
        /// </summary>
        /// <param name="child">The child element to add.</param>
        /// <returns>This.</returns>
        public ManialinkElement AddChild(ManialinkElement child)
        {
            if (child != null)
                this.children.Add(child);
            return this;
        }

        /// <summary>
        /// Remove a child ManialinkElement.
        /// </summary>
        /// <param name="child">The child element to remove.</param>
        /// <returns>This.</returns>
        public ManialinkElement RemoveChild(ManialinkElement child)
        {
            this.children.Remove(child);
            return this;
        }

        /// <summary>
        /// Returns all child-elements.
        /// </summary>
        /// <returns>A IEnumerable of the child-elements.</returns>
        public IEnumerable<ManialinkElement> Children()
        {
            return this.children;
        }
    }
}