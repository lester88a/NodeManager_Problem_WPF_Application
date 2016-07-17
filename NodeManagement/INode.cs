using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace NodeManagement
{
    public interface INode
    {
        uint NodeId { get; }
        string City { get; }

        /// <summary>
        /// Timestamp of the node's last online time
        /// </summary>
        DateTime OnlineTime { get; }
        
        /// <summary>
        /// State of node
        /// </summary>
        bool IsOnline { get; }

        /// <summary>
        /// Current % of upstream bandwidth utilization
        /// </summary>
        float UploadUtilization { get; }

        /// <summary>
        /// Current % of downstream bandwidth utilization
        /// </summary>
        float DownloadUtilization { get; }

        /// <summary>
        /// Current % of network transfer errors 
        /// </summary>
        float ErrorRate { get; }
        
        /// <summary>
        /// Number of clients connected to this node
        /// </summary>
        uint ConnectedClients { get; }

        /// <summary>
        /// Bring the node online
        /// </summary>
        void SetOnline();

        /// <summary>
        /// Bring the node offline
        /// </summary>
        /// <returns></returns>
        void SetOffline();
        /// <summary>
        /// Bring the node online
        /// </summary>
        void SetMaxMetricsValue(uint connectedClients, float uploadUtilization, float downloadUtilization, float errorRate);
    }
}
