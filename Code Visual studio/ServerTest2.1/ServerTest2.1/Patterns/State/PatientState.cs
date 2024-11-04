using Server.ThreadHandlers;

namespace Server.Patterns.State {
    public abstract class PatientState : State {
        protected PatientHandler patientHandler;

        protected PatientState(DataProtocol protocol, PatientHandler patientHandler) : base(protocol) {
            this.patientHandler = patientHandler;
        }

        public override bool isJson(string input) {
            if (!jsonRegex.IsMatch(input)) {
                MessageCommunication.SendMessage(patientHandler.networkStream, ValidMessages.a_notJson);
            }
            return jsonRegex.IsMatch(input);
        }

    }

}

