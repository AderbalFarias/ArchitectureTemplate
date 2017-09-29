namespace ArchitectureTemplate.Mvc.Models.Shared
{
    public class Message
    {
        #region Constructors
        #endregion

        #region Enuns

        /// <summary>
        /// Enum que define os possíveis tipos de mensagens
        /// </summary>
        public enum MessageKind
        {
            Warning, 
            Success, 
            Error, 
            Information
        };

        #endregion

        #region Properties
        public string Title { get; set; }

        public string Detail { get; set; }

        public int? TimeoutSeconds { get; set; }

        public MessageKind Kind { get; set; }
        #endregion

        #region Methods
        #endregion

        #region Dispose/Destructors
        #endregion
    }
}