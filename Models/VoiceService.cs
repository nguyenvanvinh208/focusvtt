using System;
using System.Net;
using System.Net.Sockets;
using NAudio.Wave;
using System.Windows.Forms;

namespace Do_an.Services
{
    public class VoiceService
    {
        private WaveInEvent _waveIn;
        private WaveOutEvent _waveOut;
        private BufferedWaveProvider _waveProvider;
        private UdpClient _udpClient;
        private IPEndPoint _remoteEndPoint;
        private bool _isListening = false;

        // Cổng kết nối LAN
        private const int PORT = 11000;

        public VoiceService()
        {
            try
            {
                // Cấu hình Loa
                _waveOut = new WaveOutEvent();
                _waveProvider = new BufferedWaveProvider(new WaveFormat(16000, 1));
                _waveProvider.DiscardOnBufferOverflow = true;
                _waveOut.Init(_waveProvider);
                _waveOut.Play();

                // Cấu hình Mạng
                _udpClient = new UdpClient();
                _udpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                _udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, PORT));
            }
            catch (Exception ex)
            {
                // Chỉ hiện lỗi nếu không thể khởi tạo phần cứng (loa hỏng)
                MessageBox.Show("Lỗi Audio: " + ex.Message);
            }
        }

        public void Start(string partnerIp)
        {
            if (_isListening) return;
            _isListening = true;

            try
            {
                _remoteEndPoint = new IPEndPoint(IPAddress.Parse(partnerIp), PORT);
                _udpClient.BeginReceive(new AsyncCallback(ReceiveCallback), null);

                _waveIn = new WaveInEvent();
                _waveIn.WaveFormat = new WaveFormat(16000, 1);
                _waveIn.BufferMilliseconds = 50;
                _waveIn.DataAvailable += OnVoiceCaptured;
                _waveIn.StartRecording();
            }
            catch
            {
                // Lỗi kết nối âm thầm dừng lại, không hiện thông báo làm phiền người dùng
                Stop();
            }
        }

        private void OnVoiceCaptured(object sender, WaveInEventArgs e)
        {
            if (!_isListening || _udpClient == null || _remoteEndPoint == null) return;
            try
            {
                _udpClient.Send(e.Buffer, e.BytesRecorded, _remoteEndPoint);
            }
            catch { }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                if (_udpClient == null || !_isListening) return;

                IPEndPoint remoteIp = new IPEndPoint(IPAddress.Any, 0);
                byte[] receivedBytes = _udpClient.EndReceive(ar, ref remoteIp);

                if (receivedBytes.Length > 0 && _waveProvider != null)
                {
                    _waveProvider.AddSamples(receivedBytes, 0, receivedBytes.Length);
                }

                if (_isListening && _udpClient != null)
                    _udpClient.BeginReceive(new AsyncCallback(ReceiveCallback), null);
            }
            catch { }
        }

        public void Stop()
        {
            _isListening = false;
            try
            {
                if (_waveIn != null) { _waveIn.StopRecording(); _waveIn.Dispose(); _waveIn = null; }
                if (_udpClient != null) { _udpClient.Close(); _udpClient = null; }
                if (_waveProvider != null) _waveProvider.ClearBuffer();
            }
            catch { }
        }
    }
}
