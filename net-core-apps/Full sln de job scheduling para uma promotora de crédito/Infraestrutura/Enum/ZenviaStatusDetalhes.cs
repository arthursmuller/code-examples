
using System.ComponentModel;

namespace Infraestrutura.Enum
{
    public enum ZenviaStatusDetalhes
    {
      [Description("Message Sent")]
      MessageSent = 000,

      [Description("Message Successfully Canceled")]
      MessageSuccessfullyCanceled = 002,

      [Description("Empty message content")]
      EmptyMessageContent = 010,

      [Description("Message body invalid")]
      MessageBodyInvalid = 011,

      [Description("Message content overflow")]
      MessageContentOverflow = 012,

      [Description("Incorrect or incomplete ‘to’ mobile number")]
      InvalidNumber = 013,

      [Description("Empty ‘to’ mobile number")]
      EmptyNumber = 014,

      [Description("Scheduling date invalid or incorrect")]
      SchedulingDateInvalid = 015,

      [Description("ID overflow")]
      IdOverflow = 016,

      [Description("Parameter ‘url’ is invalid or incorrect")]
      InvalidUrl = 017,

      [Description("Field ‘from’ invalid")]
      InvalidFrom = 018,

      [Description("‘id’ fieldismandatory")]
      MissingId = 021,

      [Description("Message with same ID already sent")]
      IdAlreadySent = 080,

      [Description("Message Queued")]
      MessageQueued = 100,

      [Description("Message sent to operator")]
      MessageSentToOperator = 110,

      [Description("Message confirmation unavailable")]
      MessageConfirmationUnavailable = 111,

      [Description("Message received by mobile")]
      MessageReceived = 120,
      
      [Description("Message blocked")]
      MessageBlocked = 130,

      [Description("Message blocked by predictive cleansing")]
      MessageBlockedByPredictiveCleansing = 131,

      [Description("Message already canceled")]
      MessageAlreadyCanceled = 132,

      [Description("Message content in analysis")]
      MessageContentInAnalysis = 133,

      [Description("Message blocked by forbidden content")]
      MessageBlockedByForbiddenContent = 134,

      [Description("Aggregate is Invalid or Inactive")]
      AggregateInvalid = 135,

      [Description("Message expired")]
      MessageExpired = 136,

      [Description("Mobile number not covered")]
      NumberNotCovered = 140,

      [Description("International sending not allowed")]
      InternationalNotAllowed = 141,

      [Description("Inactive mobile number")]
      InactiveNumber = 145,

      [Description("Message expired in operator")]
      MessageExpiredByOperator = 150,

      [Description("Operator network error")]
      OperatorNetworkError = 160,

      [Description("Message rejected by operator")]
      MessageRejectedByOperator = 161,

      [Description("Message cancelled or blocked by operator")]
      MessageCancelledByOperator = 162,

      [Description("Bad message")]
      BadMessage = 170,

      [Description("Bad number")]
      BadNumber = 171,

      [Description("Missing parameter")]
      MissingParameter = 172,

      [Description("Message ID notfound")]
      IdNotFound = 180,

      [Description("Unknown error")]
      MessageUnknownError = 190,

      [Description("Messages Sent")]
      MessagesSent = 200,

      [Description("Messages scheduled but Account Limit Reached")]
      LimitReached = 210,

      [Description("File empty or not sent")]
      FileNotSent = 240,

      [Description("File too large")]
      FileTooLarge = 241,

      [Description("File readerror")]
      FileReadError = 242,

      [Description("Received messages found")]
      ReceivedMessagesFound = 300,

      [Description("No received messages found")]
      NotFound = 301,

      [Description("Entity saved")]
      Entitysaved = 400,

      [Description("Authentication error")]
      AuthenticationError = 900,

      [Description("Account type not support this operation")]
      NotSupported = 901,

      [Description("Account Limit Reached – Please contact support")]
      AccountLimitReached = 990,

      [Description("Wrong operation requested")]
      WrongOperation = 998,

      [Description("Unknown Error")]
      UnknownError = 999,
    }
}