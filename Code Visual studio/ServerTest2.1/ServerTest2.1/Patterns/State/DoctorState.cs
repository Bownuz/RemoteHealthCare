using Server.ThreadHandlers;

namespace Server.Patterns.State
{
	public abstract class DoctorState : State
	{
		protected DoctorHandler doctorHandler;

        protected DoctorState(DataProtocol protocol, DoctorHandler doctorHandler) : base(protocol)
        {
            this.doctorHandler = doctorHandler;
        }
    }

}

