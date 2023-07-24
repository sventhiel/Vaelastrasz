using Vaelastrasz.Library.Models;
using Vaelastrasz.Server.Entities;

namespace Vaelastrasz.Server.Extensions
{
    public static class DataCiteStateTypeExtensions
    {
        public static State ToState(this DataCiteStateType state)
        {
            switch(state)
            {
                case DataCiteStateType.Draft: return State.Draft;
                case DataCiteStateType.Findable: return State.Findable;
                case DataCiteStateType.Registered: return State.Registered;

                default: return State.Draft;
            }
        }
    }
}
