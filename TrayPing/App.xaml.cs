using System.Configuration;
using System.Data;
using System.Windows;

namespace TrayPing
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        ClassPing? pinger = null;
        public bool _break = false;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var iconN = new System.Drawing.Icon(GetResourceStream(new Uri("/IconN.ico", UriKind.Relative)).Stream);
            var iconR = new System.Drawing.Icon(GetResourceStream(new Uri("/IconR.ico", UriKind.Relative)).Stream);
            var iconB = new System.Drawing.Icon(GetResourceStream(new Uri("/IconB.ico", UriKind.Relative)).Stream);

            var menu = new System.Windows.Forms.ContextMenuStrip();
            _ = menu.Items.Add("終了", null, Exit_Click);
            var notifyIcon = new System.Windows.Forms.NotifyIcon
            {
                Visible = true,
                Icon = iconN,
                Text = "タスクトレイ常駐アプリのテストです",
                ContextMenuStrip = menu
            };
            notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(NotifyIcon_Click);

            Task.Run(() =>
            {
                while (_break == false)
                {
                    if (pinger == null)
                    {
                        System.Threading.Thread.Sleep(100);
                        continue;
                    }

                    if (pinger == null)
                    {
                        notifyIcon.Icon = iconN;
                    }
                    else
                    {
                        notifyIcon.Icon = (pinger._found) ? iconB : iconR;
                    }
                    System.Threading.Thread.Sleep(100);
                }
            });

        }

        private void NotifyIcon_Click(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //if (e.Button == System.Windows.Forms.MouseButtons.Left)
            //{
            //    var wnd = new MainWindow();
            //    wnd.Show();
            //}

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (pinger != null)
                {
                    return;
                }

                pinger = new ClassPing();
                pinger.start();

            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (pinger != null)
                {
                    _break = true;
                    pinger._break = _break;
                }
                System.Threading.Thread.Sleep(3000);
                Shutdown();
            }
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Shutdown();
        }
    }

}
