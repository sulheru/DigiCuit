using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace DigitCuit_v0._1
{
    class XmlListView : ListView
    {

        // Xml tag for node, e.g. 'node' in case of <node></node>
        private const string XmlNodeTag = "node";

        // Xml attributes for node e.g. <node text="Asia" tag="" 
        // imageindex="1"></node>
        private const string XmlNodeTextAtt = "text";
        private const string XmlNodeTagAtt = "tag";
        private const string XmlNodeImageIndexAtt = "imageindex";

        public void loadFile(string fileName)
        {
            XmlTextReader reader = null;
            try
            {
                // disabling re-drawing of treeview till all nodes are added
                this.BeginUpdate();
                reader = new XmlTextReader(fileName);
                ListViewItem parentItem = null;
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.Name == XmlNodeTag)
                        {
                            ListViewItem newItem = new ListViewItem();
                            bool isEmptyElement = reader.IsEmptyElement;

                            // loading node attributes
                            int attributeCount = reader.AttributeCount;
                            if (attributeCount > 0)
                            {
                                for (int i = 0; i < attributeCount; i++)
                                {
                                    reader.MoveToAttribute(i);
                                    SetAttributeValue(newItem, reader.Name, reader.Value);
                                }
                            }
                            // add new node to Parent Node or TreeView
                            
                            if (parentItem != null)
                                parentItem.SubItems.Add(this.ConvertToListViewSubItem(newItem));
                            else
                             
                            this.Items.Add(newItem);

                            // making current node 'ParentNode' if its not empty
                            if (!isEmptyElement)
                            {
                                parentItem = newItem;
                            }
                        }
                    }
                    // moving up to in TreeView if end tag is encountered
                    else if (reader.NodeType == XmlNodeType.EndElement)
                    {
                        if (reader.Name == XmlNodeTag)
                        {
                            parentItem =null;
                        }
                    }
                    else if (reader.NodeType == XmlNodeType.XmlDeclaration)
                    {
                        //Ignore Xml Declaration                    
                    }
                    else if (reader.NodeType == XmlNodeType.None)
                    {
                        return;
                    }
                    else if (reader.NodeType == XmlNodeType.Text)
                    {
                        parentItem.SubItems.Add(reader.Value);
                    }

                }
            }
            finally
            {
                // enabling redrawing of treeview after all nodes are added
                this.EndUpdate();
                reader.Close();
            }
        }
        private ListViewItem.ListViewSubItem ConvertToListViewSubItem(ListViewItem item)
        {
            ListViewItem.ListViewSubItem sItem = new ListViewItem.ListViewSubItem();
            sItem.Text = item.Text;
            sItem.Tag = item.Tag;
            return sItem;
        }

        /// <span class="code-SummaryComment"><summary>
        /// </span>
        /// Used by Deserialize method for setting properties of 
        /// ListViewItem from xml node attributes
        /// <span class="code-SummaryComment"></summary>
        /// </span>
        private void SetAttributeValue(ListViewItem item, string propertyName, string value)
        {
            if (propertyName == XmlNodeTextAtt)
            { item.Text = value; }
            else if (propertyName == XmlNodeImageIndexAtt)
            { item.ImageIndex = int.Parse(value); }
            else if (propertyName == XmlNodeTagAtt)
            { item.Tag = value; }
        }

        public void saveFile(string fileName)
        {
            XmlTextWriter textWriter = new XmlTextWriter(fileName,
                                          System.Text.Encoding.ASCII);
            // writing the xml declaration tag
            textWriter.WriteStartDocument();
            //textWriter.WriteRaw("\r\n");
            // writing the main tag that encloses all node tags
            textWriter.WriteStartElement("TreeView");

            // save the nodes, recursive method
            SaveNodes(this.Items, textWriter);

            textWriter.WriteEndElement();

            textWriter.Close();
        }

        private void SaveNodes(ListViewItemCollection itemsCollection, XmlTextWriter textWriter)
        {
            for (int i = 0; i < itemsCollection.Count; i++)
            {
                ListViewItem item = itemsCollection[i];
                textWriter.WriteStartElement(XmlNodeTag);
                textWriter.WriteAttributeString(XmlNodeTextAtt,
                                                           item.Text);
                textWriter.WriteAttributeString(
                    XmlNodeImageIndexAtt, item.ImageIndex.ToString());
                if (item.Tag != null)
                    textWriter.WriteAttributeString(XmlNodeTagAtt,
                                                item.Tag.ToString());
                // add other node properties to serialize here  
                if (item.SubItems.Count > 0)
                {
                    SaveNodes(item.SubItems, textWriter);
                }
                textWriter.WriteEndElement();
            }
        }

        private void SaveNodes(ListViewItem.ListViewSubItemCollection itemsCollection, XmlTextWriter textWriter)
        {
            for (int i = 0; i < itemsCollection.Count; i++)
            {
                ListViewItem.ListViewSubItem item = itemsCollection[i];
                textWriter.WriteStartElement(XmlNodeTag);
                textWriter.WriteAttributeString(XmlNodeTextAtt, item.Text);                
                if (item.Tag != null)
                { textWriter.WriteAttributeString(XmlNodeTagAtt, item.Tag.ToString()); }                
                textWriter.WriteEndElement();
            }
        }
    }
}
