using System.Collections.Concurrent;

namespace ForthLab.MessageLogger;

public class MessageLogger
    {
        private readonly LoggerConfiguration _config;
        
        private readonly ConcurrentQueue<string> _messages;
        private readonly SemaphoreSlim _flushSemaphore;
        
        private CancellationTokenSource _cts;
        private Task _timerTask;

        public MessageLogger(LoggerConfiguration config)
        {
            _config = config;
            
            _messages = new ConcurrentQueue<string>();
            _flushSemaphore = new SemaphoreSlim(1, 1);
            _cts = new CancellationTokenSource();
            
            _timerTask = StartScheduledFlush(_cts.Token);
        }

        public void Add(string message)
        {
            _messages.Enqueue($"{DateTime.UtcNow}: {message}");
            
            CheckFlushCondition();
        }

        private void CheckFlushCondition()
        {
            if (_messages.Count >= _config.ResetMessageCount)
            {
                _cts.Cancel();
            }
        }

        private void ResetTimer()
        {
            _cts.Dispose();
            _cts = new CancellationTokenSource();
            _timerTask = StartScheduledFlush(_cts.Token);
        }

        private async Task StartScheduledFlush(CancellationToken cancellationToken)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    await Task.Delay(_config.ResetTimeout, cancellationToken);
                    await FlushAsync();
                }
            }
            catch (TaskCanceledException)
            {
                await FlushAsync();
                ResetTimer();
            }
        }

        private async Task FlushAsync()
        {
            await _flushSemaphore.WaitAsync();
            try
            {
                var messagesToWrite = new List<string>();
                while (_messages.TryDequeue(out var msg))
                {
                    messagesToWrite.Add(msg);
                }

                if (messagesToWrite.Count > 0)
                {
                    await File.AppendAllLinesAsync(_config.FilePath, messagesToWrite);
                }
            }
            finally
            {
                _flushSemaphore.Release();
            }
        }

        public async Task StopAsync()
        {
            await _cts.CancelAsync();
            await _timerTask;
            await FlushAsync(); 
            _cts.Dispose();
            _flushSemaphore.Dispose();
        }
    }