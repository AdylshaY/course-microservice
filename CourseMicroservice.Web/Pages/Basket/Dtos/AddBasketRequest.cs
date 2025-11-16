namespace CourseMicroservice.Web.Pages.Basket.Dtos
{
    public record AddBasketRequest(
        Guid CourseId,
        string CourseName,
        decimal CoursePrice,
        string? ImageUrl
    );
}
