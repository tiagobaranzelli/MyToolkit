//-----------------------------------------------------------------------
// <copyright file="IControlGenerator.cs" company="MyToolkit">
//     Copyright (c) Rico Suter. All rights reserved.
// </copyright>
// <license>http://mytoolkit.codeplex.com/license</license>
// <author>Rico Suter, mail@rsuter.com</author>
//-----------------------------------------------------------------------

using MyToolkit.Html;
#if WINRT
using Windows.UI.Xaml;
#else
using System.Windows;
#endif

namespace MyToolkit.Controls.Html
{
    /// <summary>The interface of a control generator.</summary>
    public interface IControlGenerator
	{
        /// <summary>Creates the UI elements for the given HTML node and HTML view.</summary>
        /// <param name="node">The HTML node.</param>
        /// <param name="textBlock">The HTML view.</param>
        /// <returns>The UI elements.</returns>
        DependencyObject[] CreateControls(HtmlNode node, IHtmlView textBlock);
	}
}