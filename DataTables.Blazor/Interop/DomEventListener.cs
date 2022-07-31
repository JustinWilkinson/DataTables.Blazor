using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataTables.Blazor.Interop
{
    /// <summary>
    /// Controls the events between .NET and JS for DataTables.Blazor.
    /// </summary>
    public interface IDomEventListener
    {
        /// <summary>
        /// Add event listener for datatable.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dom"></param>
        /// <param name="eventName"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        Task AddAsync<T>(ElementReference dom, string eventName, Action<T> callback);

        /// <summary>
        /// Clear all listeners and dispose.
        /// </summary>
        void Dispose();

        /// <summary>
        /// Remove event listener by name
        /// </summary>
        /// <param name="dom"></param>
        /// <param name="eventName"></param>
        void Remove(object dom, string eventName);
    }

    public class DomEventListener : IDomEventListener
    {
        private Dictionary<string, IDisposable> _dotNetObjectStore = new Dictionary<string, IDisposable>();
        private readonly IDataTablesInterop _jsRuntime;
        private readonly string _id;

        public DomEventListener(IDataTablesInterop jsRuntime)
        {
            _jsRuntime = jsRuntime;
            _id = Guid.NewGuid().ToString();
        }

        /// <inheritdoc/>
        public async Task AddAsync<T>(ElementReference dom, string eventName, Action<T> callback)
        {
            if (string.IsNullOrEmpty(eventName))
            {
                throw new ArgumentNullException(nameof(eventName));
            }

            var key = FormatKey(dom, eventName);
            if (_dotNetObjectStore.ContainsKey(key))
            {
                return;
            }

            var dotNetObject = DotNetObjectReference.Create(new Invoker<T>(callback));

            await _jsRuntime.AddEventListenerAsync(dom, eventName, dotNetObject);

            _dotNetObjectStore.Add(key, dotNetObject);
        }

        /// <inheritdoc/>
        public void Remove(object dom, string eventName)
        {
            var key = FormatKey(dom, eventName);
            if (_dotNetObjectStore.TryGetValue(key, out IDisposable value))
            {
                value?.Dispose();
            }
            _dotNetObjectStore.Remove(key);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            foreach (var (k, v) in _dotNetObjectStore)
            {
                v?.Dispose();
            }
            _dotNetObjectStore.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dom"></param>
        /// <param name="eventName"></param>
        /// <returns></returns>
        private string FormatKey(object dom, string eventName)
        {
            var selector = dom is ElementReference eleRef ? eleRef.Id : dom.ToString();
            return $"DEL-{_id}-{selector}-{eventName}";
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Invoker<T>
    {
        private readonly Action<T> _action;

        public Invoker(Action<T> invoker)
        {
            _action = invoker;
        } 

        [JSInvokable]
        public void Invoke(T args)
        {
            _action(args); 
        }
    }
}