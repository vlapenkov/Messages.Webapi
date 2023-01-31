namespace RK.Statistic.Logic.Queries.Dto;

/// <summary>
/// Популярный товар
/// </summary>
/// <param name="Production">Название товара</param>
/// <param name="CategoryId">ИД категории</param>
/// <param name="Producer">Производитель</param>
/// <param name="Count">Количество просмотров</param>
public record PopularProduct(string Production, long CategoryId, string Producer, long Count);
