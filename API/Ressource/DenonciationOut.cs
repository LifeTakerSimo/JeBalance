namespace API.Ressource
{
    public class DenonciationOut
    {
        public string InformantLastName { get; set; }
        public string SuspectLastName { get; set; }
        public string Offense { get; set; }
        public string EvasionCountry { get; set; }
        public decimal? Amount { get; set; }
        public DateTime Timestamp { get; set; }
        public bool ResponseType { get; set; }
        public int DenonciationId { get; set; }

        public DenonciationOut()
        {
        }

        public DenonciationOut(string informantLastName, string suspectLastName, string offense, string evasionCountry, decimal? amount, DateTime timestamp, bool responseType, int id)
        {
            InformantLastName = informantLastName;
            SuspectLastName = suspectLastName;
            Offense = offense;
            EvasionCountry = evasionCountry;
            Amount = amount;
            Timestamp = timestamp;
            ResponseType = responseType;
            DenonciationId = id;
        }
    }
}
