using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomProgram
{
    class Theme
    {
        string _themeName = "";
        int _start = 0;
        int _end = 0;

        public Theme(string themeName)
        {
            _themeName = themeName;
        }

        public Theme(string themeName, int start)
        {
            _themeName = themeName;
            _start = start;
        }

        public Theme(string themeName, int start, int end)
        {
            _themeName = themeName;
            _start = start;
            _end = end;
        }

        public string ThemeName
        {
            set
            {
                _themeName = value;
            }
            get
            {
                return _themeName;
            }
        }

        public int Start
        {
            set
            {
                _start = value;
            }
            get
            {
                return _start;
            }
        }

        public int End
        {
            set
            {
                _end = value;
            }
            get
            {
                return _end;
            }
        }
    }
}
