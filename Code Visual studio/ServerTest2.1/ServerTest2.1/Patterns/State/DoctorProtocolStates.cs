using Server.ThreadHandlers;
using System;

namespace Server.Patterns.State.DoctorStates
{
    public class D_Welcome : DoctorState
    {
        public D_Welcome(DataProtocol protocol, DoctorHandler doctorHandler) : base(protocol, doctorHandler)
        {
        }

        public override string CheckInput(string input)
        {
            throw new System.NotImplementedException();
        }
    }

    public class D_Login : DoctorState
    {
        public D_Login(DataProtocol protocol, DoctorHandler doctorHandler) : base(protocol, doctorHandler)
        {
        }

        public override string CheckInput(string input)
        {
            throw new System.NotImplementedException();
        }
    }

    public class D_RecievingCommand : DoctorState
    {
        public D_RecievingCommand(DataProtocol protocol, DoctorHandler doctorHandler) : base(protocol, doctorHandler)
        {
        }

        public override string CheckInput(string input)
        {
            throw new System.NotImplementedException();
        }
    }


    public class D_RecieveData : DoctorState
    {
        public D_RecieveData(DataProtocol protocol, DoctorHandler doctorHandler) : base(protocol, doctorHandler)
        {
        }

        public override string CheckInput(string input)
        {
            throw new System.NotImplementedException();
        }
    }

    public class D_SendData : DoctorState
    {
        public D_SendData(DataProtocol protocol, DoctorHandler doctorHandler) : base(protocol, doctorHandler)
        {
        }

        public override string CheckInput(string input)
        {
            throw new System.NotImplementedException();
        }
    }

    public class D_Subscribing : DoctorState
    {
        public D_Subscribing(DataProtocol protocol, DoctorHandler doctorHandler) : base(protocol, doctorHandler)
        {
        }

        public override string CheckInput(string input)
        {
            throw new System.NotImplementedException();
        }
    }

    public class D_Unsubsribing : DoctorState
    {
        public D_Unsubsribing(DataProtocol protocol, DoctorHandler doctorHandler) : base(protocol, doctorHandler)
        {
        }

        public override string CheckInput(string input)
        {
            throw new System.NotImplementedException();
        }
    }
}
