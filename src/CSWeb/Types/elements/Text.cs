using System;

namespace CSWeb
{
    public class Text : Element
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
            this._text = text.Str();
            this._id = id;
            _style = "";
            _attributes = "";
            colorCheck = 0;
        }
        public Text AddColor(Hex hex)
        {
            if (colorCheck > 1)
            {
                throw new TextError("Cannot set color more than once!", this);
            }
            colorCheck++;
            _style += $" color: #{hex.hex};";
            return this;
        }
        public Text AddColor(Hsl hsl)
        {
            if (colorCheck > 1)
            {
                throw new TextError("Cannot set color more than once!", this);
            }
            colorCheck++;
            _style += $" color: hsl({hsl.hsl.h}, {hsl.hsl.s}%, {hsl.hsl.l}%);";
            return this;
        }
        public Text AddColor(Rgb rgb)
        {
            if (colorCheck > 1)
            {
                throw new TextError("Cannot set color more than once!", this);
            }
            colorCheck++;
            _style += $" color: rgb({rgb.rgb.r},{rgb.rgb.g},{rgb.rgb.b});";
            return this;
        }
        public Text SetMargin(int pixels)
        {
            if (pixels < 0)
            {
                throw new TextError("Invalid margin value!", this);
            }
            _style += $" margin: {pixels}px;";
            return this;
        }
        public Text SetHoverMessage(string message)
        {
            if (String.IsNullOrEmpty(message))
            {
                throw new TextError("Hover message cannot be empty or null!", this);
            }
            _attributes += $" title=\"{message.Str()}\"";
            return this;
        }
        public Text SetFontSize(int pixels)
        {
            if (pixels < 0)
            {
                throw new TextError("Invalid font size!", this);
            }
            _style += $" font-size: {pixels}px;";
            return this;
        }
    } 
}