//
//  Copyright (c) Microsoft Corporation. All rights reserved.
//

using System;

namespace Microsoft.Azure.Communication.Email.SharedClients.Exceptions
{
    public class InvalidEmailAddressException : Exception
    {
        public InvalidEmailAddressException(string message) : base(message)
        {

        }
    }
}