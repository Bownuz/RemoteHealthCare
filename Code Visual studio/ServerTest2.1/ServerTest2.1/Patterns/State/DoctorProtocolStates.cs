using Server.ThreadHandlers;
using System;

namespace Server.Patterns.State.DoctorStates
{
    public class D_Welcome(DataProtocol protocol, DoctorHandler doctorHandler) : DoctorState(protocol, doctorHandler)
    {
        public override string CheckInput(string input)
        {
            throw new System.NotImplementedException();
        }
    }

    public class D_Login(DataProtocol protocol, DoctorHandler doctorHandler) : DoctorState(protocol, doctorHandler)
    {
        public override string CheckInput(string input)
        {
            throw new System.NotImplementedException();
        }
    }

    public class D_RecievingCommand(DataProtocol protocol, DoctorHandler doctorHandler) : DoctorState(protocol, doctorHandler)
    {
        public override string CheckInput(string input)
        {
            throw new System.NotImplementedException();
        }
    }


    public class D_RecieveData(DataProtocol protocol, DoctorHandler doctorHandler) : DoctorState(protocol, doctorHandler)
    {
        public override string CheckInput(string input)
        {
            throw new System.NotImplementedException();
        }
    }

    public class D_SendData(DataProtocol protocol, DoctorHandler doctorHandler) : DoctorState(protocol, doctorHandler)
    {
        public override string CheckInput(string input)
        {
            throw new System.NotImplementedException();
        }
    }

    public class D_Subscribing(DataProtocol protocol, DoctorHandler doctorHandler) : DoctorState(protocol, doctorHandler)
    {
        public override string CheckInput(string input)
        {
            throw new System.NotImplementedException();
        }
    }

    public class D_Unsubsribing(DataProtocol protocol, DoctorHandler doctorHandler) : DoctorState(protocol, doctorHandler)
    {
        public override string CheckInput(string input)
        {
            throw new System.NotImplementedException();
        }
    }
}
