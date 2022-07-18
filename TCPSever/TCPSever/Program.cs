using System;
using System.Net.Sockets;
using System.Text;
using System.Net;
using System.Threading;
using System.Collections.Generic;
namespace TCPSever
{
    class Program
    {
        private const int BUFFER_SIZE = 256;
        private const int PORT_NUMBER = 9000;
        static DateTime starttime = DateTime.Now;
        static ASCIIEncoding encoding = new ASCIIEncoding();
        static long rcount = 0;
        static long scount = 0;
        static long second = 1;
        
        static List<String> result=new List<String>();
        public static void Main()
        {
            ServerListen();
        }
       
        static void ServerListen()
        {
            try
            {
                IPAddress address = IPAddress.Parse("127.0.0.1");

                TcpListener listener = new TcpListener(address, PORT_NUMBER);
                
                // 1. listen
                listener.Start();
                // 1. connect
                Socket socket = listener.AcceptSocket();
                // 2. receive
                byte[] data = new byte[BUFFER_SIZE];
                Thread t = new Thread(()=>
                {
                    while (true)
                     {
                        try
                        {
                            socket.Receive(data);
                            String str = encoding.GetString(data);
                            Console.WriteLine(str);
                        }
                        catch
                        { }
                         rcount++;
                         
                     }
                        
                });
                Thread t1 = new Thread(() =>
                {
                    
                    
                    while (true)
                    {
                        
                        try
                        {
                            
                            socket.Send(encoding.GetBytes("Data received"));
                        }
                        catch 
                        { }
                        scount++;
                    }
                });
                Thread t3 = new Thread(() =>
                  {
                      while (true)
                      {
                          DateTime currentTime = DateTime.Now;
                          if (currentTime.Ticks - starttime.Ticks >= (TimeSpan.TicksPerSecond * second))
                          {
                              second = (currentTime.Ticks - starttime.Ticks) / (TimeSpan.TicksPerSecond) + 10;
                              Console.WriteLine("Last 10 Second recevied {0} times and sent {1}", rcount, scount);
                              rcount = 0;
                              scount = 0;
                          }
                          Thread.Sleep(10000);
                      }
                  });
                t.Start();
                t1.Start();
                
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }
        }
    }
}
   