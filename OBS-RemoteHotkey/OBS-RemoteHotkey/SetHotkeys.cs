using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace OBS_RemoteHotkey
{
    public partial class SetHotkeys : Form
    {
        public SetHotkeys()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Load Saved Hotkey Settings from Registry
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetHotkeys_Load(object sender, EventArgs e)
        {
            // Stream Settings
            StartStream.Text = AppSettingsManager.GetValueString(Streaming.Start);
            StopStream.Text = AppSettingsManager.GetValueString(Streaming.Stop);

            // Recording Settings
            StartRecording.Text = AppSettingsManager.GetValueString(Recording.Start);
            StopRecording.Text = AppSettingsManager.GetValueString(Recording.Stop);
            PauseRecording.Text = AppSettingsManager.GetValueString(Recording.Paused);
            ResumeRecording.Text = AppSettingsManager.GetValueString(Recording.Resumed);

            // Replay Settings
            SaveReplay.Text = AppSettingsManager.GetValueString(Replay.Save);
        }

        private void StartStream_KeyPressed(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            var nonShortcuttableKeys = new[] { Keys.Alt, Keys.LControlKey, Keys.RControlKey, Keys.LShiftKey, Keys.RShiftKey };
            var actualKey = (Keys)e.KeyChar;

            if (!nonShortcuttableKeys.Contains(actualKey))
            {
                var tb = sender as TextBox;

                var modifiers = new List<ModifierKeys>();
                if (Keyboard.Modifiers.HasFlag((ModifierKeys)2))
                {
                    modifiers.Add((ModifierKeys)2);
                }

                if (Keyboard.Modifiers.HasFlag((ModifierKeys)1))
                {
                    modifiers.Add((ModifierKeys)1);
                }

                if (Keyboard.Modifiers.HasFlag((ModifierKeys)4))
                {
                    modifiers.Add((ModifierKeys)4);
                }

                tb.Text = modifiers.Count == 0
                    ? string.Format("{0}", actualKey.ToString())
                    : string.Format("{0} + {1}", string.Join(" + ", modifiers), actualKey.ToString());
            }

            e.Handled = true;
            AppSettingsManager.SetValueString(Streaming.Start, actualKey.ToString());
        }
    }
}
