using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoSieuThi
{
    class ListItem
    {
        public string Text { get; set; }
        public String Value { get; set; }

        public ListItem(string text, String value)
        {
            Text = text;
            Value = value;
        }
    }
}
