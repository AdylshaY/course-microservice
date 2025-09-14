namespace CourseMicroservice.Bus.Events
{
    public record OrderCreatedEvent(Guid OrderId, Guid UserId);
}
