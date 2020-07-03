using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prog1
{
    class buy // Класс покупки
    {
        public int number = 0; // Количество товара
        public double price = 0.0; // Стоимость за шт
        public double priceForAll = 0.0; // Стоимость всего товара
        public buy(double Price, int Number) // Конструктор класса
        {
            price = Price;
            number = Number;
            priceForAll = price * ((double)number); // Находим стоимость всего товара
        }
    }
}
