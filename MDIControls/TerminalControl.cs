using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpindleTalker2
{
    public partial class TerminalControl : UserControl
    {
        public TerminalControl()
        {
            InitializeComponent();
        }

        private static bool IsValidHexString(IEnumerable<char> hexString)
        {
            return hexString.Select(currentCharacter =>
                        (currentCharacter >= '0' && currentCharacter <= '9') ||
                        (currentCharacter >= 'a' && currentCharacter <= 'f') ||
                        (currentCharacter >= 'A' && currentCharacter <= 'F')).All(isHexCharacter => isHexCharacter);
        }

    }
}
