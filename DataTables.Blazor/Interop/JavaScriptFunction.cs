using System;

namespace DataTables.Blazor.Interop;

/// <summary>
/// Represents a JavaScript function.
/// </summary>
public class JavaScriptFunction
{
    /// <summary>
    /// The namespace containing a JavaScript function.
    /// </summary>
    public string Namespace { get; }

    /// <summary>
    /// The name of the JavaScript function.
    /// </summary>
    public string Function { get; }

    /// <summary>
    /// Creates a new instance of <see cref="JavaScriptFunction"/>.
    /// </summary>
    /// <param name="nameSpace">A JavaScript namespace containing the provided function (see <see cref="Namespace"/> for details).</param>
    /// <param name="function">The name of a JavaScript function (see <see cref="Function"/> for details).</param>
    public JavaScriptFunction(string nameSpace, string function)
    {
        if (string.IsNullOrWhiteSpace(nameSpace))
        {
            throw new ArgumentException("Namespace for function must be provided!", nameof(nameSpace));
        }
        else if (string.IsNullOrWhiteSpace(function))
        {
            throw new ArgumentException("A function name must be provided!", nameof(function));
        }

        Namespace = nameSpace;
        Function = function;
    }

    /// <summary>
    /// Converts a string to a <see cref="JavaScriptFunction"/> implicitly.
    /// </summary>
    /// <param name="javaScriptFunction">The namespace and name of a JavaScript function to be called, separated by a point (see <see cref="javaScriptFunction"/> for details).</param>
    public static implicit operator JavaScriptFunction(string javaScriptFunction)
    {
        var split = javaScriptFunction.Split('.');
        if (split.Length != 2)
        {
            throw new ArgumentException("Javascript function must be composed of a namespace and a function separated by a '.'", nameof(javaScriptFunction));
        }

        return new JavaScriptFunction(split[0], split[1]);
    }
}