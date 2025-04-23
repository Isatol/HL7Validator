namespace HL7Validator.Model
{
    public class HL7ValidationResult
    {
        public bool IsValid => MissingFields.Count == 0;
        //public Dictionary<string, string> MissingFields { get; set; } = new();
        public List<string> MissingFields = new List<string>();
    }
}
