using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prog1
{
    public partial class Form1 : Form
    {
        DateTime timeInSimulation; // Глобавльно обявляем время в симуляции
        List<buy> Buy = new List<buy>(); // Глобавльный список объектов класса покупки
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Настраиваем время симуляции
            Random rnd = new Random(); // Создаём объект рандом
            int Hours = rnd.Next(0, 24), Minutes = rnd.Next(0, 60), Seconds = rnd.Next(0, 60); // Случайно создаём время
            timeInSimulation = new DateTime(2020, 2, 2, Hours, Minutes, Seconds); // Заносим эти значения в объект датетайм
            label1.Text = timeInSimulation.ToLongTimeString(); // Выводим время
            richTextBox1.Text = "Кол-во\t\t цена товара\n"; // Устанавливаем заголовок
            timer1.Interval = 1000; // Устанавливаем интервал таймера 1000мс=1с
            timer1.Tick += timer1_Tick; // Добавляем тик таймера
            timer1.Start(); // Запускаем таймер
        }
        // Функция обработки тика таймер
        private void timer1_Tick(object sender, EventArgs e)
        {
            timeInSimulation=timeInSimulation.AddMinutes(1); // Каждый тик(1с) добавляем 1 минуту к датетайм
            label1.Text = timeInSimulation.ToLongTimeString(); // Выводим на лейбл время в симуляции
            double totalPrice = 0.0, discountPrice = 0.0; // Суммарная стоимость покупок без скидки и со скидкой
            int discount = 0; // Скидка
            label7.Text = "Акции:"; // Выводим на label акции
            for (int i = 0; i < Buy.Count; i++)
            {
                totalPrice += Buy[i].priceForAll; // Считаем сумму покупок без скидки
            }
            richTextBox2.Text = String.Format("Без скидки = {0}\n", totalPrice);// Выводим сумму покупок без скидки
            if (timeInSimulation.Hour >= 8 && timeInSimulation.Hour < 11) // Если утро
            {
                discount = 15;// То скидка 15%
                label7.Text = String.Format("Акции: сейчас действует скидка {0}%", discount); // Выводим скидку на лабел
            }
            else if (timeInSimulation.Hour >= 22 && timeInSimulation.Hour < 24) // Если ночь
            {
                discount = 10; // Скидка 10%
                label7.Text = String.Format("Акции: сейчас действует скидка {0}%", discount); // Выводим на лабел
            }
            discountPrice = totalPrice * ((double)(100 - discount) / 100); // Считаем суммарную стоимсоть со скидкой
            richTextBox2.Text += String.Format("Со скидкой = {0}", discountPrice); //Выводим стоисоть со скидкой
        }
        // Клавиша ввод
        private void button1_Click(object sender, EventArgs e)
        {
            try // Используем try для отплавливания ошибок при вводе
            {
                if (double.Parse(textBox1.Text) > 0.0) // Если стоимость больше 0
                {
                    Buy.Add(new buy(double.Parse(textBox1.Text), (int)numericUpDown1.Value)); // Добавляем объект класса с мараметрами стоимость и кол-во
                    richTextBox1.Text += String.Format("{0}\tx\t{1}\n", (int)numericUpDown1.Value, double.Parse(textBox1.Text)); // Выводим данные параметры в чек
                }
                else // Если нет
                    MessageBox.Show("Такая цена невозможна"); // То выводим окно с сообщением
            }
            catch (Exception bug) // Если где-то оказалась ошибка
            {
                MessageBox.Show(bug.Message); // То выводим сообщение об ошибке
            }
        }
        // Клавиша сброс
        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "Кол-во\t\t цена товара\n"; // Очищаем поле чека
            Buy.Clear(); // Очищаем список покупок
        }
    }
}
