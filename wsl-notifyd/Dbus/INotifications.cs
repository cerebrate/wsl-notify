using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Tmds.DBus;

[assembly: InternalsVisibleTo(Tmds.DBus.Connection.DynamicAssemblyName)]
namespace ArkaneSystems.Wsl.Notify.Dbus
{
    [DBusInterface("org.freedesktop.Notifications")]
    interface INotifications : IDBusObject
    {
        Task<(string name, string vendor, string version, string specVersion)> GetServerInformationAsync();
        Task CloseNotificationAsync(uint Id);
        Task<uint> NotifyAsync(string AppName, uint ReplacesId, string AppIcon, string Summary, string Body, string[] Actions, IDictionary<string, object> Hints, int ExpireTimeout);
        Task<string[]> GetCapabilitiesAsync();
        Task<IDisposable> WatchActionInvokedAsync(Action<(uint, string)> handler, Action<Exception> onError = null);
        Task<IDisposable> WatchNotificationClosedAsync(Action<(uint, uint)> handler, Action<Exception> onError = null);
    }
}
