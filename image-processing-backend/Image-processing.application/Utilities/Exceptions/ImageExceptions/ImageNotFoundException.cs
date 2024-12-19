namespace Image_processing.application.Utilities.Exceptions.ImageExceptions
{
    [Serializable]
    public class ImageNotFoundException : Exception
    {
        public ImageNotFoundException()
        {
        }

        public ImageNotFoundException(string? message) : base(message)
        {
        }

        public ImageNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}