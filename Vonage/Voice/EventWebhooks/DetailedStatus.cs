using System;
using System.Collections.Generic;
using System.Text;

namespace Vonage.Voice.EventWebhooks
{
    public enum DetailedStatus
    {
        /// <summary>
        /// no detail provided
        /// </summary>
        no_detail,
        /// <summary>
        /// detail provided but not mapped to an enum
        /// </summary>
        unmapped_detail,
        /// <summary>
        /// number invalid
        /// </summary>
        invalid_number,
        /// <summary>
        /// Rejected by carrier
        /// </summary>
        restricted,
        /// <summary>
        /// rejected by callee
        /// </summary>
        declined,
        /// <summary>
        /// cannot route to the number
        /// </summary>
        cannot_route,
        /// <summary>
        /// Number is not available anymore.
        /// </summary>
        number_out_of_service,
        /// <summary>
        /// Server error or failure
        /// </summary>
        internal_error,
        /// <summary>
        /// Carrier timed out
        /// </summary>
        carrier_timeout,
        /// <summary>
        /// Callee is temorarily unavailable.
        /// </summary>
        unavailable
    }
}
