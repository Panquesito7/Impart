using System;
using System.Text;
using System.Collections.Generic;

namespace Impart
{
    /// <summary>Text element.</summary>
    public struct Text : Element, Nested
    {
        private string _Text;

        /// <value>The text value of the Text.</value>
        public string TextValue
        {
            get 
            {
                return _Text;
            }
        }
        private string _ID;

        /// <value>The ID value of the Text.</value>
        public string ID 
        {
            get 
            {
                return _ID;
            }
        }
        private TextType _Type;

        /// <value>The type value of the Text.</value>
        public TextType Type
        {
            get
            {
                return _Type;
            }
        }
        private List<Attribute> _Attributes = new List<Attribute>();

        /// <value>The attribute values of the Text.</value>
        public List<Attribute> Attributes
        {
            get 
            {
                return _Attributes;
            }
        }
        private string _TextType;
        private List<ExtAttribute> _ExtAttributes = new List<ExtAttribute>();
        private bool Changed = true;
        private string Render = "";

        /// <summary>Creates an empty Text instance.</summary>
        public Text() : this("") {}

        /// <summary>Creates a Text instance.</summary>
        /// <param name="text">The Text text.</param>
        /// <param name="id">The Text ID.</param>
        public Text(string text, string id = null)
        {
            if (text == null)
            {
                throw new ImpartError("Text cannot be null!");
            }
            _Text = text;
            _ID = id;
            _Type = Impart.TextType.Regular;
            _TextType = "p";
            if (id != null)
            {
                _ExtAttributes.Add(new ExtAttribute(ExtAttributeType.ID, id));
            }
        }

        /// <summary>Creates a Text instance with <paramref name="text"/> as the text and <paramref name="type"> as the Text type.</summary>
        /// <returns>A Text instance.</returns>
        /// <param name="text">The Text text.</param>
        /// <param name="type">The Text type.</param>
        /// <param name="id">The Text ID.</param>
        public Text(string text, TextType type, string id = null)
        {
            if (text == null)
            {
                throw new ImpartError("Text cannot be null!");
            }
            _Text = text;
            _ID = id;
            _Type = type;
            _TextType = type switch
            {
                TextType.Regular => "p",
                TextType.Bold => "b",
                TextType.Delete => "del",
                TextType.Emphasize => "em",
                TextType.Important => "strong",
                TextType.Insert => "ins",
                TextType.Italic => "i",
                TextType.Mark => "mark",
                TextType.Small => "small",
                TextType.Subscript => "sub",
                TextType.Superscript => "sup",
                _ => ""
            };
            if (id != null)
            {
                _ExtAttributes.Add(new ExtAttribute(ExtAttributeType.ID, id));
            }
        }

        /// <summary>Sets <paramref name="type"> with the value(s) in object[].</summary>
        /// <returns>A Text instance.</returns>
        /// <param name="type">The Attribute type.</param>
        /// <param name="value">The Attribute value(s).</param>
        public Text SetAttribute(AttributeType type, params object[] value)
        {
            _Attributes.Add(new Attribute(type, value));
            Changed = true;
            return this;
        }

        /// <summary>Returns the instance as a String.</summary>
        /// <returns>A String instance.</returns>
        public override string ToString()
        {
            if (!Changed)
            {
                return Render;
            }
            Changed = false;
            StringBuilder result = new StringBuilder();
            if (_Attributes.Count != 0)
            {
                result.Append($"<{_TextType} style\")");
                foreach (Attribute attribute in _Attributes)
                {
                    result.Append(attribute);
                }
                result.Append('"');
                foreach (ExtAttribute extAttribute in _ExtAttributes)
                {
                    result.Append(extAttribute);
                }
                Render = result.Append($">{_Text}</{_TextType}>").ToString();
                return Render;
            }
            result.Append($"<{_TextType}");
            foreach (ExtAttribute extAttribute in _ExtAttributes)
            {
                result.Append(extAttribute);
            }
            Render = result.Append($">{_Text}</{_TextType}>").ToString();
            return Render;
        }
        string Nested.First()
        {
            string result = ToString();
            return result.Remove(result.Length - ($"</{_TextType}>".Length) - 1);
        }
        string Nested.Last()
        {
            return $"</{_TextType}>";
        }
    }
}