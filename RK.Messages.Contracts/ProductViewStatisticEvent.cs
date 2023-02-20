namespace RK.Messages.Shared;

/// <summary>
/// Событие открытия карточки товара
/// </summary>
public class ProductViewStatisticEvent
{
    /// <summary>
    /// Url страницы
    /// </summary>
    public string? Page { get; set; }
    /// <summary>
    /// Название товара
    /// </summary>
    public string? Production { get; set; }
    /// <summary>
    /// Ид товара
    /// </summary>
    public long ProductionId { get; set; }
    /// <summary>
    /// Ид категории
    /// </summary>
    public long CategoryId { get; set; }
    /// <summary>
    /// Название производителя
    /// </summary>
    public string? Producer { get; set; }
    /// <summary>
    /// Имя пользваотеля
    /// </summary>
    public string? UserName { get; set; }
    /// <summary>
    /// Дата создания события
    /// </summary>
    public DateTime Created { get; set; }
}