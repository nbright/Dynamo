﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Xml;
using Dynamo.Models;
using Dynamo.Nodes;
using Dynamo.Utilities;

namespace Migrations.CoreNodes
{
    public class Identity : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            return MigrateToDsFunction(data, "DSCoreNodes.dll", "Function.Identity", "Function.Identity@var");
        }
    }

    public class IsNull : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            var migrationData = new NodeMigrationData(data.Document);

            XmlElement oldNode = data.MigratedNodes.ElementAt(0);
            XmlElement dummyNode = MigrationManager.CreateDummyNode(oldNode, 1, 1);
            migrationData.AppendNode(dummyNode);

            return migrationData;
        }
    }

    public class ComposeFunctions : MigrationNode
    { 
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            NodeMigrationData migratedData = new NodeMigrationData(data.Document);
            XmlElement oldNode = data.MigratedNodes.ElementAt(0);
            string oldNodeId = MigrationManager.GetGuidFromXmlElement(oldNode);

            XmlElement composeNode = MigrationManager.CreateFunctionNodeFrom(oldNode);
            MigrationManager.SetFunctionSignature(composeNode, "",
                "Compose", "__Compose@_FunctionObject[]");
            migratedData.AppendNode(composeNode);
            string composeNodeId = MigrationManager.GetGuidFromXmlElement(composeNode);

            XmlElement createListNode = MigrationManager.CreateNode(data.Document,
                "DSCoreNodesUI.CreateList", "Create List");
            migratedData.AppendNode(createListNode);
            createListNode.SetAttribute("inputcount", "2");
            string createListNodeId = MigrationManager.GetGuidFromXmlElement(createListNode);

            //create and reconnect the connecters
            PortId oldInPort0 = new PortId(oldNodeId, 0, PortType.INPUT);
            XmlElement connector0 = data.FindFirstConnector(oldInPort0);

            PortId oldInPort1 = new PortId(oldNodeId, 1, PortType.INPUT);
            XmlElement connector1 = data.FindFirstConnector(oldInPort1);

            PortId newInPort0 = new PortId(composeNodeId, 0, PortType.INPUT);
            PortId newInPort1 = new PortId(createListNodeId, 0, PortType.INPUT);
            PortId newInPort2 = new PortId(createListNodeId, 1, PortType.INPUT);

            data.ReconnectToPort(connector0, newInPort1);
            data.ReconnectToPort(connector1, newInPort2);
            data.CreateConnector(createListNode, 0, composeNode, 0);

            return migratedData;
        }
    }

    public class Reverse : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            return MigrateToDsFunction(data, "DSCoreNodes.dll", "List.Reverse",
                "List.Reverse@var[]..[]");
        }
    }

    public class NewList : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            NodeMigrationData migrationData = new NodeMigrationData(data.Document);
            XmlElement oldNode = data.MigratedNodes.ElementAt(0);

            XmlElement element = MigrationManager.CloneAndChangeName(oldNode,
                "DSCoreNodesUI.CreateList", "Create List");
            migrationData.AppendNode(element);

            int childNumber = oldNode.ChildNodes.Count;
            string childNumberString = childNumber.ToString();
            element.SetAttribute("inputcount", childNumberString);

            return migrationData;
        }
    }

    public class SortWith : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            NodeMigrationData migratedData = new NodeMigrationData(data.Document);
            XmlElement oldNode = data.MigratedNodes.ElementAt(0);
            string oldNodeId = MigrationManager.GetGuidFromXmlElement(oldNode);

            //create the node itself
            XmlElement dsListNode = MigrationManager.CreateFunctionNodeFrom(oldNode);
            MigrationManager.SetFunctionSignature(dsListNode, "",
                "SortByComparsion", "SortByComparsion@var[]..[],_FunctionObject");

            migratedData.AppendNode(dsListNode);
            string dsListNodeId = MigrationManager.GetGuidFromXmlElement(dsListNode);

            //create and reconnect the connecters
            PortId oldInPort0 = new PortId(oldNodeId, 0, PortType.INPUT);
            XmlElement connector0 = data.FindFirstConnector(oldInPort0);

            PortId oldInPort1 = new PortId(oldNodeId, 1, PortType.INPUT);
            XmlElement connector1 = data.FindFirstConnector(oldInPort1);

            PortId newInPort0 = new PortId(dsListNodeId, 0, PortType.INPUT);
            PortId newInPort1 = new PortId(dsListNodeId, 1, PortType.INPUT);

            data.ReconnectToPort(connector0, newInPort1);
            data.ReconnectToPort(connector1, newInPort0);

            return migratedData;
        }
    }

    public class SortBy : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            NodeMigrationData migratedData = new NodeMigrationData(data.Document);
            XmlElement oldNode = data.MigratedNodes.ElementAt(0);
            string oldNodeId = MigrationManager.GetGuidFromXmlElement(oldNode);

            //create the node itself
            XmlElement dsListNode = MigrationManager.CreateFunctionNodeFrom(oldNode);
            MigrationManager.SetFunctionSignature(dsListNode, "",
                "SortByKey", "SortByKey@var[]..[],_FunctionObject");

            migratedData.AppendNode(dsListNode);
            string dsListNodeId = MigrationManager.GetGuidFromXmlElement(dsListNode);

            //create and reconnect the connecters
            PortId oldInPort0 = new PortId(oldNodeId, 0, PortType.INPUT);
            XmlElement connector0 = data.FindFirstConnector(oldInPort0);

            PortId oldInPort1 = new PortId(oldNodeId, 1, PortType.INPUT);
            XmlElement connector1 = data.FindFirstConnector(oldInPort1);

            PortId newInPort0 = new PortId(dsListNodeId, 0, PortType.INPUT);
            PortId newInPort1 = new PortId(dsListNodeId, 1, PortType.INPUT);

            data.ReconnectToPort(connector0, newInPort1);
            data.ReconnectToPort(connector1, newInPort0);

            return migratedData;
        }
    }

    public class Sort : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            return MigrateToDsFunction(data, "DSCoreNodes.dll", "List.Sort", "List.Sort@var[]..[]");
        }
    }

    public class ListMin : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {            
            NodeMigrationData migratedData = new NodeMigrationData(data.Document);
            XmlElement oldNode = data.MigratedNodes.ElementAt(0);
            string oldNodeId = MigrationManager.GetGuidFromXmlElement(oldNode);

            //create the node itself
            XmlElement dsListNode = MigrationManager.CreateFunctionNodeFrom(oldNode);
            MigrationManager.SetFunctionSignature(dsListNode, "",
                "MinimumItemByKey", "MinimumItemByKey@var[]..[],_FunctionObject");

            migratedData.AppendNode(dsListNode);
            string dsListNodeId = MigrationManager.GetGuidFromXmlElement(dsListNode);

            //create and reconnect the connecters
            PortId oldInPort0 = new PortId(oldNodeId, 0, PortType.INPUT);
            XmlElement connector0 = data.FindFirstConnector(oldInPort0);

            PortId oldInPort1 = new PortId(oldNodeId, 1, PortType.INPUT);
            XmlElement connector1 = data.FindFirstConnector(oldInPort1);

            PortId newInPort0 = new PortId(dsListNodeId, 0, PortType.INPUT);
            PortId newInPort1 = new PortId(dsListNodeId, 1, PortType.INPUT);

            data.ReconnectToPort(connector0, newInPort1);
            data.ReconnectToPort(connector1, newInPort0);

            return migratedData;
        }
    }

    public class ListMax : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {            
            NodeMigrationData migratedData = new NodeMigrationData(data.Document);
            XmlElement oldNode = data.MigratedNodes.ElementAt(0);
            string oldNodeId = MigrationManager.GetGuidFromXmlElement(oldNode);

            //create the node itself
            XmlElement dsListNode = MigrationManager.CreateFunctionNodeFrom(oldNode);
            MigrationManager.SetFunctionSignature(dsListNode, "",
                "MaximumItemByKey", "MaximumItemByKey@var[]..[],_FunctionObject");

            migratedData.AppendNode(dsListNode);
            string dsListNodeId = MigrationManager.GetGuidFromXmlElement(dsListNode);

            //create and reconnect the connecters
            PortId oldInPort0 = new PortId(oldNodeId, 0, PortType.INPUT);
            XmlElement connector0 = data.FindFirstConnector(oldInPort0);

            PortId oldInPort1 = new PortId(oldNodeId, 1, PortType.INPUT);
            XmlElement connector1 = data.FindFirstConnector(oldInPort1);

            PortId newInPort0 = new PortId(dsListNodeId, 0, PortType.INPUT);
            PortId newInPort1 = new PortId(dsListNodeId, 1, PortType.INPUT);

            data.ReconnectToPort(connector0, newInPort1);
            data.ReconnectToPort(connector1, newInPort0);

            return migratedData;
        }
    }

  
    public class Fold : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            NodeMigrationData migrationData = new NodeMigrationData(data.Document);
            XmlElement oldNode = data.MigratedNodes.ElementAt(0);
            string oldNodeId = MigrationManager.GetGuidFromXmlElement(oldNode);

            XmlElement newNode = MigrationManager.CloneAndChangeType(oldNode, "DSCore.Reduce");
            newNode.SetAttribute("inputcount", "3");
            string newNodeId = MigrationManager.GetGuidFromXmlElement(newNode);

            migrationData.AppendNode(newNode);
            return migrationData;
        }
    }

    public class FilterMask : MigrationNode
    {
    }

    public class FilterInAndOut : MigrationNode
    {
    }

    public class Filter : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            NodeMigrationData migrationData = new NodeMigrationData(data.Document);
            XmlElement oldNode = data.MigratedNodes.ElementAt(0);
            string oldNodeId = MigrationManager.GetGuidFromXmlElement(oldNode);

            XmlElement newNode = MigrationManager.CloneAndChangeType(oldNode, "DSCore.Filter");
            newNode.SetAttribute("nickname", "Filter");
            string newNodeId = MigrationManager.GetGuidFromXmlElement(newNode);

            PortId oldInPort0 = new PortId(oldNodeId, 0, PortType.INPUT);
            XmlElement connector0 = data.FindFirstConnector(oldInPort0);

            PortId oldInPort1 = new PortId(oldNodeId, 1, PortType.INPUT);
            XmlElement connector1 = data.FindFirstConnector(oldInPort1);

            PortId newInPort0 = new PortId(newNodeId, 0, PortType.INPUT);
            PortId newInPort1 = new PortId(newNodeId, 1, PortType.INPUT);

            data.ReconnectToPort(connector0, newInPort1);
            data.ReconnectToPort(connector1, newInPort0);

            migrationData.AppendNode(newNode);
            return migrationData;
        }
    }

    public class FilterOut : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            NodeMigrationData migrationData = new NodeMigrationData(data.Document);
            XmlElement oldNode = data.MigratedNodes.ElementAt(0);
            string oldNodeId = MigrationManager.GetGuidFromXmlElement(oldNode);

            XmlElement newNode = MigrationManager.CloneAndChangeType(oldNode, "DSCore.Filter");
            newNode.SetAttribute("nickname", "Filter");
            string newNodeId = MigrationManager.GetGuidFromXmlElement(newNode);

            PortId oldOutputPort = new PortId(oldNodeId, 0, PortType.OUTPUT);
            PortId newOutputPort = new PortId(newNodeId, 1, PortType.OUTPUT);

            if (data.FindConnectors(oldOutputPort) != null)
                foreach (XmlElement connector in data.FindConnectors(oldOutputPort))
                    data.ReconnectToPort(connector, newOutputPort);

            PortId oldInPort0 = new PortId(oldNodeId, 0, PortType.INPUT);
            XmlElement connector0 = data.FindFirstConnector(oldInPort0);

            PortId oldInPort1 = new PortId(oldNodeId, 1, PortType.INPUT);
            XmlElement connector1 = data.FindFirstConnector(oldInPort1);

            PortId newInPort0 = new PortId(newNodeId, 0, PortType.INPUT);
            PortId newInPort1 = new PortId(newNodeId, 1, PortType.INPUT);

            data.ReconnectToPort(connector0, newInPort1);
            data.ReconnectToPort(connector1, newInPort0);

            migrationData.AppendNode(newNode);
            return migrationData;
        }
    }

    public class NumberRange : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            NodeMigrationData migrationData = new NodeMigrationData(data.Document);
            XmlElement oldNode = data.MigratedNodes.ElementAt(0);

            int start = MigrationManager.GetNextIdentifierIndex();
            int end = MigrationManager.GetNextIdentifierIndex();
            int step = MigrationManager.GetNextIdentifierIndex();
            string content = "start" + start + ".." + "end" + end + ".." + "step" + step + ";";

            XmlElement newNode = MigrationManager.CreateCodeBlockNodeFrom(oldNode);
            newNode.SetAttribute("CodeText", content);
            migrationData.AppendNode(newNode);
            return migrationData;
        }
    }

    public class NumberSeq : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            NodeMigrationData migrationData = new NodeMigrationData(data.Document);
            XmlElement oldNode = data.MigratedNodes.ElementAt(0);

            int start = MigrationManager.GetNextIdentifierIndex();
            int amount = MigrationManager.GetNextIdentifierIndex();
            int step = MigrationManager.GetNextIdentifierIndex();
            string content = string.Format("start{0}.. amount{1}*step{2}-" + 
                "step{2}+start{0}..step{2};", start, amount, step);

            XmlElement newNode = MigrationManager.CreateCodeBlockNodeFrom(oldNode);
            newNode.SetAttribute("CodeText", content);
            migrationData.AppendNode(newNode);
            return migrationData;
        }
    }

    public class Combine : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            NodeMigrationData migrationData = new NodeMigrationData(data.Document);
            XmlElement oldNode = data.MigratedNodes.ElementAt(0);
            XmlElement newNode = MigrationManager.CloneAndChangeType(oldNode, "DSCore.Combine");
            newNode.RemoveAttribute("inputs");
            int numberOfInputs = Convert.ToInt32(oldNode.GetAttribute("inputs")) + 1;
            newNode.SetAttribute("inputcount", Convert.ToString(numberOfInputs));

            migrationData.AppendNode(newNode);
            return migrationData;
        }
    }

    public class CartProd : MigrationNode
    {
    }

    public class LaceShortest : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            NodeMigrationData migrationData = new NodeMigrationData(data.Document);
            XmlElement oldNode = data.MigratedNodes.ElementAt(0);
            XmlElement newNode = MigrationManager.CloneAndChangeType(oldNode, "DSCore.LaceShortest");
            newNode.RemoveAttribute("inputs");
            int numberOfInputs = Convert.ToInt32(oldNode.GetAttribute("inputs")) + 1;
            newNode.SetAttribute("inputcount", Convert.ToString(numberOfInputs));

            migrationData.AppendNode(newNode);
            return migrationData;
        }
    }

    public class LaceLongest : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            NodeMigrationData migrationData = new NodeMigrationData(data.Document);
            XmlElement oldNode = data.MigratedNodes.ElementAt(0);
            XmlElement newNode = MigrationManager.CloneAndChangeType(oldNode, "DSCore.LaceLongest");
            newNode.RemoveAttribute("inputs");
            int numberOfInputs = Convert.ToInt32(oldNode.GetAttribute("inputs")) + 1;
            newNode.SetAttribute("inputcount", Convert.ToString(numberOfInputs));

            migrationData.AppendNode(newNode);
            return migrationData;
        }
    }

    public class Map : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            NodeMigrationData migrationData = new NodeMigrationData(data.Document);
            XmlElement oldNode = data.MigratedNodes.ElementAt(0);
            string oldNodeId = MigrationManager.GetGuidFromXmlElement(oldNode);

            XmlElement newNode = MigrationManager.CloneAndChangeType(oldNode, "DSCore.Map");
            newNode.SetAttribute("nickname", "List Map");
            string newNodeId = MigrationManager.GetGuidFromXmlElement(newNode);

            PortId oldInPort0 = new PortId(oldNodeId, 0, PortType.INPUT);
            XmlElement connector0 = data.FindFirstConnector(oldInPort0);

            PortId oldInPort1 = new PortId(oldNodeId, 1, PortType.INPUT);
            XmlElement connector1 = data.FindFirstConnector(oldInPort1);

            PortId newInPort0 = new PortId(newNodeId, 0, PortType.INPUT);
            PortId newInPort1 = new PortId(newNodeId, 1, PortType.INPUT);

            data.ReconnectToPort(connector0, newInPort1);
            data.ReconnectToPort(connector1, newInPort0);

            migrationData.AppendNode(newNode);
            return migrationData;
        }
    }

    public class ForEach : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            NodeMigrationData migratedData = new NodeMigrationData(data.Document);
            XmlElement oldNode = data.MigratedNodes.ElementAt(0);
            string oldNodeId = MigrationManager.GetGuidFromXmlElement(oldNode);

            //create the node itself
            XmlElement dsListNode = MigrationManager.CreateFunctionNodeFrom(oldNode);
            MigrationManager.SetFunctionSignature(dsListNode, "",
                "ForEach", "__ForEach@_FunctionObject,var[]..[]");

            migratedData.AppendNode(dsListNode);
            string dsListNodeId = MigrationManager.GetGuidFromXmlElement(dsListNode);

            //create and reconnect the connecters
            PortId oldInPort0 = new PortId(oldNodeId, 0, PortType.INPUT);
            XmlElement connector0 = data.FindFirstConnector(oldInPort0);

            PortId oldInPort1 = new PortId(oldNodeId, 1, PortType.INPUT);
            XmlElement connector1 = data.FindFirstConnector(oldInPort1);

            PortId newInPort0 = new PortId(dsListNodeId, 0, PortType.INPUT);
            PortId newInPort1 = new PortId(dsListNodeId, 1, PortType.INPUT);

            data.ReconnectToPort(connector0, newInPort1);
            data.ReconnectToPort(connector1, newInPort0);

            return migratedData;
        }
    }

    public class AndMap : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            NodeMigrationData migratedData = new NodeMigrationData(data.Document);
            XmlElement oldNode = data.MigratedNodes.ElementAt(0);
            string oldNodeId = MigrationManager.GetGuidFromXmlElement(oldNode);

            //create the node itself
            XmlElement dsListNode = MigrationManager.CreateFunctionNodeFrom(oldNode);
            MigrationManager.SetFunctionSignature(dsListNode, "",
                "TrueForAll", "TrueForAll@var[]..[],_FunctionObject");

            migratedData.AppendNode(dsListNode);
            string dsListNodeId = MigrationManager.GetGuidFromXmlElement(dsListNode);

            //create and reconnect the connecters
            PortId oldInPort0 = new PortId(oldNodeId, 0, PortType.INPUT);
            XmlElement connector0 = data.FindFirstConnector(oldInPort0);

            PortId oldInPort1 = new PortId(oldNodeId, 1, PortType.INPUT);
            XmlElement connector1 = data.FindFirstConnector(oldInPort1);

            PortId newInPort0 = new PortId(dsListNodeId, 0, PortType.INPUT);
            PortId newInPort1 = new PortId(dsListNodeId, 1, PortType.INPUT);

            data.ReconnectToPort(connector0, newInPort1);
            data.ReconnectToPort(connector1, newInPort0);

            return migratedData;
        }
    }

    public class OrMap : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            NodeMigrationData migratedData = new NodeMigrationData(data.Document);
            XmlElement oldNode = data.MigratedNodes.ElementAt(0);
            string oldNodeId = MigrationManager.GetGuidFromXmlElement(oldNode);

            //create the node itself
            XmlElement dsListNode = MigrationManager.CreateFunctionNodeFrom(oldNode);
            MigrationManager.SetFunctionSignature(dsListNode, "",
                "TrueForAny", "TrueForAny@var[]..[],_FunctionObject");

            migratedData.AppendNode(dsListNode);
            string dsListNodeId = MigrationManager.GetGuidFromXmlElement(dsListNode);

            //create and reconnect the connecters
            PortId oldInPort0 = new PortId(oldNodeId, 0, PortType.INPUT);
            XmlElement connector0 = data.FindFirstConnector(oldInPort0);

            PortId oldInPort1 = new PortId(oldNodeId, 1, PortType.INPUT);
            XmlElement connector1 = data.FindFirstConnector(oldInPort1);

            PortId newInPort0 = new PortId(dsListNodeId, 0, PortType.INPUT);
            PortId newInPort1 = new PortId(dsListNodeId, 1, PortType.INPUT);

            data.ReconnectToPort(connector0, newInPort1);
            data.ReconnectToPort(connector1, newInPort0);

            return migratedData;
        }
    }

    public class DeCons : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            return MigrateToDsFunction(data, "DSCoreNodes.dll", "List.Deconstruct",
                "List.Deconstruct@var[]..[]");
        }
    }

    public class List : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            return MigrateToDsFunction(data, "DSCoreNodes.dll", "List.AddItemToFront",
                "List.AddItemToFront@var,var[]..[]");
        }
    }

    public class TakeList : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            NodeMigrationData migratedData = new NodeMigrationData(data.Document);
            XmlElement oldNode = data.MigratedNodes.ElementAt(0);
            string oldNodeId = MigrationManager.GetGuidFromXmlElement(oldNode);

            //create the node itself
            XmlElement dsCoreNode = MigrationManager.CreateFunctionNodeFrom(oldNode);
            MigrationManager.SetFunctionSignature(dsCoreNode, "DSCoreNodes.dll",
                "List.TakeItems", "List.TakeItems@var[]..[],int");

            migratedData.AppendNode(dsCoreNode);
            string dsCoreNodeId = MigrationManager.GetGuidFromXmlElement(dsCoreNode);

            //create and reconnect the connecters
            PortId oldInPort0 = new PortId(oldNodeId, 0, PortType.INPUT);
            XmlElement connector0 = data.FindFirstConnector(oldInPort0);

            PortId oldInPort1 = new PortId(oldNodeId, 1, PortType.INPUT);
            XmlElement connector1 = data.FindFirstConnector(oldInPort1);

            PortId newInPort0 = new PortId(dsCoreNodeId, 0, PortType.INPUT);
            PortId newInPort1 = new PortId(dsCoreNodeId, 1, PortType.INPUT);

            data.ReconnectToPort(connector0, newInPort1);
            data.ReconnectToPort(connector1, newInPort0);

            return migratedData;
        }
    }

    public class DropList : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            NodeMigrationData migratedData = new NodeMigrationData(data.Document);
            XmlElement oldNode = data.MigratedNodes.ElementAt(0);
            string oldNodeId = MigrationManager.GetGuidFromXmlElement(oldNode);

            //create the node itself
            XmlElement dsCoreNode = MigrationManager.CreateFunctionNodeFrom(oldNode);
            MigrationManager.SetFunctionSignature(dsCoreNode, "DSCoreNodes.dll",
                "List.DropItems", "List.DropItems@var[]..[],int");

            migratedData.AppendNode(dsCoreNode);
            string dsCoreNodeId = MigrationManager.GetGuidFromXmlElement(dsCoreNode);

            //create and reconnect the connecters
            PortId oldInPort0 = new PortId(oldNodeId, 0, PortType.INPUT);
            XmlElement connector0 = data.FindFirstConnector(oldInPort0);

            PortId oldInPort1 = new PortId(oldNodeId, 1, PortType.INPUT);
            XmlElement connector1 = data.FindFirstConnector(oldInPort1);

            PortId newInPort0 = new PortId(dsCoreNodeId, 0, PortType.INPUT);
            PortId newInPort1 = new PortId(dsCoreNodeId, 1, PortType.INPUT);

            data.ReconnectToPort(connector0, newInPort1);
            data.ReconnectToPort(connector1, newInPort0);

            return migratedData;
        }
    }

    public class ShiftList : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            NodeMigrationData migratedData = new NodeMigrationData(data.Document);
            XmlElement oldNode = data.MigratedNodes.ElementAt(0);
            string oldNodeId = MigrationManager.GetGuidFromXmlElement(oldNode);

            //create the node itself
            XmlElement dsCoreNode = MigrationManager.CreateFunctionNodeFrom(oldNode);
            MigrationManager.SetFunctionSignature(dsCoreNode, "DSCoreNodes.dll",
                "List.ShiftIndices", "List.ShiftIndices@var[]..[],int");

            migratedData.AppendNode(dsCoreNode);
            string dsCoreNodeId = MigrationManager.GetGuidFromXmlElement(dsCoreNode);

            //create and reconnect the connecters
            PortId oldInPort0 = new PortId(oldNodeId, 0, PortType.INPUT);
            XmlElement connector0 = data.FindFirstConnector(oldInPort0);

            PortId oldInPort1 = new PortId(oldNodeId, 1, PortType.INPUT);
            XmlElement connector1 = data.FindFirstConnector(oldInPort1);

            PortId newInPort0 = new PortId(dsCoreNodeId, 0, PortType.INPUT);
            PortId newInPort1 = new PortId(dsCoreNodeId, 1, PortType.INPUT);

            data.ReconnectToPort(connector0, newInPort1);
            data.ReconnectToPort(connector1, newInPort0);

            return migratedData;
        }
    }

    public class GetFromList : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            NodeMigrationData migratedData = new NodeMigrationData(data.Document);
            XmlElement oldNode = data.MigratedNodes.ElementAt(0);
            string oldNodeId = MigrationManager.GetGuidFromXmlElement(oldNode);

            //create the node itself
            XmlElement dsCoreNode = MigrationManager.CreateFunctionNodeFrom(oldNode);
            MigrationManager.SetFunctionSignature(dsCoreNode, "DSCoreNodes.dll",
                "List.GetItemAtIndex", "List.GetItemAtIndex@var[]..[],int");

            migratedData.AppendNode(dsCoreNode);
            string dsCoreNodeId = MigrationManager.GetGuidFromXmlElement(dsCoreNode);

            //create and reconnect the connecters
            PortId oldInPort0 = new PortId(oldNodeId, 0, PortType.INPUT);
            XmlElement connector0 = data.FindFirstConnector(oldInPort0);

            PortId oldInPort1 = new PortId(oldNodeId, 1, PortType.INPUT);
            XmlElement connector1 = data.FindFirstConnector(oldInPort1);

            PortId newInPort0 = new PortId(dsCoreNodeId, 0, PortType.INPUT);
            PortId newInPort1 = new PortId(dsCoreNodeId, 1, PortType.INPUT);

            data.ReconnectToPort(connector0, newInPort1);
            data.ReconnectToPort(connector1, newInPort0);

            return migratedData;
        }
    }

    public class Shuffle : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            return MigrateToDsFunction(data, "DSCoreNodes.dll", "List.Shuffle",
                "List.Shuffle@var[]..[]");
        }
    }

    public class GroupBy : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            NodeMigrationData migratedData = new NodeMigrationData(data.Document);
            XmlElement oldNode = data.MigratedNodes.ElementAt(0);
            string oldNodeId = MigrationManager.GetGuidFromXmlElement(oldNode);

            //create the node itself
            XmlElement dsListNode = MigrationManager.CreateFunctionNodeFrom(oldNode);
            MigrationManager.SetFunctionSignature(dsListNode, "",
                "GroupByKey", "GroupByKey@var[]..[],_FunctionObject");

            migratedData.AppendNode(dsListNode);
            string dsListNodeId = MigrationManager.GetGuidFromXmlElement(dsListNode);

            //create and reconnect the connecters
            PortId oldInPort0 = new PortId(oldNodeId, 0, PortType.INPUT);
            XmlElement connector0 = data.FindFirstConnector(oldInPort0);

            PortId oldInPort1 = new PortId(oldNodeId, 1, PortType.INPUT);
            XmlElement connector1 = data.FindFirstConnector(oldInPort1);

            PortId newInPort0 = new PortId(dsListNodeId, 0, PortType.INPUT);
            PortId newInPort1 = new PortId(dsListNodeId, 1, PortType.INPUT);

            data.ReconnectToPort(connector0, newInPort1);
            data.ReconnectToPort(connector1, newInPort0);

            return migratedData;
        }
    }

    public class SliceList : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            NodeMigrationData migratedData = new NodeMigrationData(data.Document);
            XmlElement oldNode = data.MigratedNodes.ElementAt(0);
            string oldNodeId = MigrationManager.GetGuidFromXmlElement(oldNode);

            //create the node itself
            XmlElement dsCoreNode = MigrationManager.CreateFunctionNodeFrom(oldNode);
            MigrationManager.SetFunctionSignature(dsCoreNode, "DSCoreNodes.dll",
                "List.Slice", "List.Slice@var[]..[],int,int,int");

            migratedData.AppendNode(dsCoreNode);
            string dsCoreNodeId = MigrationManager.GetGuidFromXmlElement(dsCoreNode);

            XmlElement codeBlockNode = MigrationManager.CreateCodeBlockNodeModelNode(
                data.Document,"1;");   

            migratedData.AppendNode(codeBlockNode);

            //create and reconnect the connecters
            PortId oldInPort0 = new PortId(oldNodeId, 0, PortType.INPUT);
            XmlElement connector0 = data.FindFirstConnector(oldInPort0);

            PortId oldInPort1 = new PortId(oldNodeId, 1, PortType.INPUT);
            XmlElement connector1 = data.FindFirstConnector(oldInPort1);

            PortId oldInPort2 = new PortId(oldNodeId, 2, PortType.INPUT);
            XmlElement connector2 = data.FindFirstConnector(oldInPort2);

            PortId newInPort0 = new PortId(dsCoreNodeId, 0, PortType.INPUT);
            PortId newInPort1 = new PortId(dsCoreNodeId, 1, PortType.INPUT);
            PortId newInPort2 = new PortId(dsCoreNodeId, 2, PortType.INPUT);

            data.ReconnectToPort(connector0, newInPort1);
            data.ReconnectToPort(connector1, newInPort2);
            data.ReconnectToPort(connector2, newInPort0);
            data.CreateConnector(codeBlockNode, 0, dsCoreNode, 3);

            return migratedData;
        }
    }

    public class RemoveFromList : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            NodeMigrationData migratedData = new NodeMigrationData(data.Document);
            XmlElement oldNode = data.MigratedNodes.ElementAt(0);
            string oldNodeId = MigrationManager.GetGuidFromXmlElement(oldNode);

            //create the node itself
            XmlElement dsCoreNode = MigrationManager.CreateFunctionNodeFrom(oldNode);
            MigrationManager.SetFunctionSignature(dsCoreNode, "DSCoreNodes.dll",
                "List.RemoveItemsAtIndices", "List.RemoveItemsAtIndices@var[]..[],var[]");

            migratedData.AppendNode(dsCoreNode);
            string dsCoreNodeId = MigrationManager.GetGuidFromXmlElement(dsCoreNode);

            //create and reconnect the connecters
            PortId oldInPort0 = new PortId(oldNodeId, 0, PortType.INPUT);
            XmlElement connector0 = data.FindFirstConnector(oldInPort0);

            PortId oldInPort1 = new PortId(oldNodeId, 1, PortType.INPUT);
            XmlElement connector1 = data.FindFirstConnector(oldInPort1);

            PortId newInPort0 = new PortId(dsCoreNodeId, 0, PortType.INPUT);
            PortId newInPort1 = new PortId(dsCoreNodeId, 1, PortType.INPUT);

            data.ReconnectToPort(connector0, newInPort1);
            data.ReconnectToPort(connector1, newInPort0);

            return migratedData;
        }
    }

    public class RemoveEveryNth : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            NodeMigrationData migratedData = new NodeMigrationData(data.Document);
            XmlElement oldNode = data.MigratedNodes.ElementAt(0);
            string oldNodeId = MigrationManager.GetGuidFromXmlElement(oldNode);

            //create the node itself
            XmlElement dsCoreNode = MigrationManager.CreateFunctionNodeFrom(oldNode);
            MigrationManager.SetFunctionSignature(dsCoreNode, "DSCoreNodes.dll",
                "List.DropEveryNthItem", "List.DropEveryNthItem@var[]..[],int,int");

            migratedData.AppendNode(dsCoreNode);
            string dsCoreNodeId = MigrationManager.GetGuidFromXmlElement(dsCoreNode);

            //create and reconnect the connecters
            PortId oldInPort0 = new PortId(oldNodeId, 0, PortType.INPUT);
            XmlElement connector0 = data.FindFirstConnector(oldInPort0);

            PortId oldInPort1 = new PortId(oldNodeId, 1, PortType.INPUT);
            XmlElement connector1 = data.FindFirstConnector(oldInPort1);

            PortId newInPort0 = new PortId(dsCoreNodeId, 0, PortType.INPUT);
            PortId newInPort1 = new PortId(dsCoreNodeId, 1, PortType.INPUT);

            data.ReconnectToPort(connector0, newInPort1);
            data.ReconnectToPort(connector1, newInPort0);

            return migratedData;
        }
    }

    public class TakeEveryNth : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            NodeMigrationData migratedData = new NodeMigrationData(data.Document);
            XmlElement oldNode = data.MigratedNodes.ElementAt(0);
            string oldNodeId = MigrationManager.GetGuidFromXmlElement(oldNode);

            //create the node itself
            XmlElement dsCoreNode = MigrationManager.CreateFunctionNodeFrom(oldNode);
            MigrationManager.SetFunctionSignature(dsCoreNode, "DSCoreNodes.dll",
                "List.TakeEveryNthItem", "List.TakeEveryNthItem@var[]..[],int,int");

            migratedData.AppendNode(dsCoreNode);
            string dsCoreNodeId = MigrationManager.GetGuidFromXmlElement(dsCoreNode);

            //create and reconnect the connecters
            PortId oldInPort0 = new PortId(oldNodeId, 0, PortType.INPUT);
            XmlElement connector0 = data.FindFirstConnector(oldInPort0);

            PortId oldInPort1 = new PortId(oldNodeId, 1, PortType.INPUT);
            XmlElement connector1 = data.FindFirstConnector(oldInPort1);

            PortId newInPort0 = new PortId(dsCoreNodeId, 0, PortType.INPUT);
            PortId newInPort1 = new PortId(dsCoreNodeId, 1, PortType.INPUT);

            data.ReconnectToPort(connector0, newInPort1);
            data.ReconnectToPort(connector1, newInPort0);

            return migratedData;
        }
    }

    public class Empty : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            return MigrateToDsFunction(data, "DSCoreNodes.dll", "List.Empty", "List.Empty");
        }
    }

    public class IsEmpty : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            return MigrateToDsFunction(data, "DSCoreNodes.dll", "List.IsEmpty",
                "List.IsEmpty@var[]..[]");
        }
    }

    public class Length : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            return MigrateToDsFunction(data, "DSCoreNodes.dll", "List.Count",
                "List.Count@var[]..[]");
        }
    }

    public class Append : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            NodeMigrationData migratedData = new NodeMigrationData(data.Document);
            XmlElement oldNode = data.MigratedNodes.ElementAt(0);
            string oldNodeId = MigrationManager.GetGuidFromXmlElement(oldNode);

            XmlElement listJoinNode = MigrationManager.CreateVarArgFunctionNodeFrom(oldNode);
            MigrationManager.SetFunctionSignature(listJoinNode, "DSCoreNodes.dll",
                "List.Join", "List.Join@var[]..[]");
            migratedData.AppendNode(listJoinNode);
            
            listJoinNode.SetAttribute("inputcount", "2");

            return migratedData;
        }
    }

    public class First : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            return MigrateToDsFunction(data, "DSCoreNodes.dll", "List.FirstItem",
                "List.FirstItem@var[]..[]");
        }
    }

    public class Last : MigrationNode
    {
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            return MigrateToDsFunction(data, "DSCoreNodes.dll", "List.LastItem",
                "List.LastItem@var[]..[]");
        }
    }

    public class Rest : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            return MigrateToDsFunction(data, "DSCoreNodes.dll", "List.RestOfItems",
                "List.RestOfItems@var[]..[]");
        }
    }

    public class Slice : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            return MigrateToDsFunction(data, "DSCoreNodes.dll", "List.Chop",
                "List.Chop@var[]..[],int");
        }
    }

    public class DiagonalRightList : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            return MigrateToDsFunction(data, "DSCoreNodes.dll", "List.DiagonalRight",
                "List.DiagonalRight@var[]..[],int");
        }
    }

    public class DiagonalLeftList : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            return MigrateToDsFunction(data, "DSCoreNodes.dll", "List.DiagonalLeft",
                "List.DiagonalLeft@var[]..[],int");
        }
    }

    public class Transpose : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            return MigrateToDsFunction(data, "", "Transpose", "Transpose@var[]..[]");
        }
    }

    public partial class Sublists : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            NodeMigrationData migrationData = new NodeMigrationData(data.Document);

            // Create DSFunction node
            XmlElement oldNode = data.MigratedNodes.ElementAt(0);

            var newNode = MigrationManager.CreateFunctionNodeFrom(oldNode);
            MigrationManager.SetFunctionSignature(newNode, "DSCoreNodes.dll",
                "List.Sublists", "List.Sublists@var[]..[],var[]..[],int");
            migrationData.AppendNode(newNode);
            string newNodeId = MigrationManager.GetGuidFromXmlElement(newNode);

            // Create code block node
            string rangesString = "{0}";
            foreach (XmlNode childNode in oldNode.ChildNodes)
            {
                if (childNode.Name.Equals(typeof(string).FullName))
                    rangesString = "{" + childNode.Attributes[0].Value + "};";
            }

            XmlElement codeBlockNode = MigrationManager.CreateCodeBlockNodeModelNode(
                data.Document, rangesString);
            migrationData.AppendNode(codeBlockNode);
            string codeBlockNodeId = MigrationManager.GetGuidFromXmlElement(codeBlockNode);

            // Update connectors
            for (int idx = 0; true; idx++)
            {
                PortId oldInPort = new PortId(newNodeId, idx + 2, PortType.INPUT);
                PortId newInPort = new PortId(codeBlockNodeId, idx, PortType.INPUT);
                XmlElement connector = data.FindFirstConnector(oldInPort);
                
                if (connector == null)
                    break;

                data.ReconnectToPort(connector, newInPort);
            }

            PortId oldInPort1 = new PortId(newNodeId, 1, PortType.INPUT);
            PortId newInPort2 = new PortId(newNodeId, 2, PortType.INPUT);
            XmlElement connector1 = data.FindFirstConnector(oldInPort1);
            
            data.ReconnectToPort(connector1, newInPort2);
            data.CreateConnector(codeBlockNode, 0, newNode, 1);

            return migrationData;
        }
    }

    public class Repeat : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            return MigrateToDsFunction(data, "DSCoreNodes.dll", "List.Repeat",
                "List.Repeat@var[]..[],int");
        }
    }

    public class FlattenList : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            NodeMigrationData migrationData = new NodeMigrationData(data.Document);

            XmlElement oldNode = data.MigratedNodes.ElementAt(0);
            XmlElement dummyNode = MigrationManager.CreateDummyNode(oldNode, 1, 1);
            migrationData.AppendNode(dummyNode);

            return migrationData;
        }
    }

    public class FlattenListAmt : MigrationNode
    {
    }

    public class LessThan : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            return MigrateToDsFunction(data, "<", "<@,");
        }
    }

    public class LessThanEquals : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            return MigrateToDsFunction(data, "<=", "<=@,");
        }
    }

    public class GreaterThan : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            return MigrateToDsFunction(data, ">", ">@,");
        }
    }

    public class GreaterThanEquals : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            return MigrateToDsFunction(data, ">=", ">=@,");
        }
    }

    public class Equal : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            return MigrateToDsFunction(data, "==", "==@,");
        }
    }

    public class And : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            NodeMigrationData migrationData = new NodeMigrationData(data.Document);
            XmlElement oldNode = data.MigratedNodes.ElementAt(0);
            string oldNodeId = MigrationManager.GetGuidFromXmlElement(oldNode);

            XmlElement newNode = MigrationManager.CloneAndChangeType(oldNode, "DSCore.Logic.And");
            newNode.SetAttribute("inputcount", "2");
            string newNodeId = MigrationManager.GetGuidFromXmlElement(newNode);

            migrationData.AppendNode(newNode);
            return migrationData;
        }
    }

    public class Or : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            NodeMigrationData migrationData = new NodeMigrationData(data.Document);
            XmlElement oldNode = data.MigratedNodes.ElementAt(0);
            string oldNodeId = MigrationManager.GetGuidFromXmlElement(oldNode);

            XmlElement newNode = MigrationManager.CloneAndChangeType(oldNode, "DSCore.Logic.Or");
            newNode.SetAttribute("inputcount", "2");
            string newNodeId = MigrationManager.GetGuidFromXmlElement(newNode);

            migrationData.AppendNode(newNode);
            return migrationData;
        }
    }

    public class Xor : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            return MigrateToDsFunction(data, "Logic.Xor", "Logic.Xor@bool,bool");
        }
    }

    public class Not : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            return MigrateToDsFunction(data, "Not", "Not@,");
        }
    }

    public class Addition : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            return MigrateToDsFunction(data, "+", "+@,");
        }
    }

    public class Subtraction : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            return MigrateToDsFunction(data, "-", "-@,");
        }
    }

    public class Multiplication : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            return MigrateToDsFunction(data, "*", "*@,");
        }
    }

    public class Division : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            return MigrateToDsFunction(data, "/", "/@,");
        }
    }

    public class Modulo : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            return MigrateToDsFunction(data, "%", "%@,");
        }
    }

    public class Pow : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            return MigrateToDsFunction(data, "DSCoreNodes.dll", "Math.Pow", "Math.Pow@double,double");
        }
    }

    public class Round : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            return MigrateToDsFunction(data, "DSCoreNodes.dll", "Math.Round", "Math.Round@double");
        }
    }

    public class Floor : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            return MigrateToDsFunction(data, "DSCoreNodes.dll", "Math.Floor", "Math.Floor@double");
        }
    }

    public class Ceiling : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            return MigrateToDsFunction(data, "DSCoreNodes.dll", "Math.Ceiling", "Math.Ceiling@double");
        }
    }

    public class RandomSeed : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            return MigrateToDsFunction(data, "DSCoreNodes.dll", "Math.RandomSeed", "Math.RandomSeed@int");
        }
    }

    public class Random : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            return MigrateToDsFunction(data, "DSCoreNodes.dll", "Math.Rand", "Math.Rand");
        }
    }

    public class RandomList : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            return MigrateToDsFunction(data, "DSCoreNodes.dll", "Math.RandomList", "Math.RandomList@int");
        }
    }

    public class EConstant : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            return MigrateToDsFunction(data, "DSCoreNodes.dll", "Math.E", "Math.E");
        }
    }

    public class Pi : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            return MigrateToDsFunction(data, "DSCoreNodes.dll", "Math.PI", "Math.PI");
        }
    }

    public class PiTimes2 : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            return MigrateToDsFunction(data, "DSCoreNodes.dll", "Math.PiTimes2", "Math.PiTimes2");
        }
    }

    public class Sin : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            NodeMigrationData migrationData = new NodeMigrationData(data.Document);

            // Create DSFunction node
            XmlElement oldNode = data.MigratedNodes.ElementAt(0);

            var newNode = MigrationManager.CreateFunctionNodeFrom(oldNode);
            MigrationManager.SetFunctionSignature(newNode, "DSCoreNodes.dll",
                "Math.Sin", "Math.Sin@double");
            migrationData.AppendNode(newNode);
            string newNodeId = MigrationManager.GetGuidFromXmlElement(newNode);

            // Create new node
            XmlElement converterNode = MigrationManager.CreateFunctionNode(
                data.Document, "DSCoreNodes.dll",
                "Math.RadiansToDegrees", "Math.RadiansToDegrees@double");
            migrationData.AppendNode(converterNode);
            string converterNodeId = MigrationManager.GetGuidFromXmlElement(converterNode);

            // Update connectors
            PortId oldInPort0 = new PortId(newNodeId, 0, PortType.INPUT);
            PortId newInPortCBN = new PortId(converterNodeId, 0, PortType.INPUT);
            XmlElement connector0 = data.FindFirstConnector(oldInPort0);

            data.ReconnectToPort(connector0, newInPortCBN);
            data.CreateConnector(converterNode, 0, newNode, 0);

            return migrationData;
        }
    }

    public class Cos : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            NodeMigrationData migrationData = new NodeMigrationData(data.Document);

            // Create DSFunction node
            XmlElement oldNode = data.MigratedNodes.ElementAt(0);

            var newNode = MigrationManager.CreateFunctionNodeFrom(oldNode);
            MigrationManager.SetFunctionSignature(newNode, "DSCoreNodes.dll",
                "Math.Cos", "Math.Cos@double");
            migrationData.AppendNode(newNode);
            string newNodeId = MigrationManager.GetGuidFromXmlElement(newNode);

            // Create new node
            XmlElement converterNode = MigrationManager.CreateFunctionNode(
                data.Document, "DSCoreNodes.dll",
                "Math.RadiansToDegrees", "Math.RadiansToDegrees@double");
            migrationData.AppendNode(converterNode);
            string converterNodeId = MigrationManager.GetGuidFromXmlElement(converterNode);

            // Update connectors
            PortId oldInPort0 = new PortId(newNodeId, 0, PortType.INPUT);
            PortId newInPortCBN = new PortId(converterNodeId, 0, PortType.INPUT);
            XmlElement connector0 = data.FindFirstConnector(oldInPort0);

            data.ReconnectToPort(connector0, newInPortCBN);
            data.CreateConnector(converterNode, 0, newNode, 0);

            return migrationData;
        }
    }

    public class Tan : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            NodeMigrationData migrationData = new NodeMigrationData(data.Document);

            // Create DSFunction node
            XmlElement oldNode = data.MigratedNodes.ElementAt(0);

            var newNode = MigrationManager.CreateFunctionNodeFrom(oldNode);
            MigrationManager.SetFunctionSignature(newNode, "DSCoreNodes.dll",
                "Math.Tan", "Math.Tan@double");
            migrationData.AppendNode(newNode);
            string newNodeId = MigrationManager.GetGuidFromXmlElement(newNode);

            // Create new node
            XmlElement converterNode = MigrationManager.CreateFunctionNode(
                data.Document, "DSCoreNodes.dll",
                "Math.RadiansToDegrees", "Math.RadiansToDegrees@double");
            migrationData.AppendNode(converterNode);
            string converterNodeId = MigrationManager.GetGuidFromXmlElement(converterNode);

            // Update connectors
            PortId oldInPort0 = new PortId(newNodeId, 0, PortType.INPUT);
            PortId newInPortCBN = new PortId(converterNodeId, 0, PortType.INPUT);
            XmlElement connector0 = data.FindFirstConnector(oldInPort0);

            data.ReconnectToPort(connector0, newInPortCBN);
            data.CreateConnector(converterNode, 0, newNode, 0);

            return migrationData;
        }
    }

    public class Asin : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            NodeMigrationData migrationData = new NodeMigrationData(data.Document);

            // Create DSFunction node
            XmlElement oldNode = data.MigratedNodes.ElementAt(0);

            var converterNode = MigrationManager.CreateFunctionNodeFrom(oldNode);
            MigrationManager.SetFunctionSignature(converterNode, "DSCoreNodes.dll",
                "Math.DegreesToRadians", "Math.DegreesToRadians@double");
            migrationData.AppendNode(converterNode);
            string converterNodeId = MigrationManager.GetGuidFromXmlElement(converterNode);

            // Create new node
            XmlElement asinNode = MigrationManager.CreateFunctionNode(
                data.Document, "DSCoreNodes.dll",
                "Math.Asin", "Math.Asin@double");
            migrationData.AppendNode(asinNode);
            string asinNodeId = MigrationManager.GetGuidFromXmlElement(asinNode);

            // Update connectors
            PortId oldInPort0 = new PortId(converterNodeId, 0, PortType.INPUT);
            PortId newInPortAsin = new PortId(asinNodeId, 0, PortType.INPUT);
            XmlElement connector0 = data.FindFirstConnector(oldInPort0);

            data.ReconnectToPort(connector0, newInPortAsin);
            data.CreateConnector(asinNode, 0, converterNode, 0);

            return migrationData;
        }
    }

    public class Acos : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            NodeMigrationData migrationData = new NodeMigrationData(data.Document);

            // Create DSFunction node
            XmlElement oldNode = data.MigratedNodes.ElementAt(0);

            var converterNode = MigrationManager.CreateFunctionNodeFrom(oldNode);
            MigrationManager.SetFunctionSignature(converterNode, "DSCoreNodes.dll",
                "Math.DegreesToRadians", "Math.DegreesToRadians@double");
            migrationData.AppendNode(converterNode);
            string converterNodeId = MigrationManager.GetGuidFromXmlElement(converterNode);

            // Create new node
            XmlElement acosNode = MigrationManager.CreateFunctionNode(
                data.Document, "DSCoreNodes.dll",
                "Math.Acos", "Math.Acos@double");
            migrationData.AppendNode(acosNode);
            string acosNodeId = MigrationManager.GetGuidFromXmlElement(acosNode);

            // Update connectors
            PortId oldInPort0 = new PortId(converterNodeId, 0, PortType.INPUT);
            PortId newInPortAcos = new PortId(acosNodeId, 0, PortType.INPUT);
            XmlElement connector0 = data.FindFirstConnector(oldInPort0);

            data.ReconnectToPort(connector0, newInPortAcos);
            data.CreateConnector(acosNode, 0, converterNode, 0);

            return migrationData;
        }
    }

    public class Atan : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            NodeMigrationData migrationData = new NodeMigrationData(data.Document);

            // Create DSFunction node
            XmlElement oldNode = data.MigratedNodes.ElementAt(0);

            var converterNode = MigrationManager.CreateFunctionNodeFrom(oldNode);
            MigrationManager.SetFunctionSignature(converterNode, "DSCoreNodes.dll",
                "Math.DegreesToRadians", "Math.DegreesToRadians@double");
            migrationData.AppendNode(converterNode);
            string converterNodeId = MigrationManager.GetGuidFromXmlElement(converterNode);

            // Create new node
            XmlElement atanNode = MigrationManager.CreateFunctionNode(
                data.Document, "DSCoreNodes.dll",
                "Math.Atan", "Math.Atan@double");
            migrationData.AppendNode(atanNode);
            string atanNodeId = MigrationManager.GetGuidFromXmlElement(atanNode);

            // Update connectors
            PortId oldInPort0 = new PortId(converterNodeId, 0, PortType.INPUT);
            PortId newInPortAtan = new PortId(atanNodeId, 0, PortType.INPUT);
            XmlElement connector0 = data.FindFirstConnector(oldInPort0);

            data.ReconnectToPort(connector0, newInPortAtan);
            data.CreateConnector(atanNode, 0, converterNode, 0);

            return migrationData;
        }
    }

    public class Average : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            return MigrateToDsFunction(data, "DSCoreNodes.dll", "Math.Average", "Math.Average@var[]");
        }
    }

    public class Smooth : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            NodeMigrationData migrationData = new NodeMigrationData(data.Document);

            XmlElement oldNode = data.MigratedNodes.ElementAt(0);
            XmlElement dummyNode = MigrationManager.CreateDummyNode(oldNode, 1, 1);
            migrationData.AppendNode(dummyNode);

            return migrationData;
        }
    }

    public class Begin : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            NodeMigrationData migrationData = new NodeMigrationData(data.Document);

            XmlElement oldNode = data.MigratedNodes.ElementAt(0);

            int inputCount = oldNode.ChildNodes.Count;
            XmlElement dummyNode = MigrationManager.CreateDummyNode(oldNode, inputCount, 1);

            migrationData.AppendNode(dummyNode);
            return migrationData;
        }
    }

    public class ApplyList : MigrationNode
    {
    }

    public class Apply1 : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            NodeMigrationData migratedData = new NodeMigrationData(data.Document);
            XmlElement oldNode = data.MigratedNodes.ElementAt(0);
            string oldNodeId = MigrationManager.GetGuidFromXmlElement(oldNode);

            XmlElement applyNode = MigrationManager.CreateFunctionNodeFrom(oldNode);
            MigrationManager.SetFunctionSignature(applyNode, "",
                "Apply", "Apply@_FunctionObject,var[]..[]");
            migratedData.AppendNode(applyNode);
            string applyNodeId = MigrationManager.GetGuidFromXmlElement(applyNode);

            int numberOfArgs = oldNode.ChildNodes.Count;
            string numberOfArgsString = numberOfArgs.ToString();
            XmlElement createListNode = MigrationManager.CreateNode(data.Document,
                "DSCoreNodesUI.CreateList", "Create List");
            migratedData.AppendNode(createListNode);
            createListNode.SetAttribute("inputcount", numberOfArgsString);
            string createListNodeId = MigrationManager.GetGuidFromXmlElement(createListNode);

            //create and reconnect the connecters
            while (numberOfArgs > 0) 
            {
                PortId oldInPort = new PortId(oldNodeId, numberOfArgs, PortType.INPUT);
                XmlElement connector = data.FindFirstConnector(oldInPort);
                PortId newInPort = new PortId(createListNodeId, numberOfArgs - 1, PortType.INPUT);
                data.ReconnectToPort(connector, newInPort);
                numberOfArgs--;
            }
            data.CreateConnector(createListNode, 0, applyNode, 1);

            return migratedData;
        }
    }

    public class Conditional : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            NodeMigrationData migrationData = new NodeMigrationData(data.Document);
            migrationData.AppendNode(MigrationManager.CloneAndChangeType(
                data.MigratedNodes.ElementAt(0), "DSCoreNodesUI.Logic.If"));

            return migrationData;
        }
    }
    
    public partial class Breakpoint : MigrationNode
    {
    }

    public partial class DoubleInput : MigrationNode
    {
        
    }

    public class AngleInput : MigrationNode
    {
    }

    public partial class DoubleSliderInput : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            NodeMigrationData migrationData = new NodeMigrationData(data.Document);
            XmlElement oldNode = data.MigratedNodes.ElementAt(0);
            XmlElement newNode = MigrationManager.CloneAndChangeType(oldNode, "Dynamo.Nodes.DoubleSlider");

            // Get attributes from old child node
            XmlElement newChild1 = data.Document.CreateElement("System.Double");
            XmlElement newChild2 = data.Document.CreateElement("Range");

            foreach (XmlNode subNode in oldNode.ChildNodes)
            {
                foreach (XmlNode attr in subNode.Attributes)
                {
                    if (attr.Name.Equals("value"))
                        newChild1.InnerText = attr.Value;
                    else
                        newChild2.SetAttribute(attr.Name, attr.Value);
                }
            }

            newNode.AppendChild(newChild1);
            newNode.AppendChild(newChild2);

            migrationData.AppendNode(newNode);
            return migrationData;
        }
    }

    public partial class IntegerSliderInput : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            NodeMigrationData migrationData = new NodeMigrationData(data.Document);
            XmlElement oldNode = data.MigratedNodes.ElementAt(0);
            XmlElement newNode = MigrationManager.CloneAndChangeType(oldNode, "Dynamo.Nodes.IntegerSlider");

            // Get attributes from old child node
            XmlElement newChild1 = data.Document.CreateElement("System.Int32");
            XmlElement newChild2 = data.Document.CreateElement("Range");

            foreach (XmlNode subNode in oldNode.ChildNodes)
            {
                foreach (XmlNode attr in subNode.Attributes)
                {
                    if (attr.Name.Equals("value"))
                        newChild1.InnerText = attr.Value;
                    else
                        newChild2.SetAttribute(attr.Name, attr.Value);
                }
            }

            newNode.AppendChild(newChild1);
            newNode.AppendChild(newChild2);

            migrationData.AppendNode(newNode);
            return migrationData;
        }
    }

    public partial class BoolSelector : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            NodeMigrationData migrationData = new NodeMigrationData(data.Document);
            XmlElement oldNode = data.MigratedNodes.ElementAt(0);
            XmlElement newNode = MigrationManager.CloneAndChangeType(oldNode, "DSCoreNodesUI.BoolSelector");

            // Get attribute from old child node
            XmlElement newChild = data.Document.CreateElement("System.Boolean");

            foreach (XmlNode subNode in oldNode.ChildNodes)
            {
                foreach (XmlNode attr in subNode.Attributes)
                {
                    if (attr.Name.Equals("value"))
                        newChild.InnerText = attr.Value;
                }
            }

            newNode.AppendChild(newChild);

            migrationData.AppendNode(newNode);
            return migrationData;
        }
    }

    public partial class StringInput : MigrationNode
    {
        [NodeMigration(@from: "0.5.3.0", to: "0.6.3.0")]
        public static NodeMigrationData Migrate_0530_to_0600(NodeMigrationData data)
        {
            NodeMigrationData migrationData = new NodeMigrationData(data.Document);

            XmlNode nodeElement = data.MigratedNodes.ElementAt(0);
            XmlNode newNode = nodeElement.CloneNode(true);

            var query = from XmlNode subNode in newNode.ChildNodes
                        where subNode.Name.Equals(typeof(string).FullName)
                        from XmlAttribute attr in subNode.Attributes
                        where attr.Name.Equals("value")
                        select attr;

            foreach (XmlAttribute attr in query)
                attr.Value = HttpUtility.HtmlEncode(HttpUtility.UrlDecode(attr.Value));

            migrationData.AppendNode(newNode as XmlElement);
            return migrationData;
        }

        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            NodeMigrationData migrationData = new NodeMigrationData(data.Document);
            XmlElement original = data.MigratedNodes.ElementAt(0);

            // Escape special characters for display in code block node.
            string content = ExtensionMethods.GetChildNodeStringValue(original);
            content = content.Replace("\r\n", "\\n");
            content = content.Replace("\t", "\\t");
            content = content.Replace("\"", "\\\"");
            content = string.Format("\"{0}\";", content);

            XmlElement newNode = MigrationManager.CreateCodeBlockNodeFrom(original);
            newNode.SetAttribute("CodeText", content);
            migrationData.AppendNode(newNode);
            return migrationData;
        }

    }

    public partial class StringDirectory : StringFilename
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            NodeMigrationData migrationData = new NodeMigrationData(data.Document);
            XmlElement original = data.MigratedNodes.ElementAt(0);
            var cloned = MigrationManager.CloneAndChangeType(original, "DSCore.File.Directory");

            var document = original.OwnerDocument;
            foreach (XmlNode childNode in original.ChildNodes)
            {
                if (childNode.Name.Equals(typeof(string).FullName))
                {
                    var childElement = document.CreateElement(typeof(string).FullName);
                    childElement.InnerText = childNode.Attributes[0].Value;
                    cloned.AppendChild(childElement);
                }
            }

            migrationData.AppendNode(cloned);
            return migrationData;            
        }
    }

    public partial class StringFilename : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            NodeMigrationData migrationData = new NodeMigrationData(data.Document);
            XmlElement original = data.MigratedNodes.ElementAt(0);
            var cloned = MigrationManager.CloneAndChangeType(original, "DSCore.File.Filename");

            var document = original.OwnerDocument;
            foreach (XmlNode childNode in original.ChildNodes)
            {
                if (childNode.Name.Equals(typeof(string).FullName))
                {
                    var childElement = document.CreateElement(typeof(string).FullName);
                    childElement.InnerText = childNode.Attributes[0].Value;
                    cloned.AppendChild(childElement);
                }
            }

            migrationData.AppendNode(cloned);
            return migrationData;
        }
    }

    public class ConcatStrings : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            NodeMigrationData migratedData = new NodeMigrationData(data.Document);
            XmlElement oldNode = data.MigratedNodes.ElementAt(0);
            string oldNodeId = MigrationManager.GetGuidFromXmlElement(oldNode);

            XmlElement stringNode = MigrationManager.CreateVarArgFunctionNodeFrom(oldNode);
            MigrationManager.SetFunctionSignature(stringNode, "DSCoreNodes.dll",
                "String.Concat", "String.Concat@string[]");
            migratedData.AppendNode(stringNode);

            int numberOfArgs = oldNode.ChildNodes.Count + 2;
            string numberOfArgsString = numberOfArgs.ToString();

            stringNode.SetAttribute("inputcount", numberOfArgsString);

            return migratedData;
        }
    }

    public class String2Num : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            NodeMigrationData migrationData = new NodeMigrationData(data.Document);

            XmlElement oldNode = data.MigratedNodes.ElementAt(0);
            XmlElement dummyNode = MigrationManager.CreateDummyNode(oldNode, 1, 1);
            migrationData.AppendNode(dummyNode);

            return migrationData;
        }
    }

    public class Num2String : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            return MigrateToDsFunction(data, "DSCoreNodes.dll", "String.FromObject", "String.FromObject@var");
        }
    }

    public class StringLen : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            return MigrateToDsFunction(data, "DSCoreNodes.dll", "String.Length", "String.Length@string");
        }
    }

    public class ToString : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            return MigrateToDsFunction(data, "DSCoreNodes.dll", "String.FromObject", "String.FromObject@var");
        }
    }

    public class SplitString : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            return MigrateToDsFunction(data, "DSCoreNodes.dll", "String.Split", "String.Split@string,string[]");
        }
    }

    public class JoinStrings : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            NodeMigrationData migratedData = new NodeMigrationData(data.Document);
            XmlElement oldNode = data.MigratedNodes.ElementAt(0);
            string oldNodeId = MigrationManager.GetGuidFromXmlElement(oldNode);

            //create the node itself
            XmlElement dsCoreNode = MigrationManager.CreateFunctionNodeFrom(oldNode);
            MigrationManager.SetFunctionSignature(dsCoreNode, "DSCoreNodes.dll",
                "String.Join", "String.Join@string,string[]");

            migratedData.AppendNode(dsCoreNode);
            string dsCoreNodeId = MigrationManager.GetGuidFromXmlElement(dsCoreNode);

            //create and reconnect the connecters
            PortId oldInPort0 = new PortId(oldNodeId, 0, PortType.INPUT);
            XmlElement connector0 = data.FindFirstConnector(oldInPort0);

            PortId oldInPort1 = new PortId(oldNodeId, 1, PortType.INPUT);
            XmlElement connector1 = data.FindFirstConnector(oldInPort1);

            PortId newInPort0 = new PortId(dsCoreNodeId, 0, PortType.INPUT);
            PortId newInPort1 = new PortId(dsCoreNodeId, 1, PortType.INPUT);

            data.ReconnectToPort(connector0, newInPort1);
            data.ReconnectToPort(connector1, newInPort0);

            return migratedData;           
        }
    }

    public class StringCase : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            return MigrateToDsFunction(data, "DSCoreNodes.dll", "String.StringCase", "String.StringCase@string,bool");
        }
    }

    public class Substring : MigrationNode
    {
        [NodeMigration(@from: "0.6.3.0", to: "0.7.0.0")]
        public static NodeMigrationData Migrate_0630_to_0700(NodeMigrationData data)
        {
            return MigrateToDsFunction(data, "DSCoreNodes.dll", "String.Substring", "String.Substring@string,int,int");
        }
    }
}