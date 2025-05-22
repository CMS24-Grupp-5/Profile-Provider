namespace Profile.ResponsResults
{
    public class Profile:BaseResult
    {
      
    }

    public class ProfileResult<T>:Profile
    {
        public T? Result { get; set; }
    }
}
