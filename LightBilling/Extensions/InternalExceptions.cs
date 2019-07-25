using System;
using System.Runtime.Serialization;

namespace LightBilling.Extensions
{
    /// <summary>
    /// Кастомные эксепшены
    /// </summary>
    public class InternalExceptions
    {
        /// <inheritdoc />
        public class NotFoundException : Exception {

            /// <inheritdoc />
            public NotFoundException(string error)
                : base(error) {
            }

            /// <inheritdoc />
            public NotFoundException(string error, Exception exception)
                : base(error, exception) {
            }

            /// <inheritdoc />
            protected NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) {
            }

        }
    }
}