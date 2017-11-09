using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace WCFCommon
{
    public class ChannelFactoryWrapper<TService> : IChannelFactoryWrapper<TService> where TService : class
    {
        private readonly ChannelFactory<TService> _channelFactory;
        private readonly TService _channel;
        private bool _isDisposed;

        public ChannelFactoryWrapper(ChannelFactory<TService> channelFactory, Func<ChannelFactory<TService>, TService> channelCreator)
        {
            _channelFactory = channelFactory;
            _channel = channelCreator.Invoke(channelFactory);
        }

        public void Invoke(Action<TService> serviceCall)
        {
            serviceCall.Invoke(_channel);
        }

        public Task Invoke(Func<TService, Task> serviceCall)
        {
            return serviceCall.Invoke(_channel);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    if (_channelFactory != null)
                    {
                        if (_channelFactory.State != CommunicationState.Faulted)
                        {
                            try
                            {
                                _channelFactory.Close();
                            }
                            catch
                            {
                                _channelFactory.Abort();
                            }
                        }
                        else
                        {
                            _channelFactory.Abort();
                        }
                    }
                }
            }
            _isDisposed = true;
        }

        ~ChannelFactoryWrapper()
        {
            Dispose(false);
        }
    }

}
