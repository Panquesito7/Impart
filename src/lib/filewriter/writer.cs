using System.IO;
using System;

namespace Csweb
{
    internal class Writer
    {
        string autoMessage => "<!-- Generated by CSWeb - https://github.com/PixelatedLagg/CSWeb-lib -->";
        //Called once and only if there is nothing on the file
        internal void Write(string path)
        {
            if (!ValidPath(path))
            {
                throw new ArgumentException("Not a valid file path!");
            }
        }
        //Checks file path by relying the streamwriter class throwing an error
        internal bool ValidPath(string path)
        {
            try
            {
                using (StreamWriter _streamWriter = new StreamWriter(path)) 
                {
                    _streamWriter.Write(autoMessage);
                    _streamWriter.Close();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}