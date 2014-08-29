using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MyToolkit.Paging.Handlers
{
    /// <summary>Registers for the hardware back key button on Windows Phone and calls the registered methods when the event occurs. </summary>
    public class BackKeyPressedHandler
    {
        private Type _hardwareButtonsType = null;
        private EventInfo _backPressedEvent = null;

        private readonly List<Tuple<MtPage, Func<object, bool>>> _handlers;
        private object _registrationToken;

        private bool _isEventRegistered = false;

        public BackKeyPressedHandler()
        {
            _handlers = new List<Tuple<MtPage, Func<object, bool>>>();
        }

        /// <summary>Adds a back key handler for a given page. </summary>
        /// <param name="page">The page. </param>
        /// <param name="handler">The handler. </param>
        public void Add(MtPage page, Func<object, bool> handler)
        {
            if (!_isEventRegistered)
            {
                if (_hardwareButtonsType == null)
                {
                    _hardwareButtonsType = Type.GetType(
                        "Windows.Phone.UI.Input.HardwareButtons, " +
                        "Windows, Version=255.255.255.255, Culture=neutral, " +
                        "PublicKeyToken=null, ContentType=WindowsRuntime");

                    _backPressedEvent = _hardwareButtonsType.GetRuntimeEvent("BackPressed");
                }

                Action<object, object> callback = OnBackKeyPressed;
                var callbackMethodInfo = callback.GetMethodInfo();
                var backPressedDelegate = callbackMethodInfo.CreateDelegate(_backPressedEvent.EventHandlerType, this);

                _registrationToken = _backPressedEvent.AddMethod.Invoke(null, new object[] { backPressedDelegate });
                _isEventRegistered = true;
            }

            _handlers.Insert(0, new Tuple<MtPage, Func<object, bool>>(page, handler));
        }

        /// <summary>Removes a back key pressed handler for a given page. </summary>
        /// <param name="page">The page. </param>
        public void Remove(MtPage page)
        {
            _handlers.Remove(_handlers.Single(h => h.Item1 == page));

            if (_handlers.Count == 0)
            {
                _backPressedEvent.RemoveMethod.Invoke(null, new[] { _registrationToken });
                _isEventRegistered = false; 
            }
        }

        private void OnBackKeyPressed(object sender, dynamic args)
        {
            if (args.Handled)
                return;

            foreach (var item in _handlers)
            {
                args.Handled = item.Item2(sender);
                if (args.Handled)
                    return;
            }
        }
    }
}