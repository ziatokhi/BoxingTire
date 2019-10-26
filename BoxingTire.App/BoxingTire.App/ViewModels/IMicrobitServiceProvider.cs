using BoxingTire.App.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoxingTire.App.ViewModels
{
    public interface IMicrobitServiceProvider
    {
        String ServiceName { get; }
        String ServiceDescription { get; }
        IMicrobitService GetServiceInstance();
    }
}