using System.Collections.Concurrent;
using System.ComponentModel;
using System.IO.Ports;

namespace Grapp.AppBase.Std.Library.Serial
{
    [Localizable(isLocalizable: false)]
    public class XSerialProviderPool
    {
        public static readonly XSerialProviderPool Instance = new XSerialProviderPool();

        private readonly ConcurrentDictionary<string, SerialProvider> _data;

        protected XSerialProviderPool()
        {
            _data = new ConcurrentDictionary<string, SerialProvider>();
        }

        private static string CreateKey(string portName, int baudRate, int dataBits, Parity parity, StopBits stopBits) => $"{portName}.{baudRate}.{dataBits}.{parity}.{stopBits}";

        public SerialProvider Get(string portName, int baudRate, int dataBits, Parity parity, StopBits stopBits)
        {
            return _data.GetOrAdd(
                key: CreateKey(portName, baudRate, dataBits, parity, stopBits),
                lKey =>
                    {
                        var provider = new SerialProvider();

                        provider.Configure(portName, baudRate, dataBits, parity, stopBits);
                        provider.Disposed += (sender, args) => _data.TryRemove(lKey, out _);

                        return provider;
                    });
        }
    }
}