using System;
using System.IO;
using System.IO.Ports;
using System.Threading;
using Grapp.AppBase.Std.Library.Events;

namespace Grapp.AppBase.Std.Library.Serial
{
	public class SerialProvider : ISerialProvider
	{
		private readonly MemoryStream _buffer;
		private readonly SerialPort _serial;

		private bool _disposed;
		private long _identityID;

		public SerialProvider()
		{
			_serial = new SerialPort();
			_buffer = new MemoryStream();

			BindingEvents();
		}

		public bool IsOpen => _serial.IsOpen;

		public event EventHandler<DataEventArgs<byte[]>> DataReceived;

		public bool Close()
		{
			if(!IsOpen)
			{
				return false;
			}

			_serial.Close();

			return true;
		}

		public void Configure(string portName, int baudRate, int dataBits, Parity parity, StopBits stopBits)
		{
			_serial.PortName = portName;
			_serial.BaudRate = baudRate;
			_serial.DataBits = dataBits;
			_serial.Parity = parity;
			_serial.StopBits = stopBits;

			_serial.ReadTimeout = 500;
		}

		public long NewIdentity()
		{
			return Interlocked.Increment(ref _identityID);
		}

		public bool Open()
		{
			if(IsOpen)
			{
				return false;
			}

			_serial.Open();
			return true;
		}

		public void Write(byte[] data)
		{
			_serial.Write(data, offset: 0, data.Length);
		}

		private void BindingEvents()
		{
			_serial.DataReceived +=
				(sender, args) =>
					{
                        _buffer.SetLength(value: 0);

                        try
                        {
                            var v = _serial.ReadLine();
                            Console.WriteLine(v);

                            int value;
                            while ((value = _serial.ReadByte()) != (byte) '\r')
                            {
                                _buffer.WriteByte(value: (byte) value);
                            }

                            if (_buffer.Length > 0)
                            {
                                DataReceived?.Invoke(sender: this, e: new DataEventArgs<byte[]>(data: _buffer.ToArray()));
                            }
                        }
                        catch (TimeoutException)
                        {
                            DataReceived?.Invoke(sender: this, e: new DataEventArgs<byte[]>(data: _buffer.ToArray()));
                        }
                        catch (Exception)
                        {
							XHelper.Flow.Ensure(() => _serial.Close());
                        }
					};

            _serial.ErrorReceived +=
                (sender, args) =>
                    {
						XHelper.Flow.Ensure(() => _serial.Close());
                    };
        }

		#region IDisposable

		public void Dispose()
		{
			Dispose(disposing: true);
		}

		private void Dispose(bool disposing)
		{
			if(_disposed)
			{
				return;
			}

			_disposed = true;

			if(disposing)
			{
				GC.SuppressFinalize(obj: this);
			}

			_serial.Close();
			_serial.Dispose();

			Disposed?.Invoke(sender: this, EventArgs.Empty);
		}

		~SerialProvider()
		{
			Dispose(disposing: false);
		}

		public event EventHandler Disposed;

		#endregion
	}
}