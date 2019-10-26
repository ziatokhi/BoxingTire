﻿
using BoxingTire.App.Services.Helpers;
using BoxingTire.App.ViewModels;
using Plugin.BLE.Abstractions.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoxingTire.App.Services
{
    public static class IdToServiceProviderMappingProvider
    {
        private static Dictionary<Guid, Func<IService, IMicrobitServiceProvider>> _mapping =
            new Dictionary<Guid, Func<IService, IMicrobitServiceProvider>>()
        {
			//// Device Information Service
			//{ServiceIds.DeviceInformationServiceId, (service) => new MicrobitServiceProvider(
   //             "Device Information Service",
   //             "Device name, manufacturer, etc.",
   //             ServiceIds.DeviceInformationServiceId,
   //             service,
   //             DeviceInformationService.GetInstance)},

			//// Temperature Service
			//{ServiceIds.TemperatureServiceId, (service) => new MicrobitServiceProvider(
   //             "Temperature Service",
   //             "Temperature sensor data",
   //             ServiceIds.TemperatureServiceId,
   //             service,
   //             TemperatureService.GetInstance)},

			//// Accelerometer Service
			//{ServiceIds.AccelerometerServiceId, (service) => new MicrobitServiceProvider(
   //             "Accelerometer Service",
   //             "X, Y, Z axis acceleration values",
   //             ServiceIds.AccelerometerServiceId,
   //             service,
   //             AccelerometerService.GetInstance)},

			//// Button Service
			//{ServiceIds.ButtonServiceId, (service) => new MicrobitServiceProvider(
   //             "Button Service",
   //             "A / B button states",
   //             ServiceIds.ButtonServiceId,
   //             service,
   //             ButtonService.GetInstance)},

			//// Magnetometer Service
			//{ServiceIds.MagnetometerServiceId, (service) => new MicrobitServiceProvider(
   //             "Magnetometer Service",
   //             "Compass Data",
   //             ServiceIds.MagnetometerServiceId,
   //             service,
   //             MagnetometerService.GetInstance)},
			
			//// Led Service
			//{ServiceIds.LedServiceId, (service) => new MicrobitServiceProvider(
   //             "LED Service",
   //             "Show text on LED Display",
   //             ServiceIds.LedServiceId,
   //             service,
   //             SEService.GetInstance)},
        };

        public static IMicrobitServiceProvider ServiceProvider(IService serviceInstance)
        {
            Func<IService, IMicrobitServiceProvider> provider;
            if (!_mapping.TryGetValue(serviceInstance.Id, out provider))
            {
                return null;
            }
            return provider(serviceInstance);
        }
    }
}