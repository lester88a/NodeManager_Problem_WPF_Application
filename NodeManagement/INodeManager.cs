using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace NodeManagement
{
    public interface INodeManager
    {
        /// <summary>
        /// Add new node to the manager
        /// </summary>
        /// <param name="node"></param>
        void AddNode(INode node);

        /// <summary>
        /// Remove node from the manager
        /// </summary>
        /// <param name="nodeId">ID of the node to remove</param>
        void RemoveNode(uint nodeId);

        /// <summary>
        /// Retrieve a managed node
        /// </summary>
        /// <param name="nodeId">ID of the node to retrieve</param>
        /// <returns></returns>
        INode GetNode(uint nodeId);

        /// <summary>
        /// Retrieve all nodes added to the manager
        /// </summary>
        /// <returns></returns>
        ICollection<INode> GetNodes();
    }
}
