using OBSWebsocketDotNet;
using System;
using System.Windows.Forms;
using NHotkey.WindowsForms;
using NHotkey;

namespace OBS_RemoteHotkey
{
    public class OBS
    {
        protected OBSWebsocket _obs;
        public OBS()
        {
            _obs = new OBSWebsocket();
            HotkeyManager.Current.AddOrReplace("Save Replay", Keys.Control | Keys.F1, SaveReplay);
        }

        protected void Connect()
        {
            if (!_obs.IsConnected)
            {
                try
                {
                    _obs.Connect("ws://"+AppSettingsManager.GetValueString(RegOptions.RemoteIP)+":4444", AppSettingsManager.GetValueString(RegOptions.RemotePasswd));
                }
                catch (AuthFailureException)
                {
                    MessageBox.Show("Authentication failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                catch (ErrorResponseException ex)
                {
                    MessageBox.Show("Connect failed : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Connect failed : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
        }

        public void SaveReplay(object sender, HotkeyEventArgs e)
        {
            if (!_obs.IsConnected)
                Connect();
            if(_obs.IsConnected)
                _obs.SaveReplayBuffer();
            e.Handled = true;
        }
    }
}
