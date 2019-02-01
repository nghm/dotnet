namespace Pitech.TestUtils
{
    using System;

    public class BookService
    {
        public BookService(string dbString)
        {
            throw new ArgumentNullException(nameof(dbString));
        }

        public object Fetch(int id)
        {
            return null;
        }
    }
}
