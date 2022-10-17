using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Messages.Common.Helpers
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Метод возвращает полную копию объекта или коллекции включая вложенные объекты
        /// (см. паттерн prototype)
        /// </summary>
        /// <param name="self">исходный объект</param>
        /// <typeparam name="T">исходный тип</typeparam>
        /// <returns>клонированный объект</returns>
        public static T DeepCopy<T>(this T self)
        {
            var resultSerialized = JsonSerializer.Serialize(self);
            
            var result = JsonSerializer.Deserialize<T>(resultSerialized);

            return result;
        }

        /// <summary>
        /// Проверка, есть ли в типе IEnumerable данные
        /// </summary>
        public static bool IsAny<T>(this IEnumerable<T> data)
        {
            return data != null  && data.Any();
        }
        
        /// <summary>
        /// Сравнить 2 массива без учета порядка
        /// </summary>
        /// <param name="source">исходный</param>
        /// <param name="dest">сравниваемый</param>
        /// <typeparam name="T">тип</typeparam>
        /// <returns>true - если массивы одинаковы (без учета порядка), иначе false</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static bool AreEqual<T>(this IReadOnlyCollection<T> source, IReadOnlyCollection<T> dest) where T:struct
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (dest == null) throw new ArgumentNullException(nameof(dest));

            if (source.Count != dest.Count) return false;

            var orderedSource =source.OrderBy(self => self).ToArray();
            var orderedDest =source.OrderBy(self => self).ToArray();

            for (int counter =0 ; counter<orderedSource.Length; counter++)
            {
                if (!orderedSource[counter].Equals(orderedDest[counter])) return false;
            }

            return true;

        }

    }
}