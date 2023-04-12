using BE.Data.Enums;

namespace BE.Data.Interfaces
{
    public interface ISwitchable
    {
        Status Status { get; set; }
    }
}