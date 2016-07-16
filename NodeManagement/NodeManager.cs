using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeManagement
{
    public class NodeManager : INodeManager
    {
        private readonly List<INode> _nodes;

        public NodeManager()
        {
            _nodes = new List<INode>();
        }

        public void AddNode(INode node)
        {
            //add a error handling to prevent add the same city twice
            if (!_nodes.Exists(x => x.NodeId == node.NodeId) && !_nodes.Exists(x => x.City == node.City))
            {
                // add a error handling to prevent empty input
                if (!String.IsNullOrEmpty(node.City.Replace(" ", "")))
                {
                    _nodes.Add(node);
                }
                else
                {
                    throw new Exception(string.Format("City cannot empty"));
                }
            }

            else
            {
                // add city on error handling
                throw new Exception(string.Format("Cannot add node. NodeId: {0} - City: {1} already exists.", node.NodeId ,node.City));
            }
        }

        public void RemoveNode(uint nodeId)
        {
            var existingNode = GetNode(nodeId);

            if (existingNode != null)
            {
                _nodes.Remove(existingNode);
            }
            else
            {
                throw new Exception(string.Format("Cannot remove node. NodeId {0} does not exist", nodeId));
            }
        }

        public INode GetNode(uint nodeId)
        {
            return _nodes.Find(x => x.NodeId == nodeId);
        }

        public ICollection<INode> GetNodes()
        {
            return _nodes;
        }
    }
}
