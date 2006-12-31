/*
 * ManagedWinapi - A collection of .NET components that wrap PInvoke calls to 
 * access native API by managed code. http://mwinapi.sourceforge.net/
 * Copyright (C) 2006 Michael Schierl
 * 
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 * 
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library; see the file COPYING. if not, visit
 * http://www.gnu.org/licenses/lgpl.html or write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA
 */
using System;
using System.Collections.Generic;

namespace ManagedWinapi.Windows.Contents
{

    /// <summary>
    /// The content of a text box.
    /// </summary>
    public class TextContent : WindowContent
    {
        readonly string text;
        readonly bool password;

        internal TextContent(string text, bool password)
        {
            this.text = text;
            this.password = password;
        }

        ///
        public string ComponentType
        {
            get { return "Text"; }
        }

        ///
        public string ShortDescription
        {
            get
            {
                if (text.IndexOf("\n") != -1)
                    return "<MultiLine>";
                else if (password)
                    return text + " <Password>";
                else
                    return text;
            }
        }

        ///
        public string LongDescription
        {
            get
            {
                if (password)
                    return text + " <Password>";
                else
                    return text;
            }
        }

        ///
        public Dictionary<string, string> PropertyList
        {
            get
            {
                Dictionary<string, string> result = new Dictionary<string, string>();
                result.Add("Password", password ? "True" : "False");
                result.Add("MultiLine", text.IndexOf('\n') != -1 ? "True" : "False");
                result.Add("Text", text);
                return result;
            }
        }
    }

    class TextFieldParser : WindowContentParser
    {
        internal override bool CanParseContent(SystemWindow sw)
        {
            return sw.Title != "";
        }

        internal override WindowContent ParseContent(SystemWindow sw)
        {
            return new TextContent(sw.Title, sw.PasswordCharacter != 0);
        }
    }
}