using System;
using System.Drawing;

namespace Csweb
{
    public class Text
    {
        private string _text;
        private string _id;
        private string _style;
        private string _attributes;
        private int colorCheck;
        internal string attributes 
        {
            get
            {
                return _attributes;
            }
        }
        internal string style 
        {
            get
            {
                if (_style == "")
                {
                    return "";
                }
                return $" style=\"{_style}\"".Replace("\" ", "\"");
            }
        }
        public string text 
        {
            get 
            {
                return _text;
            }
        }
        public string id 
        {
            get 
            {
                return _id;
            }
        }
        public Text(string text, string id = null)
        {
            if (String.IsNullOrEmpty(text))
            {
                throw new TextError("Text cannot be null or empty!", null);
            }
            this._text = text;
            this._id = id;
            _style = "";
            _attributes = "";
            colorCheck = 0;
        }
        public Text AddColor(Color color)
        {
            if (colorCheck > 1)
            {
                throw new TextError("Cannot set color more than once!", this);
            }
            colorCheck++;
            _style = $"{_style} color: {color.ToKnownColor()};";
            return this;
        }
        public Text AddColor(string hex)
        {
            if (colorCheck > 1)
            {
                throw new TextError("Cannot set color more than once!", this);
            }
            colorCheck++;
            if (hex == null || hex.Length != 6)
            {
                throw new TextError("Invalid hex value!", this);
            }
            _style = $"{_style} color: #{hex};";
            return this;
        }
        public Text AddColor(int x, int y, int z)
        {
            if (colorCheck > 1)
            {
                throw new TextError("Cannot set color more than once!", this);
            }
            colorCheck++;
            if (!(x >= 0 && y >= 0 && z >= 0 && x <= 255 && y <= 255 && z <= 255))
            {
                throw new TextError("Invalid RGB value!", this);
            }
            _style = $"{_style} color: rgb({x},{y},{z});";
            return this;
        }
        public Text SetHoverMessage(string message)
        {
            if (String.IsNullOrEmpty(message))
            {
                throw new TextError("Hover message cannot be empty or null!", this);
            }
            _attributes = $"{_attributes} title=\"{message}\"";
            return this;
        }
        public Text SetDraggable(bool obj)
        {
            _attributes = $"{_attributes} draggable=\"{obj}\"";
            return this;
        }
        public Text SetFontSize(int pixels)
        {
            if (pixels < 0)
            {
                throw new TextError("Font size cannot be negative!", this);
            }
            _style = $"{_style} font-size: {pixels}px;";
            return this;
        }
    } 
}