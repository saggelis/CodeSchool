namespace FlipWeen.MVC.Api
{
    public interface ITokenContainer
    {
        object ApiToken { get; set; }

        string FullName { get; set; }

        int? UserId { get; set; }
    }
}