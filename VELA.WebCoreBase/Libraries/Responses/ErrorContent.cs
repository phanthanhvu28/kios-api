using VELA.WebCoreBase.Libraries.Exceptions;

namespace VELA.WebCoreBase.Libraries.Responses;

public struct ErrorContent
{
    public static string CombineMessage(
        string message,
        params object?[] @params)
    {
        if (@params is null or { Length: 0 })
        {
            return message.Trim();
        }

        return $"{string.Format(message, @params)}".Trim();
    }

    public static string CombineMessage(
       CommonExceptionBase exception,
       params object?[] @params)
    {
        string? result = GetMessage(exception?.ErrorCode);
        if (string.IsNullOrWhiteSpace(result))
        {
            return exception?.Message;
        }

        if (@params is null or { Length: 0 })
        {
            return result.Trim();
        }

        return $"{string.Format(result, @params)}".Trim();
    }


    public static string GetMessage(int? errorCode)
    {
        return errorCode switch
        {
            100001 => "Upload maximum {0} files",
            100002 => "Please attach files or add type document",
            100003 => "Incorrect valid from {0} or valid to {1}",
            100004 => "Couldn’t find any cost data to {0}",
            100005 => "Operation {0} failed",
            100006 => "Access deny, you have no permission to {0}",
            100007 => "Not found {0}",
            100008 => "Invalid request",
            100009 => "Type of document incorrect",
            100010 => "Invalid status to {0}",
            100011 => "Upload maximum {0} files",
            100012 => "Upload file size maximum {0} Bytes",
            100013 => "Contract creation is not allowed. Because supplier {0} has a valid contract!",//MESCON0004            
            100014 => "The {0} is required",
            100015 => "Status annex not delete when has process flow",
            100016 => "{0} creation is not allowed. Because {1} number {2} exists!",
            100017 => "Inactive valid to {0}",
            100018 => "Invalid type file",
            100019 => "The duration annex must be within duration contract from {0} to {1}",
            100020 => "Contract is inactive",
            100021 => "Annex type {0} exists",
            100022 => "Quotation '{0}' is created by annex or agreement",
            100023 => "Billing cycle '{0}' invalid",
            100024 => "Due date '{0}' invalid",
            100025 => "Trial contract creation is not allowed. Because supplier {0} has a valid trial contract!",
            100026 => "Trial contract creation is not allowed. Because supplier {0} has a valid contract!",
            100027 => "Quotation '{0}' is created by annex, trial contract or agreement",
            100028 => "{0} {1} fail! Because {1} status is changed to {2}!",//Approve annex fail! Because annex status is changed to {status}!
            100029 => "File name duplicate",
            100030 => "Do not exceed 4 billing cycle",
            100031 => "The start date of Annex duration must be greater than or equal to the start date of Contract duration: {0} - {1}",
            100032 => "The Annex duration must be greater than or equal to Contract duration: {0} - {1}",
            100033 => "Delete contract fail, because annex exists!",
            100034 => "Annex creation is not allowed. Because supplier {0} an existing extend contract duration!",
            100035 => "{0} number length in 50 character",//Agreement/Contract/Trial Contract/Annex number length in 50 character
            100036 => "{0}",
            100037 => "Approve {0} fail! Because {0} duration invalid. Please check and update data again!",
            100038 => "Contract creation is not allowed. Because trial contract {0} exists for contract code {1}!",
            _ => string.Empty
        };
    }
}