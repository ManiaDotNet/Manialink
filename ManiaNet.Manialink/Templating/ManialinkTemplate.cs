using RazorEngine.Templating;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ManiaNet.Manialink.Templating
{
    /// <summary>
    /// The base for manialink templates.
    /// </summary>
    /// <typeparam name="TModel">The type of the Model.</typeparam>
    public class ManialinkTemplate<TModel> : TemplateBase<TModel>
    {
        /// <summary>
        /// Gets or sets an object containing arbitrary information used by the view.
        /// </summary>
        public TModel Model { get; set; }

        /// <summary>
        /// Gets or sets a collection of styles used for different elements in the manialink.
        /// </summary>
        public StyleCollection Styles { get; set; }
    }
}