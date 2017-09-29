using System;

namespace ArchitectureTemplate.Mvc.Controllers.Shared
{
    public class ActionType : Attribute
    {
        public AccessType AccessType { get; set; }

        public ActionType(AccessType accessType)
        {
            AccessType = accessType;
        }
    }

    public enum AccessType
    {
        Create = 1,
        Read = 2,
        Update = 3,
        Delete = 4,
    }
}