using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class InputManager : IInputManager
    {
        private string _content;
        public string Content
        {
            get { return _content; }
            set
            {
                if (value != null)
                {
                    _content = value;
                }
            }
        }
        public void ReadFromFile(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Invaild File Path");

            using (var srReader = new StreamReader(filePath))
            {
                _content = srReader.ReadToEnd();
            }
        }


    }
}
