namespace TeamManagement.ViewModals
{
    public class PlayerView
    {
        #region Properties
        public string PlayerName { get; set; }
        public string PlayerEmail { get; set; }
        public string CoachName { get; set; }

        public string CoachEmail { get; set; }

        public string CaptainName { get; set; }
        public string CaptainEmail { get; set; }
        #endregion

        #region Method
        public override string ToString()
        {
            return $"Welcome {PlayerName},You are in a team.\n" +
                $"Your email id is {PlayerEmail}\n" +
                $"Coach : {CoachName}\n" +
                $"Coach Email : {CoachEmail}\n" +
                $"Captain : {CaptainName}\n" +
                $"Captain Email : {CaptainEmail}";
        }
        #endregion
    }
}
