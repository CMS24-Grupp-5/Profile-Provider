namespace Profile.ResponsResults
{
    public class RepositoryResult<TResult>
    {
        public TResult? Result { get; set; }
        public bool Succeeded { get; set; }
        public string? Message { get; set; }
        public int StatusCode { get; set; }
    }
}
