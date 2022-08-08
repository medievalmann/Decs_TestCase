namespace ConfigurationManager.API.DTOs
{
    public class ConfigurationSaveDTO
    {
        public string Name { get; set; }
        public byte Type { get; set; }
        public string Value { get; set; }
        public bool IsActive { get; set; }
        public string ApplicationId { get; set; }
    }
}
