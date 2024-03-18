
namespace Infraestrutura.Enum
{
    public enum ZenviaStatus
    {
      Ok = 00,
      Scheduled = 01,
      Sent = 02,
      Delivered = 03,
      NotReceived = 04,
      BlockedNoCoverage = 05,
      BlockedBlacklisted = 06,
      BlockedInvalidNumber = 07,
      BlockedContentNotAllowed = 08,
      Blocked = 09,
      Error = 10,
    }
}