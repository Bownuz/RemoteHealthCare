using Server.ThreadHandlers;

namespace Server.Patterns.State
{
	public abstract class PatientState : State
	{
		protected PatientHandler patientHandler;

        protected PatientState(DataProtocol protocol, PatientHandler patientHandler) : base(protocol)
        {
            this.patientHandler = patientHandler;
        }
    }

}

