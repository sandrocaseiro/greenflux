using System;

namespace Greenflux.Exceptions
{
    public class AppException : BaseException
    {
        private readonly AppErrors _errorType;

        public AppException(AppErrors errorType, Exception ex) : base(ex)
        {
            _errorType = errorType;
        }

        public AppException(AppErrors errorType) : base()
        {
            _errorType = errorType;
        }

        public override AppErrors AppError => _errorType;
    }
}
