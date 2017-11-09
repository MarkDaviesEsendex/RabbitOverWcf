using System;

namespace WCFCommon
{
    public interface IChannelFactoryWrapper<TService> : IDisposable
    {
        void Invoke(Action<TService> serviceCall);
    }
}
