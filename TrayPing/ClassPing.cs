using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrayPing
{
    internal class ClassPing
    {
        public bool _break = false;
        public bool _found = false;

        public ClassPing() { }

        public void start()
        {
            Task.Run(() =>
            {
                while (_break == false)
                {
                    //Pingオブジェクトの作成
                    System.Net.NetworkInformation.Ping p =
                        new System.Net.NetworkInformation.Ping();
                    //"www.yahoo.com"にPingを送信する
                    System.Net.NetworkInformation.PingReply reply = p.Send("192.168.0.2");

                    //結果を取得
                    if (reply.Status == System.Net.NetworkInformation.IPStatus.Success)
                    {
                        System.Diagnostics.Debug.WriteLine("Reply from {0}:bytes={1} time={2}ms TTL={3}",
                            reply.Address, reply.Buffer.Length,
                            reply.RoundtripTime, reply.Options.Ttl);
                        _found = true;
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("Ping送信に失敗。({0})",
                            reply.Status);
                        _found = false;
                    }

                    p.Dispose();

                    System.Threading.Thread.Sleep(1000);
                }
            });
        }
    }
}
