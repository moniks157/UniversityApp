namespace UniversityApp
{
    public static class Constants
    {
        public const string GENDER_REGEX = @"\b[kKmM]\b";
        public const string NAME_REGEX = @"((^[A-Z])[A-Za-z]*)([\s-]*[A-Z]+[a-z]*)*";

        public const int MIN_AGE = 16;
        public const int AGE_OF_ADULTHOOD = 18;
        public const int MAX_AGE = 200;

        public const int MIN_PAGE_SIZE = 1;
        public const int MAX_PAGE_SIZE = 50;
    }
}
