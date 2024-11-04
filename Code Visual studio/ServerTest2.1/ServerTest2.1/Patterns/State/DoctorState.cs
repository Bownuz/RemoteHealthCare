using Server.ThreadHandlers;

namespace Server.Patterns.State {
    public abstract class DoctorState : State {
        protected DoctorHandler doctorHandler;

        protected DoctorState(DataProtocol protocol, DoctorHandler doctorHandler) : base(protocol) {
            this.doctorHandler = doctorHandler;
        }

        public override bool isJson(string input) {
            if (!jsonRegex.IsMatch(input)) {
                MessageCommunication.SendMessage(doctorHandler.networkStream, ValidMessages.a_notJson);
            }
            return jsonRegex.IsMatch(input);
        }
    }
}

