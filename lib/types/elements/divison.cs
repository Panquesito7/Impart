using System.Linq.Expressions;
using System;
using System.Drawing;

namespace Csweb
{
    public class Division : IDisposable
    {
        private int colorCheck;
        private string elementCache;
        private string styleCache;
        private string attributeCache;
        internal string textCache 
        {
            get 
            {
                if (styleCache == "")
                {
                    return $"%^    <div{attributeCache}>{elementCache}%^    </div>";
                }
                else
                {
                    return $"%^    <div{attributeCache} style=\"{$"\"{styleCache}".Replace("\" ", "")}\">{elementCache}%^    </div>";
                }
            }
        }
        public Division(DivisionType? type = null, string id = null)
        {
            if (String.IsNullOrEmpty(id))
            {
                id = null;
            }
            switch (type, id)
            {
                case (DivisionType, string) a when a != (null, null):
                    if (type == DivisionType.ID)
                    {
                        attributeCache = $"id=\"{id}\"";
                    }
                    else
                    {
                        attributeCache = $"class=\"{id}\"";
                    }
                    break;
                case (DivisionType, string) b when b.type == null && b.id != null:
                    throw new DivisionError("Type and ID must both be null or not null!", this);
                case (DivisionType, string) c when c.type != null && c.id == null:
                    throw new DivisionError("Type and ID must both be null or not null!", this);
            }
            colorCheck = 0;
        }
        public void AddText(Text text)
        {
            Timer.StartTimer();
            if (text.id == null)
            {
                elementCache += $"%^        <p{text.attributes}{text.style}>{text.text}</p>";
            }
            else
            {
                elementCache += $"%^        <p id=\"{text.id}\"{text.attributes}{text.style}>{text.text}</p>";
            }
            Debug.CallObjectEvent("[division] added text element");
        }
        public void AddImage(Image image)
        {
            Timer.StartTimer();
            if (image.id == null)
            {
                elementCache += $"%^        <img src=\"{image.path}\"{image.attributes}{image.style}>";
            }
            else
            {
                elementCache += $"%^        <img src=\"{image.path}\" id=\"{image.id}\"{image.attributes}{image.style}>";
            }
            Debug.CallObjectEvent("[division] added image element");
        }
        public Division AddColor(Color color)
        {
            Timer.StartTimer();
            if (colorCheck > 1)
            {
                throw new DivisionError("Cannot set color more than once!", this);
            }
            colorCheck++;
            styleCache += $" color: {color.ToKnownColor()};";
            Debug.CallObjectEvent("[division] added color");
            return this;
        }
        public Division AddColor(string hex)
        {
            Timer.StartTimer();
            if (colorCheck > 1)
            {
                throw new DivisionError("Cannot set color more than once!", this);
            }
            colorCheck++;
            if (hex == null || hex.Length != 6)
            {
                throw new DivisionError("Invalid hex value!", this);
            }
            styleCache += $" color: #{hex};";
            Debug.CallObjectEvent("[division] added color");
            return this;
        }
        public Division AddColor(int x, int y, int z)
        {
            Timer.StartTimer();
            if (colorCheck > 1)
            {
                throw new DivisionError("Cannot set color more than once!", this);
            }
            if (!(x >= 0 && y >= 0 && z >= 0 && x <= 255 && y <= 255 && z <= 255))
            {
                throw new DivisionError("Invalid RGB value!", this);
            }
            colorCheck++;
            styleCache += $" color: rgb({x},{y},{z});";
            Debug.CallObjectEvent("[division] added color");
            return this;
        }
        public Division SetBorder(int pixels, string border, Color color)
        {
            Timer.StartTimer();
            if (pixels < 0)
            {
                throw new DivisionError("Border thickness must be above 0!", this);
            }
            if (!Border.Any(border))
            {
                throw new DivisionError("Invalid border value!", this);
            }
            styleCache = $" border: {pixels}px {border} {color.ToKnownColor()};";
            Debug.CallObjectEvent("[division] added border");
            return this;
        }
        public Division SetBorder(float percent, string border, Color color)
        {
            Timer.StartTimer();
            if (percent > 1 || percent < 0)
            {
                throw new DivisionError("Percent number must be between 1-0!", this);
            }
            if (!Border.Any(border))
            {
                throw new DivisionError("Invalid border value!", this);
            }
            styleCache = $" border: {percent * 100}% {border} {color.ToKnownColor()};";
            Debug.CallObjectEvent("[division] added border");
            return this;
        }
        public void Dispose() {}
    } 
    public enum DivisionType
    {
        ID = 1,
        Class = 0
    }
}