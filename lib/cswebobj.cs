using System.Threading;
using System.Globalization;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Csweb
{
    public class cswebobj
    {
        private string path;
        internal string textCache;
        private string cssPath;
        private List<idstyle> styles = new List<idstyle>();
        public cswebobj(string path, string cssPath)
        {
            this.path = path;
            textCache = "";
            this.cssPath = cssPath;
        }
        public void AddStyle(idstyle style)
        {
            styles.Add(style);
        }
        public void AddText(string text, string id)
        {
            if (String.IsNullOrEmpty(text)) 
            {
                throw new ArgumentException("Text cannot be empty or null!");
            }
            if (String.IsNullOrEmpty(id))
            {
                textCache = $"{textCache}%^    <p>{text}</p>";
            }
            else
            {
                textCache = $"{textCache}%^    <p id=\"{id}\">{text}</p>";
            }
        }
        public void AddImage(string path, Nullable<(int x, int y)> dimensions, string id)
        {
            if (String.IsNullOrEmpty(path)) 
            {
                throw new ArgumentException("Image path cannot be empty or null!");
            }
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Image file not found!");
            }
            if (!Common.IsImage(Path.GetExtension(path)))
            {
                throw new ArgumentException("Unsupported file extension!");
            }
            if (dimensions != null)
            {
                if (String.IsNullOrEmpty(id))
                {
                    textCache = $"{textCache}%^    <img src=\"{path}\" width=\"{dimensions.Value.x}\" height=\"{dimensions.Value.y}\"></img>";
                }
                else 
                {
                    textCache = $"{textCache}%^    <img src=\"{path}\" id=\"{id}\" width=\"{dimensions.Value.x}\" height=\"{dimensions.Value.y}\"></img>";
                }
            }
            else
            {
                if (String.IsNullOrEmpty(id))
                {
                    textCache = $"{textCache}%^    <img src=\"{path}\"></img>";
                }
                else 
                {
                    textCache = $"{textCache}%^    <img src=\"{path}\" id=\"{id}\"></img>";
                }
            }
        }
        public void Render()
        {
            string tempCache = "";
            foreach (idstyle style in styles)
            {
                if (tempCache == "")
                {
                    tempCache = $"{tempCache}{style.Render()}{Environment.NewLine}";
                }
                else
                {
                    tempCache = $"{tempCache}{Environment.NewLine}{style.Render()}{Environment.NewLine}";
                }
            }
            Common.Delete(path);
            Common.Change(path, $"<!-- Generated by CSWeb - https://github.com/PixelatedLagg/CSWeb-lib -->"
            + $"{Environment.NewLine}<html>{Environment.NewLine}    <link rel=\"stylesheet\" href=\"{cssPath}\">"
            + $"{textCache.Replace("%^", Environment.NewLine)}{Environment.NewLine}</html>");
            Common.Delete(cssPath);
            Common.Change(cssPath, tempCache);
        }
    }
}