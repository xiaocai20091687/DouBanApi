﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace LuoWangDemo.Helper
{
    public class ConvertController
    {
        public Color GetColorFromHexString(string s)
        {
            byte a = System.Convert.ToByte("FF", 16);//Alpha should be 255
            byte r = System.Convert.ToByte(s.Substring(0, 2), 16);
            byte g = System.Convert.ToByte(s.Substring(2, 2), 16);
            byte b = System.Convert.ToByte(s.Substring(4, 2), 16);
            return Color.FromArgb(a, r, g, b);
        }
    }
}
