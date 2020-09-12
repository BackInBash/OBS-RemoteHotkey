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
            var StreamStart = AppSettingsManager.GetValueString(Streaming.Start);
            var StreamStop = AppSettingsManager.GetValueString(Streaming.Stop);

            var RecordingStart = AppSettingsManager.GetValueString(Recording.Start);
            var RecordingStop = AppSettingsManager.GetValueString(Recording.Stop);
            var RecordingPause = AppSettingsManager.GetValueString(Recording.Paused);
            var RecodingResume = AppSettingsManager.GetValueString(Recording.Resumed);

            var ReplaySave = AppSettingsManager.GetValueString(Replay.Save);

            // Stream Settings
            if (!string.IsNullOrWhiteSpace(StreamStart))
                StartStream.Text = ((Keys)int.Parse(StreamStart)).ToString();
            if(!string.IsNullOrWhiteSpace(StreamStop))
                StopStream.Text = ((Keys)int.Parse(StreamStop)).ToString();

            // Recording Settings
            if(!string.IsNullOrWhiteSpace(RecordingStart))
                StartRecording.Text = AppSettingsManager.GetValueString(Recording.Start);
            if(!string.IsNullOrWhiteSpace(RecordingStop))
                StopRecording.Text = AppSettingsManager.GetValueString(Recording.Stop);
            if(!string.IsNullOrWhiteSpace(RecordingPause))
                PauseRecording.Text = AppSettingsManager.GetValueString(Recording.Paused);
            if(!string.IsNullOrWhiteSpace(RecodingResume))
                ResumeRecording.Text = AppSettingsManager.GetValueString(Recording.Resumed);

            // Replay Settings
            if(!string.IsNullOrWhiteSpace(ReplaySave))
                SaveReplay.Text = AppSettingsManager.GetValueString(Replay.Save);
        }

        private void StartStream_KeyPressed(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            var nonShortcuttableKeys = new[] { Keys.Alt, Keys.LControlKey, Keys.RControlKey, Keys.LShiftKey, Keys.RShiftKey };
            var actualKey = (Keys)e.KeyChar;
            var tb = sender as TextBox;
            if (!nonShortcuttableKeys.Contains(actualKey))
            {
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
                if (modifiers.Count != 0)
                {
                    AppSettingsManager.SetValueString(Streaming.Start, (int)modifiers[0] + ";" + (int)actualKey);
                }
                else
                {
                    AppSettingsManager.SetValueString(Streaming.Start, ((int)actualKey).ToString());
                }
            }

            e.Handled = true;
        }
    }
}
