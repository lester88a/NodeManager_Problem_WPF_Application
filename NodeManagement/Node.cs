using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NodeManagement
{
    public class Node : INode
    {
        private readonly Random _rnd;

        #region Properties
        // Basic properties
        public uint NodeId { get; private set; } //change from int to uint because the id is better store as unit
        public string City { get; private set; }
        
        // State
        public DateTime OnlineTime { get; private set; }
        public bool IsOnline { get; private set; }
        
        // Metrics
        public float UploadUtilization { get; private set; }
        public float DownloadUtilization  { get; private set; }
        public float ErrorRate { get; private set; }
        public uint ConnectedClients { get; private set; }
        #endregion

        #region Initialization
        public Node(uint nodeId, string city)
        {

        }
        public Node(uint nodeId, string city, Random rnd)
        {
            _rnd = rnd;

            NodeId = nodeId;
            City = city;

            OnlineTime = DateTime.Now;

            IsOnline = false;

            ResetMetrics();
        }

        #endregion

        #region Public Methods
        public void SetOnline()
        {
            IsOnline = true;
            OnlineTime = DateTime.Now;//update time
            SimulateRandomMetrics();
        }

        public void SetOffline()
        {
            IsOnline = false;
            OnlineTime = DateTime.Now; //update time
            ResetMetrics();
        }

        //set maximum values of Metrics
        public void SetMaxMetricsValue(uint connectedClients, float uploadUtilization, float downloadUtilization, float errorRate)
        {
            ConnectedClients = connectedClients;
            UploadUtilization = uploadUtilization;
            DownloadUtilization = downloadUtilization;
            ErrorRate = errorRate;
        }
        #endregion

        #region Private Methods
        private void ResetMetrics()
        {
            // Clear metrics back to 0.

            ConnectedClients = 0;

            UploadUtilization = 0.0f;
            DownloadUtilization = 0.0f;
            ErrorRate = 0.0f;
        }

        private void SimulateRandomMetrics()
        {
            // Generate random values to simulate metrics.
            
            ConnectedClients = (uint)_rnd.Next(1, 500);

            UploadUtilization = (float)_rnd.NextDouble();
            DownloadUtilization = (float)_rnd.NextDouble();
            ErrorRate = (float)_rnd.NextDouble();
        }
        #endregion
    }
}
