using System;
using System.Threading.Tasks;

namespace DoctorApplication {
    public enum DoctorStateEnum {
        Connecting,
        Login,
        CommandType,
        RetrieveData,
        Subscribe,
        Unsubscribe,
        SendData,
        Exit
    }

    public class DoctorState {
        private Form1 form;
        private ServerConnection serverConnection;
        private DoctorStateEnum currentState;

        public DoctorState(Form1 form, ServerConnection serverConnection) {
            this.form = form;
            this.serverConnection = serverConnection;
            currentState = DoctorStateEnum.Login; 
        }

        public void EnterCommandTypeState() {
           
            currentState = DoctorStateEnum.CommandType;
            form.UpdateStatus("Klaar om commando's te ontvangen...");
            WaitForCommand();
        }

        private async void WaitForCommand() {
           
            string command = form.GetUserInput(); 

            switch(command.ToLower()) {
                case "retrievedata":
                    currentState = DoctorStateEnum.RetrieveData;
                    RetrieveData();
                    break;

                case "senddata":
                    currentState = DoctorStateEnum.SendData;
                    SendData();
                    break;

                case "subscribe":
                    currentState = DoctorStateEnum.Subscribe;
                    Subscribe();
                    break;

                case "unsubscribe":
                    currentState = DoctorStateEnum.Unsubscribe;
                    Unsubscribe();
                    break;

                case "exit":
                    currentState = DoctorStateEnum.Exit;
                    form.Close();
                    break;

                default:
                    form.UpdateStatus("Ongeldig commando ontvangen.");
                    break;
            }
        }

        private void RetrieveData() {
           
            string patientName = form.GetPatientName();
            DateTime sessionDate = form.GetSessionDate();

            var command = new {
                Action = "RetrieveData",
                PatientName = patientName,
                SessionDate = sessionDate
            };

            serverConnection.SendCommandToServer(command);
            form.UpdateStatus($"Gegevens opgevraagd voor patiënt {patientName} op {sessionDate}");
        }

        private void SendData() {
            
            string patientName = form.GetPatientName();
            string message = form.GetMessage();

            var command = new {
                Action = "SendData",
                PatientName = patientName,
                Message = message
            };

            serverConnection.SendCommandToServer(command);
            form.UpdateStatus($"Bericht verzonden naar patiënt {patientName}: {message}");
        }

        private void Subscribe() {
            
            string patientName = form.GetPatientName();

            var command = new {
                Action = "Subscribe",
                PatientName = patientName
            };

            serverConnection.SendCommandToServer(command);
            form.UpdateStatus($"Geabonneerd op patiënt {patientName}");
        }

        private void Unsubscribe() {
            
            string patientName = form.GetPatientName();

            var command = new {
                Action = "Unsubscribe",
                PatientName = patientName
            };

            serverConnection.SendCommandToServer(command);
            form.UpdateStatus($"Afgemeld van patiënt {patientName}");
        }
    }
}
