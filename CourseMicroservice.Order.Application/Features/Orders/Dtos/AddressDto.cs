namespace CourseMicroservice.Order.Application.Features.Orders.Dtos;

public record AddressDto(string Province, string District, string Street, string ZipCode, string Line);
