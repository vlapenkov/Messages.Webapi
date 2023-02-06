namespace RK.Statistic.Interfaces;

/// <summary>
/// Названия таблиц
/// </summary>
public static class Tables
{
    /// <summary>
    /// Таблица для сохранения просмотров продуктов
    /// </summary>
    public static class ProductReadTable
    {
        /// <summary>
        /// Название таблицы
        /// </summary>
        public const string TableName = "product_read";
        
#pragma warning disable CS1591
        public const string IdColumn = "id";
        public const string PageColumn = "page";
        public const string ProductionColumn = "production";
        public const string ProductionIdColumn = "productionId";
        public const string CategoryIdColumn = "categoryId";
        public const string ProducerColumn = "producer";
        public const string UsernameColumn = "username";
        public const string CreatedColumn = "created";
#pragma warning restore CS1591
    }
}

