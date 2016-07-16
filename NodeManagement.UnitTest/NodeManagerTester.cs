using System;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NodeManagement.UnitTest
{
    [TestClass]
    public class NodeManagerTester
    {

[TestMethod]
public void AddNode_Success()
{
    var nodeManager = new NodeManager();

    nodeManager.AddNode(new Node(1, "Ottawa"));

    var nodes = nodeManager.GetNodes();
}


        [TestMethod]
        public void InitializeNodeManager()
        {
            var rnd = new Random();
            var nodeManager = new NodeManager();

            nodeManager.AddNode(new Node(1, "Vancouver", rnd));
            nodeManager.AddNode(new Node(2, "Calgary", rnd));
            nodeManager.AddNode(new Node(3, "Toronto", rnd));
            nodeManager.AddNode(new Node(4, "Ottawa", rnd));
            nodeManager.AddNode(new Node(5, "Montreal", rnd));

            var nodes = nodeManager.GetNodes();

            Assert.IsTrue(nodes.Count == 5);

            // All good
        }

        [TestMethod]
        public void RemoveNodes()
        {
            const int NodeId = 1;
            const string Location = "Vancouver";

            var rnd = new Random();
            var nodeManager = new NodeManager();
            
            nodeManager.AddNode(new Node(NodeId, Location, rnd));

            var nodes = nodeManager.GetNodes();

            Assert.IsTrue(nodes.Count == 1);

            nodeManager.RemoveNode(NodeId);

            Assert.IsTrue(nodes.Count == 0);
        }

        [TestMethod]
        public void ToggleOnlineOffline()
        {
            var rnd = new Random();
            var nodeManager = new NodeManager();

            const int NodeId = 1;
            const string Location = "Vancouver";

            nodeManager.AddNode(new Node(NodeId, Location, rnd));

            var node = nodeManager.GetNode(NodeId);

            Assert.IsNotNull(node);

            node.SetOnline();
            Assert.IsTrue(node.IsOnline);

            node.SetOffline();
            Assert.IsFalse(node.IsOnline);
        }
    }
}
