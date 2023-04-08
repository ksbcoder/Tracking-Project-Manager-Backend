namespace Projects.Domain.Commands.Inscription
{
    public class NewInscriptionCommand
    {
        public Guid ProjectID { get; set; }
        public string UidUser { get; set; }
    }
}
