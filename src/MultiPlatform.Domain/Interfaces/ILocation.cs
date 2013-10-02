using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiPlatform.Domain.Interfaces
{

    public interface ILocation
    {
        Task<MultiPlatform.Domain.Models.Ui.GeoPosition> GetPosition(MultiPlatform.Domain.Interfaces.LocationAccuracy desiredAccuracy);
        Task<MultiPlatform.Domain.Models.Ui.GeoPosition> GetPositionAsync(MultiPlatform.Domain.Interfaces.LocationAccuracy desiredAccuracy, TimeSpan maximumAge, TimeSpan timeout);
        event EventHandler<MultiPlatform.Domain.Models.Ui.GeoPositionChangedEventArgs> PositionChanged;
        void Start();
        void Start(MultiPlatform.Domain.Interfaces.LocationAccuracy desiredAccuracy);
        void Start(int desiredAccuracyInMeters);
        event EventHandler<MultiPlatform.Domain.Models.Ui.LocationStatusChangedEventArgs> StatusChanged;
        void Stop();
    }

    public enum LocationStatus
    {
        /// <summary>
        /// Location data is available.
        /// </summary>
        Ready = 0,

        /// <summary>
        /// The location provider is initializing. This is the status if a GPS is the source of location data and the GPS receiver does not yet have the required number of satellites in view to obtain an accurate position.
        /// </summary>
        Initializing = 1,

        /// <summary>
        /// No location data is available from any location provider.
        /// </summary>
        NoData = 2,

        /// <summary>
        /// The location provider is disabled. This status indicates that the user has not granted the application permission to access location.
        /// </summary>
        Disabled = 3,

        /// <summary>
        /// An operation to retrieve location has not yet been initialized.
        /// </summary>
        NotInitialized = 4,

        /// <summary>
        /// The Windows Sensor and Location Platform is not available on this version of Windows.
        /// </summary>
        NotAvailable = 5,
    }
    public enum LocationAccuracy
    {
        /// <summary>
        /// Optimize for power, performance, and other cost considerations.
        /// </summary>
        Default,

        /// <summary>
        /// Deliver the most accurate report possible. This includes using services that might charge money, or consuming higher levels of battery power or connection bandwidth. An accuracy level of High may degrade system performance and should be used only when necessary.
        /// </summary>
        High
    }

}
