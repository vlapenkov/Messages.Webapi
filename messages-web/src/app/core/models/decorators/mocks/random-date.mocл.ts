/** Выдаёт случайный деньв пределах +-deltaDays от сегодняшнего дня */
export const randomDate =
  (deltaDays = 10) =>
  () => {
    const date = new Date();
    const addDays = Math.floor(Math.random() * deltaDays * 2);
    date.setDate(date.getDate() + addDays - deltaDays);
    return date;
  };
