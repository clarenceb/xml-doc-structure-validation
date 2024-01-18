using System;
using System.Xml;

namespace XmlDocStructureValidation
{
    public class XmlValidator
    {
        private const int MaxTextLength = 16384;
        private const int MaxAttributeValueLength = 2048;
        private const int MaxAttributeNameLength = 128;
        private const int MaxDepth = 32;

        public void ValidateXml(string xml)
        {
            using (XmlReader reader = XmlReader.Create(new StringReader(xml)))
            {
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (reader.Depth > MaxDepth)
                            {
                                throw new Exception($"Element nesting depth exceeds {MaxDepth} levels");
                            }

                            if (reader.HasAttributes)
                            {
                                for (int i = 0; i < reader.AttributeCount; i++)
                                {
                                    reader.MoveToAttribute(i);

                                    if (reader.Name.Length > MaxAttributeNameLength)
                                    {
                                        throw new Exception($"Attribute name length exceeds {MaxAttributeNameLength} chars");
                                    }

                                    if (reader.Value.Length > MaxAttributeValueLength)
                                    {
                                        throw new Exception($"Attribute value length exceeds {MaxAttributeValueLength} chars");
                                    }
                                }

                                // Move the reader back to the element node.
                                reader.MoveToElement();
                            }
                            break;

                        case XmlNodeType.Text:
                        case XmlNodeType.CDATA:
                            if (reader.Value.Length > MaxTextLength)
                            {
                                throw new Exception($"Text length exceeds {MaxTextLength} chars");
                            }
                            break;
                    }
                }
            }
        }
    }
}
