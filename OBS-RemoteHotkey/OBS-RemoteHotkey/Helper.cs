using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OBS_RemoteHotkey
{
    public static class Helper
    {
        public static Key RealKey(this System.Windows.Forms.KeyEventArgs e)
        {
            switch ((Key)e.KeyCode)
            {
                case Key.System:
                    return (Key)e.KeyCode;

                case Key.ImeProcessed:
                    return (Key)e.KeyCode;

                case Key.DeadCharProcessed:
                    return (Key)e.KeyCode;

                default:
                    return (Key)e.KeyCode;
            }
        }
    }
}
