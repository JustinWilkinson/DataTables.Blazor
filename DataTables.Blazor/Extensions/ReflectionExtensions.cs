using System;
using System.Reflection;

namespace DataTables.Blazor.Extensions
{
    /// <summary>
    /// Contains helpful extension methods to enhance performance of Reflection.
    /// </summary>
    internal static class ReflectionExtensions
    {
        /// <summary>
        /// Gets the getter for the specified property on the class.
        /// </summary>
        /// <typeparam name="T">Type of instance.</typeparam>
        /// <param name="property">Property to obtain setter of.</param>
        /// <returns>A delegate that invokes the property setter.</returns>
        public static Func<object, object> GetGetter(this PropertyInfo property, Type type) => GetGetDelegate(property.GetMethod, type);

        #region Helpers
        private static readonly MethodInfo _genericGetHelper = typeof(ReflectionExtensions).GetMethod(nameof(GetGetDelegateHelper), BindingFlags.Static | BindingFlags.NonPublic);

        /// <summary>
        /// Creates a type action from a method info.
        /// </summary>
        /// <typeparam name="S">Type of instance</typeparam>
        /// <typeparam name="T">Type of property</typeparam>
        /// <param name="method">Set method</param>
        /// <returns>Typed setter</returns>
        private static Action<S, T> CreateTypedAction<S, T>(MethodInfo method) => (Action<S, T>)Delegate.CreateDelegate(typeof(Action<S, T>), method);


        /// <summary>
        /// Helper method that returns a getter for a property using the object class.
        /// </summary>
        /// <typeparam name="T">Type of instance</typeparam>
        /// <param name="method">Method Info to pass</param>
        /// <returns></returns>
        private static Func<object, object> GetGetDelegate(MethodInfo method, Type type)
        {
            // Supply the type arguments to the generic helper.
            MethodInfo oConstructedHelper = _genericGetHelper.MakeGenericMethod(type, method.ReturnType);

            // Cast the result to the right kind of delegate and return it. The null argument is because it's a static method
            return (Func<object, object>)oConstructedHelper.Invoke(null, new[] { method });
        }

        /// <summary>
        /// Generic helper method that creates a more weakly typed delegate that will call a strongly typed one.
        /// </summary>
        /// <typeparam name="TTarget">Target type</typeparam>
        /// <typeparam name="TParam">Parameter type</typeparam>
        /// <param name="method">Get Method</param>
        /// <returns>A weakly typed delegate that calls a strongly type one.</returns>
        private static Func<object, object> GetGetDelegateHelper<TTarget, TParam>(MethodInfo method) where TTarget : class
        {
            // Convert the slow MethodInfo into a fast, strongly typed, open delegate
            Func<TTarget, TParam> func = CreateTypedFunction<TTarget, TParam>(method);

            // Now create a more weakly typed delegate which will call the strongly typed one
            return target => (TParam)Convert.ChangeType(func((TTarget)Convert.ChangeType(target, typeof(TTarget))), typeof(TParam));
        }

        /// <summary>
        /// Creates a typed function from a method info.
        /// </summary>
        /// <typeparam name="S">Type of instance</typeparam>
        /// <typeparam name="T">Type of property</typeparam>
        /// <param name="method">Get method</param>
        /// <returns>Typed getter</returns>
        private static Func<S, T> CreateTypedFunction<S, T>(MethodInfo method) => (Func<S, T>)Delegate.CreateDelegate(typeof(Func<S, T>), method);
        #endregion
    }
}