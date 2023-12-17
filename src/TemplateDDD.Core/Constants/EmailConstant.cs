using TemplateDDD.Data.Dtos.Common;
using TemplateDDD.Data.Dtos.MonitorFailedHNITxn;

namespace TemplateDDD.Core.Constants
{
    public class EmailConstant
    {
        public static string EmailBody(EmailDto email, MonitoringFailedHNITxnDto monitoringFailedHNITxnDto)
        {
            string mutation = @$"Hello {monitoringFailedHNITxnDto.AccountName},
                    Your transaction with {monitoringFailedHNITxnDto.AccountNumber} and amount of {monitoringFailedHNITxnDto.Amount} failed.
                    We will validate this transaction again and send an email with the status

                    Thank you for banking with us.";

            return mutation;
        }
    }
}