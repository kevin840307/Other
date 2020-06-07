using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomProgram
{
    class WordData
    {
        string _groupID = null;
        string[] _datas = null;
        List<Theme> _themes = new List<Theme>();
        Dictionary<string, int> _themesIndex = new Dictionary<string, int>();

        public WordData(string content, string groupID)
        {
            _datas = content.Replace("\f", "").Split(new string[] { "。 \r" }, StringSplitOptions.None);
            _groupID = groupID;
            ProcessReadWordDatas();
        }

        public string[] GetDatas()
        {
            return _datas;
        }

        public string GetGroup()
        {
            return _groupID;
        }

        public Theme GetTheme(string title)
        {
            
            return _themes[_themesIndex[title]];
        }

        public List<Theme> GetThemes()
        {
            return _themes;
        }

        private void ProcessReadWordDatas()
        {
            for (int index = 0; index < _datas.Length; index++)
            {
                int bracketsIndex = _datas[index].IndexOf("(");
                if (bracketsIndex < 0)
                    continue;

                int oneIndex =_datas[index].Substring(0, bracketsIndex).IndexOf("\r1.");
                if(oneIndex > 0)
                {
                    string title = _datas[index].Substring(0, oneIndex).Replace("\r", "").Replace(" ", "").Replace("\t", "");
                    if (_themes.Count != 0)
                    {
                        _themes[_themes.Count - 1].End = index;
                    }
                    _themesIndex[title] = _themes.Count;
                    _themes.Add(new Theme(title, index));

                }
                _datas[index] = _datas[index].Substring(bracketsIndex, _datas[index].Length - bracketsIndex);
                _datas[index] = FixProgram(_datas[index]);

            }

            if (_themes.Count != 0)
            {
                _themes[_themes.Count - 1].End = _datas.Length - 1;
            }
        }

        private string FixProgram(string program)
        {
            string newProgram = program;
            int index = -1;

            do
            {
                index = newProgram.IndexOf("\r ");
                if (index >= 0)
                {
                    int endIndex = index + 1;
                    while (newProgram[endIndex] == ' ')
                    {
                        endIndex++;
                    }
                    newProgram = newProgram.Substring(0, index) + "\r" + newProgram.Substring(endIndex, newProgram.Length - endIndex);
                }
            } while (index != -1);

            do
            {
                index = newProgram.IndexOf("\v ");
                if (index >= 0)
                {
                    int endIndex = index + 1;
                    while (newProgram[endIndex] == ' ')
                    {
                        endIndex++;
                    }
                    newProgram = newProgram.Substring(0, index) + "\r" + newProgram.Substring(endIndex, newProgram.Length - endIndex);
                }

            } while (index != -1);
            return newProgram;
        }
    }
}
