using System;
using System.IO;

namespace Impart
{
    /// <summary>Class that represents an image.</summary>
    public class Image : Element
    {
        private string _path;
        private string _id;
        private string _style;
        private string _attributes;
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
        public string path 
        {
            get 
            {
                return _path;
            }
        }
        public string id 
        {
            get 
            {
                return _id;
            }
        }

        /// <summary>Constructor for the image class.</summary>
        public Image(string path, string id = null)
        {
            if (String.IsNullOrEmpty(path)) 
            {
                throw new ImageError("Path cannot be empty or null!", this);
            }
            if (!File.Exists(path))
            {
                throw new ImageError("Image file not found!", this);
            }
            if (!Common.IsImage(Path.GetExtension(path)))
            {
                throw new ImageError("Unsupported file extension!", this);
            }
            if (String.IsNullOrEmpty(id))
            {
                this._id = null;
            }
            else
            {
                this._id = id;
            }
            this._path = path;
            _style = "";
            _attributes = "";
        }

        /// <summary>Method for setting the image size.</summary>
        public Image SetSize(int x, int y)
        {
            if (x < 0 || y < 0)
            {
                throw new ImageError("Width and height values must be positive!", this);
            }
            _attributes += $" width=\"{x}\" height=\"{y}\"";
            return this;
        }

        /// <summary>Method for setting the image border.</summary>
        public Image SetBorder(int pixels, string border, Color color)
        {
            if (pixels < 0)
            {
                throw new ImageError("Border thickness must be above 0!", this);
            }
            if (!Border.Any(border))
            {
                throw new ImageError("Invalid border value!", this);
            }
            switch (color.GetType().FullName)
            {
                case "Impart.Rgb":
                    Rgb rgb = (Rgb)color;
                    _style += $" border: {pixels}px {border} rgb({rgb.rgb.r},{rgb.rgb.g},{rgb.rgb.b});";
                    break;
                case "Impart.Hsl":
                    Hsl hsl = (Hsl)color;
                    _style += $" border: {pixels}px {border} hsl({hsl.hsl.h}, {hsl.hsl.s}%, {hsl.hsl.l}%);";
                    break;
                case "Impart.Hex":
                    Hex hex = (Hex)color;
                    _style += $" border: {pixels}px {border} #{hex.hex};";
                    break;
            }
            return this;
        }
    } 
}