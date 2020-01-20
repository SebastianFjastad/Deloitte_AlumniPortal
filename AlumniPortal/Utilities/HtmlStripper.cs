using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace AlumniPortal.Utilities
{
    public static class HtmlStripper
    {
        public static string Strip(string input)
        {
            return Regex.Replace(input, "<.*?>", String.Empty);
        }
    }
}