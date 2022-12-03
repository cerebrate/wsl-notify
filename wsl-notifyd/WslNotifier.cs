using System;
using System.Threading.Tasks;

using Tmds.DBus;

using ArkaneSystems.Wsl.Notify.DBus;

namespace ArkaneSystems.Wsl.Notify
{
    class WslNotifier: INotifications
    {
        public static readonly ObjectPath Path = new ObjectPath("/org/freedesktop/Notifications");

        public static uint NotificationId = 0;

        public ObjectPath ObjectPath => WslNotifier.Path;

        public Task<(string name, string vendor, string version, string specVersion)> GetServerInformationAsync()
        {
            return Task.FromResult(("WslNotifier", "Arkane Systems", "0.1", "1.2"));
        }
        
        public Task<string[]> GetCapabilitiesAsync()
        {
            return Task.FromResult(new string [] { "x-arkanesystems-wsl" });
        }

        public Task<uint> NotifyAsync(string AppName, uint ReplacesId, string AppIcon, string Summary, string Body, string[] Actions,
            IDictionary<string, object> Hints, int ExpireTimeout)
        {
            Console.WriteLine ("Notification received:");
            Console.WriteLine ($"   appname        = {AppName}");
            Console.WriteLine ($"   replaces_id    = {ReplacesId}");
            Console.WriteLine ($"   appicon        = {AppIcon}");
            Console.WriteLine ($"   summary        = {Summary}");
            Console.WriteLine ($"   body           = {Body}");
            Console.WriteLine ($"   actions        = {Actions.Length}");
            Console.WriteLine ($"   hints          = {Hints.Keys.Count}");
            Console.WriteLine ($"   expire_timeout = {ExpireTimeout}");
            Console.WriteLine ();

            if (ReplacesId == 0)
                return Task.FromResult(++WslNotifier.NotificationId);
            else
                return Task.FromResult(ReplacesId);
        }

        public Task CloseNotificationAsync(uint Id)
        {
            // no-op for now
            return Task.CompletedTask;
        }

        // Signals and handlers.
        public event Action<(uint, uint)> OnNotificationClosed;
        public event Action<(uint, string)> OnActionInvoked;

        public Task<IDisposable> WatchNotificationClosedAsync(Action<(uint, uint)> handler, Action<Exception> onError = null)
        {
            return SignalWatcher.AddAsync(this, nameof(OnNotificationClosed), handler);
        }
   
        public Task<IDisposable> WatchActionInvokedAsync(Action<(uint, string)> handler, Action<Exception> onError = null)
        {
            return SignalWatcher.AddAsync(this, nameof(OnActionInvoked), handler);
        }
    }
}