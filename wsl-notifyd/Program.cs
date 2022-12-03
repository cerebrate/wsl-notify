using System;
using System.Threading.Tasks;

using Tmds.DBus;

using ArkaneSystems.Wsl.Notify.DBus;

namespace ArkaneSystems.Wsl.Notify
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var connection = new Connection(Address.Session!);
            await connection.ConnectAsync();
            await connection.RegisterObjectAsync(new WslNotifier());
            await connection.RegisterServiceAsync ("org.freedesktop.Notifications");

            Console.WriteLine ("Monitoring for notifications. Press CTRL-C to stop.");

            await Task.Delay(int.MaxValue);
        }
    }
}