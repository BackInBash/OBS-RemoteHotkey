using OBSWebsocketDotNet;
using System;
using System.Windows.Forms;

namespace OBS_RemoteHotkey
{
    public partial class Options : Form
    {
        public Options()
        {
            InitializeComponent();
        }

        private void Save_Click(object sender, EventArgs e)
        {
            AppSettingsManager.SetValueString(RegOptions.RemoteIP, IP.Text);
            AppSettingsManager.SetValueString(RegOptions.RemotePasswd, Password.Text);
        }

        private void Options_Load(object sender, EventArgs e)
        {
            string ip = AppSettingsManager.GetValueString(RegOptions.RemoteIP);
            string passwd = AppSettingsManager.GetValueString(RegOptions.RemotePasswd);

            if (!string.IsNullOrWhiteSpace(ip))
            {
                IP.Text = ip;
            }

            if (!string.IsNullOrWhiteSpace(passwd))
            {
                Password.Text = passwd;
            }
        }

        private void Test_Click(object sender, EventArgs e)
        {
            OBSWebsocket _obs = new OBSWebsocket();
            try
            {
                _obs.Connect("ws://" + AppSettingsManager.GetValueString(RegOptions.RemoteIP) + ":4444", AppSettingsManager.GetValueString(RegOptions.RemotePasswd));
            }
            catch (AuthFailureException)
            {
                MessageBox.Show("Authentication failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
            if (!_obs.IsConnected)
            {
                MessageBox.Show("Connect failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (_obs.IsConnected)
            {
                MessageBox.Show("Connection Successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            _obs.Disconnect();
        }
    }
}
