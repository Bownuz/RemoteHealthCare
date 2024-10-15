using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerTest2._1
{
    public static class DoctorThread
    {
        public static void HandleDoctorThread(TcpClient doctor)
        {
            while (true)
            {
                String recived = DataProtocol.Messages.ReciveMessage(doctor);
                Console.WriteLine($"Doctor: {recived}");


            }


        }
    }
}
