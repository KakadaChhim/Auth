namespace Authentication.Helper
{
    public class LogicException : Exception
    {
        public LogicException(string msg) : base(msg)
        {
        }
    }

    public class NotFoundException : LogicException
    {
        public NotFoundException(string msg) : base(msg)
        {
        }
    }

    public class UniqueException : LogicException
    {
        public UniqueException(string msg) : base(msg)
        {
        }
    }

    public class UnauthorizedException : LogicException
    {
        public UnauthorizedException(string msg) : base(msg)
        {
        }
    }

    public class InfoException : LogicException
    {
        public InfoException(string msg) : base(msg)
        {
        }
    }
}
