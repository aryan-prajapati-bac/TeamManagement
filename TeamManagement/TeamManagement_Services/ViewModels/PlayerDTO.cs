namespace TeamManagement.ViewModals
{
    public class PlayerDTO
    {
        public string PlayerName { get; set; }
        public string PlayerEmail { get; set; }
        public string CoachName { get; set; }

        public string CoachEmail { get; set; }

        public string CaptainName { get; set; }
        public string CaptainEmail { get; set; }

        public override string ToString()
        {
            return $"Welcome {PlayerName},You are in a team.\n" +
                $"Your email id is {PlayerEmail}\n" +
                $"Coach : {CoachName}\n" +
                $"Coach Email : {CoachEmail}\n" +
                $"Captain : {CaptainName}\n" +
                $"Captain Email : {CaptainEmail}";
        }
    }
}
